using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PridictionFrogAIScript : MonoBehaviour
{
    Rigidbody2D _aIrb;

    private int _randomAvoidance = 0;//�����_���ϐ�����

    private float _moveCPUSpeed = 10f;�@//�ړ����x
    private float _jumpSpeed = 20f;�@//�W�����v��
    private float _plaDistance = 0;//�v���C���[�Ƃ̋���
    private float _returnCPUSpeed = 0.025f;//�X�s�[�h�_�E�����痧�Ē�������
    private float _downCPUMultiple = 1; //�J�G�����Ƃ̒�R��
    private float _downCPUSpeed;//���ۂɗ�����X�s�[�h

    private bool _isAlive = false;
    private bool _isJump = false; //�W�����v������
    private bool _isRun = true;
    private bool _isCPUInvincible = false;//���G
    private bool _isCoolDown = false;

    [Header("�v���C���[")] [SerializeField] GameObject _pla;
    // Start is called before the first frame update
    void Start()
    {
        _aIrb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(StartWait());

    }

    // Update is called once per frame
    void Update() {
        if (_isAlive) {
            if (_isJump == true) //�W�����v
     {
                _aIrb.velocity = new Vector3(_aIrb.velocity.x, _jumpSpeed);
                _isJump = false;
            } else if (_isRun == true) {
                if (_moveCPUSpeed >= 10)//���i�͉��ړ�
                {
                    _aIrb.velocity = new Vector3(_moveCPUSpeed, _aIrb.velocity.y, 0); //* Time.deltaTime ;
                } else //�ړ����x�����X�Ɍ��ɖ߂�
                  {
                    _aIrb.velocity = new Vector3(_moveCPUSpeed, _aIrb.velocity.y, 0); //* Time.deltaTime ;
                    _moveCPUSpeed = Mathf.Abs(_moveCPUSpeed + _returnCPUSpeed); //* Time.deltaTime;
                }
            }
        }

     
    }

    public void DistanceCalculation() //���g�ƃv���C���[�Ƃ̋����̌v�Z
    {
        _plaDistance = Mathf.Abs(this.transform.position.x) - Mathf.Abs(_pla.transform.position.x);
        //���ʂ�+�Ȃ�CPU�����O�ɂ���(������x��)
        //���ʂ�-�Ȃ�CPU�������ɂ���i������x��)

        if (_plaDistance >= 0)//�{�C�x��
        {
            print("A");
            AvoidanceProbability();
        }
        else�@//������x��
        {
            print("B");  
            AvoidanceProbabilityLaxity();
        }
        print(_plaDistance);
    }



    private void AvoidanceProbability()  //��Q�����m���ł悯��
    {

        _randomAvoidance = Random.Range(1, 11);
        if (_randomAvoidance >= 3) //80���ŏ�Q�����悯��
        {
            _randomAvoidance = Random.Range(1, 4);

            if (_isCoolDown == false && _randomAvoidance == 1)//33���ŃA�r���e�B������parry
            {
                print("A Parry");
                _isCoolDown = true;
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 255);
               _isCPUInvincible = true;
                StartCoroutine(AbilityStop());
            }
            else //66%�ł�����Q�����悯��
            {
                print("A" + _randomAvoidance);
                Jump();
            }
            
        }
        else //20���ł悯��̂Ɏ��s����
        {
            print("A-B" + _randomAvoidance);
        }
    }

    private void AvoidanceProbabilityLaxity()
    {
        _randomAvoidance = Random.Range(1, 20);
        if (_randomAvoidance <= 13)//65%�ŏ�Q�����悯��
        {
            _randomAvoidance = Random.Range(1, 6);
            if (_randomAvoidance == 1)//20���ŃA�r���e�B������parry
            {
                print("B Parry");
                _isCoolDown = true;
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 255);
                _isCPUInvincible = true;
                StartCoroutine(AbilityStop());
            }
            else  //80%�ł�����Q�����悯��
            {
                print("B" + _randomAvoidance);
                Jump();
            }
        }
        else //35%�ŏ�Q�����悯��Ȃ�
        {
            print("A-B" + _randomAvoidance);
        }
    }

    private IEnumerator AbilityStop()
    {
        yield return new WaitForSeconds(2f);//�A�r���e�B�I��
        print("�A�r���e�B����");
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        _isCPUInvincible = false;
        StartCoroutine(CoolDownOut());
    }
    private IEnumerator CoolDownOut()
    {
        yield return new WaitForSeconds(20f);
        _isCoolDown = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Flor" && _isRun == false)�@//�W�����v�������ɒn�ʂɒ��n�����牡�ړ��ɖ߂�
        {
            _isJump = false;
            _isRun = true;
        }
    }

    private void Jump()
    {

        _isJump = true;
        _isRun = false;

    }
    public void CPUObstacleCollision(float speedDownValue, float jumpDownValue)
    {
        if (_isCPUInvincible == false) //��Q���ɓ���������
        {
            _downCPUSpeed = speedDownValue * _downCPUMultiple;
            _moveCPUSpeed = Mathf.Abs(_moveCPUSpeed - _downCPUSpeed);

        }
        else//��Q���ɂ������Ă��A�r���e�B������
        {
            _moveCPUSpeed += 7.5f;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            _isCPUInvincible = false;
            StartCoroutine(SpeedReset());
            print("parry");
        }
    }

    private IEnumerator SpeedReset()
    {
        yield return new WaitForSeconds(6f);
        _moveCPUSpeed = 20;
    }
    private IEnumerator StartWait() {
        yield return new WaitForSeconds(5);
        _isAlive = true;
      
    }
}
