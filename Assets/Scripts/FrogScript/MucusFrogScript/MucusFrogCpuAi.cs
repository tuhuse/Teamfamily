using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MucusFrogCpuAi : MonoBehaviour
{
    [Header("�v���C���[")]
    [SerializeField]
    private Transform[] _player;
 
    [Header("�M�~�b�N")]
    [SerializeField]
    private Transform[] _enemys;
    [Header("�\�͔��ˈʒu")]
    [SerializeField]
    private Transform _spawn;
    [Header("�I��")]
    [SerializeField]
    private Transform[] _chooses;

    [Header("�S�t�����")]
    [SerializeField] private GameObject _mucus;
    [Header("�������")]
    [SerializeField] private GameObject _tongue;

    private bool _isAbility = false;
    private bool _isdistans;
    private bool _isjump;
    private bool _isAlive;
    private bool _isAttack;

    [Header("CPU�̑���")]
    [SerializeField]
    private float _movespeed;
    [Header("CPU�̃W�����v��")]
    [SerializeField]
    private float[] _movejump;
    [Header("CPU�̑I���W�����v��")]
    [SerializeField]
    private float[] _choosemovejump;
    private float _downMultipl = 1f;�@//��Q���ɓ����������̒�R��
    private float _downSpeed; //���ۂ̌�������X�s�[�h
    private float _returnCPUSpeed = 0.025f;//�X�s�[�h�_�E�����痧�Ē�������

    
    //��Q��
    private int _enemynumbers = 0;
    //�ォ���̑I������ꏊ
    private int _choosenumbers = 0;
    //�v���C���[�ԍ�
    private int _playernumbers;
    private Rigidbody2D _rb;
    private Animator _animator;

    private enum Jotai
    {
        Move,
        Jump

    }
    Jotai _jotai;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StartWait());
        _animator = GetComponent<Animator>();
        _jotai = default;
       
        _isAttack = true;
        //�v���C���[�̃L�����̎�ނ��擾
        _playernumbers = GameObject.FindGameObjectWithTag("select").GetComponent<SelectCharacter>()._playernumber;
        _enemynumbers = GameObject.FindGameObjectWithTag("StageRoop").GetComponent<StageRoopMan>()._enemynumber;
        _choosenumbers = GameObject.FindGameObjectWithTag("StageRoop").GetComponent<StageRoopMan>()._choosenumber;
    }
   
    // Update is called once per frame
    void Update() {
        #region CPU����
        float distancetoplayer = Vector3.Distance(this.transform.position, _player[_playernumbers].position);//�v���C���[�Ƃ̋���
        //�M�~�b�N�Ƃ̋���
        float distancetoenemy = Vector3.Distance(this.transform.position, _enemys[_enemynumbers].position);
        //�ォ���������߂鋗��
        float distancetochoose = Vector3.Distance(this.transform.position, _chooses[_choosenumbers].position);
       
        if (GameObject.FindGameObjectWithTag("StageRoop").GetComponent<StageRoopMan>()._isRoop) {
            StartCoroutine(EnemynumberWait());
        }
        
        print(_enemynumbers);
        //�����Ă���ꍇ
        if (_isAlive) 
        {
            //���i�͉��ړ�
            if (_movespeed >= 20)
           {
                //�E�Ɉړ�
                _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0);
            }
            //�ړ����x�����X�Ɍ��ɖ߂�
            else 
            {
                //�E�Ɉړ�
                _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0);
                _movespeed = Mathf.Abs(_movespeed + _returnCPUSpeed); 
            }
            _jotai = Jotai.Move;
            //��Ɖ��ɑI�������������ꍇ 
            if (distancetochoose < 1 && this.transform.localPosition.x < _chooses[_choosenumbers].localPosition.x && _isjump)
                {
                //�W�����v�����̂܂܂��̑I��
                _rb.velocity = Vector2.up * _choosemovejump[Random.Range(0, 2)];
                Choosecount();
            }
            //�v���C���[�Ƃ̋���������Ă���ꍇ
            if (distancetoplayer > 3)
            {
               
                //��Q�����ڂ̑O�ɗ��������n�ʂɑ������Ă鎞
                if (distancetoenemy < 2 && this.transform.localPosition.x < _enemys[_enemynumbers].localPosition.x && _isjump)
                {
                    //�m��ŃW�����v����
                    _rb.velocity = Vector2.up * _movejump[0];
                    Enemycount();
                }
                //�v���C���[�������������ɂ����ꍇ
                if (this.transform.localPosition.x >= _player[_playernumbers].localPosition.x)
                    {
                    Ability();
                }
              
            }
            //�v���C���[�̂Ƃ̋������߂��Ƃ�
            else if (distancetoplayer < 3) {
                if (_isAttack && this.transform.localPosition.x < _player[_playernumbers].localPosition.x) {
                    _tongue.gameObject.SetActive(true);
                    StartCoroutine(TongueCoolDown());
                }
                if (this.transform.localPosition.x >= _player[_playernumbers].localPosition.x) {
                    Ability();
                }
                //��Q�����ڂ̑O�ɗ��������n�ʂɑ������Ă鎞
                if (distancetoenemy < 2 && this.transform.localPosition.x < _enemys[_enemynumbers].localPosition.x && _isjump)
                {
                    //��W�����v�A���W�����v�A���΂Ȃ��̃����_���ŃW�����v
                    _rb.velocity = Vector2.up * _movejump[Random.Range(0, 3)];
                    Enemycount();
                }

            }
        }

        #endregion
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        //���ɑ��������Ă�Ƃ�
        if (collision.gameObject.CompareTag("Flor"))
        {
            _isjump = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //�����瑫�����ꂽ�Ƃ�
        if (collision.gameObject.CompareTag("Flor"))
        {
            _isjump = false;
            _jotai = Jotai.Jump;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("edge"))
        {
            //�ǒ[�ŃW�����v���Ȃ��悤�ɂ�����
            _isAlive = false;
        }
        //�E�ɓ���������
        if (collision.gameObject.layer == 9)
    {
            //�E������
            collision.gameObject.SetActive(false);
            StartCoroutine(Beard());
        }
    }
    private void FrogAnimation()
    {
        switch (_jotai)
            {
           
            case Jotai.Move:
                _animator.SetBool("Stand", false);
                _animator.SetBool("Move", true);
                _animator.SetBool("Jump", false);
                break; 
            case Jotai.Jump:
                _animator.SetBool("Stand", false);
                _animator.SetBool("Move", false);
                _animator.SetBool("Jump", true);
                break;
            default :
                _animator.SetBool("Stand", true);
                _animator.SetBool("Move", false);
                _animator.SetBool("Jump", false);
                break;
        }
    }
    private void Ability() {
        if (_isAbility) {
            Instantiate(_mucus, _spawn.position, Quaternion.identity);
            StartCoroutine(CoolDown());
        }
    }
    //�N�[���^�C��
    private IEnumerator CoolDown() {
       
        _isAbility = false;
        yield return new WaitForSeconds(5);
        _isAbility = true;

    }
    private IEnumerator StartWait() {
        yield return new WaitForSeconds(5);
        _isAlive = true;
        StartCoroutine(CoolDown());//�N�[���^�C��
    }
    //�Ђ��ɓ����������̌���
    private IEnumerator Beard()
        {

        _movespeed = 3;
        yield return new WaitForSeconds(1);
        _movespeed = 5;
    }
    private IEnumerator TongueCoolDown() {
        _isAttack = false;
        yield return new WaitForSeconds(0.6f);
        _tongue.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        _isAttack = true;
    }
    public void ObstacleCollision(float speedDownValue, float jumpDownValue) {

        _downSpeed = speedDownValue * _downMultipl;
        _movespeed = Mathf.Abs(_movespeed - _downSpeed);

    }
    private void Enemycount() {
        if (_enemynumbers < 5) {
            //���̏�Q���̋������擾
            _enemynumbers++;
        }
    }
    private void Choosecount() {
        if (_enemynumbers < 15) {
            //���̑I�����̋����擾
            _choosenumbers++;
        }
    }
    private IEnumerator EnemynumberWait() {
        yield return new WaitForSeconds(3);
        _enemynumbers = GameObject.FindGameObjectWithTag("StageRoop").GetComponent<StageRoopMan>()._enemynumber;
        _choosenumbers = GameObject.FindGameObjectWithTag("StageRoop").GetComponent<StageRoopMan>()._choosenumber;
    }
}
