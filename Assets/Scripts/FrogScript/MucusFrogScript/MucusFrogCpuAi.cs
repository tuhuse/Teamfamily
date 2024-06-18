using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MucusFrogCpuAi : MonoBehaviour
{
    [Header("プレイヤー")]
    [SerializeField]
    private Transform[] _player;
 
    [Header("ギミック")]
    [SerializeField]
    private Transform[] _enemys;
    [Header("能力発射位置")]
    [SerializeField]
    private Transform _spawn;
    [Header("選択")]
    [SerializeField]
    private Transform[] _chooses;

    [Header("粘液入れて")]
    [SerializeField] private GameObject _mucus;
    [Header("舌を入れて")]
    [SerializeField] private GameObject _tongue;

    private bool _isAbility = false;
    private bool _isdistans;
    private bool _isjump;
    private bool _isAlive;
    private bool _isAttack;

    [Header("CPUの速さ")]
    [SerializeField]
    private float _movespeed;
    [Header("CPUのジャンプ力")]
    [SerializeField]
    private float[] _movejump;
    [Header("CPUの選択ジャンプ力")]
    [SerializeField]
    private float[] _choosemovejump;
    private float _downMultipl = 1f;　//障害物に当たった時の抵抗力
    private float _downSpeed; //実際の減少するスピード
    private float _returnCPUSpeed = 0.025f;//スピードダウンから立て直す速さ

    
    //障害物
    private int _enemynumbers = 0;
    //上か下の選択する場所
    private int _choosenumbers = 0;
    //プレイヤー番号
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
        //プレイヤーのキャラの種類を取得
        _playernumbers = GameObject.FindGameObjectWithTag("select").GetComponent<SelectCharacter>()._playernumber;
        _enemynumbers = GameObject.FindGameObjectWithTag("StageRoop").GetComponent<StageRoopMan>()._enemynumber;
        _choosenumbers = GameObject.FindGameObjectWithTag("StageRoop").GetComponent<StageRoopMan>()._choosenumber;
    }
   
    // Update is called once per frame
    void Update() {
        #region CPU挙動
        float distancetoplayer = Vector3.Distance(this.transform.position, _player[_playernumbers].position);//プレイヤーとの距離
        //ギミックとの距離
        float distancetoenemy = Vector3.Distance(this.transform.position, _enemys[_enemynumbers].position);
        //上か下かを決める距離
        float distancetochoose = Vector3.Distance(this.transform.position, _chooses[_choosenumbers].position);
       
        if (GameObject.FindGameObjectWithTag("StageRoop").GetComponent<StageRoopMan>()._isRoop) {
            StartCoroutine(EnemynumberWait());
        }
        
        print(_enemynumbers);
        //生きている場合
        if (_isAlive) 
        {
            //普段は横移動
            if (_movespeed >= 20)
           {
                //右に移動
                _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0);
            }
            //移動速度を徐々に元に戻す
            else 
            {
                //右に移動
                _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0);
                _movespeed = Mathf.Abs(_movespeed + _returnCPUSpeed); 
            }
            _jotai = Jotai.Move;
            //上と下に選択肢があった場合 
            if (distancetochoose < 1 && this.transform.localPosition.x < _chooses[_choosenumbers].localPosition.x && _isjump)
                {
                //ジャンプかそのままかの選択
                _rb.velocity = Vector2.up * _choosemovejump[Random.Range(0, 2)];
                Choosecount();
            }
            //プレイヤーとの距離が離れている場合
            if (distancetoplayer > 3)
            {
               
                //障害物が目の前に来た時＆地面に足がついてる時
                if (distancetoenemy < 2 && this.transform.localPosition.x < _enemys[_enemynumbers].localPosition.x && _isjump)
                {
                    //確定でジャンプする
                    _rb.velocity = Vector2.up * _movejump[0];
                    Enemycount();
                }
                //プレイヤーが自分よりも後ろにいた場合
                if (this.transform.localPosition.x >= _player[_playernumbers].localPosition.x)
                    {
                    Ability();
                }
              
            }
            //プレイヤーのとの距離が近いとき
            else if (distancetoplayer < 3) {
                if (_isAttack && this.transform.localPosition.x < _player[_playernumbers].localPosition.x) {
                    _tongue.gameObject.SetActive(true);
                    StartCoroutine(TongueCoolDown());
                }
                if (this.transform.localPosition.x >= _player[_playernumbers].localPosition.x) {
                    Ability();
                }
                //障害物が目の前に来た時＆地面に足がついてる時
                if (distancetoenemy < 2 && this.transform.localPosition.x < _enemys[_enemynumbers].localPosition.x && _isjump)
                {
                    //大ジャンプ、小ジャンプ、跳ばないのランダムでジャンプ
                    _rb.velocity = Vector2.up * _movejump[Random.Range(0, 3)];
                    Enemycount();
                }

            }
        }

        #endregion
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        //床に足が着いてるとき
        if (collision.gameObject.CompareTag("Flor"))
        {
            _isjump = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //床から足が離れたとき
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
            //壁端でジャンプしないようにするやつ
            _isAlive = false;
        }
        //髭に当たった時
        if (collision.gameObject.layer == 9)
    {
            //髭消える
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
    //クールタイム
    private IEnumerator CoolDown() {
       
        _isAbility = false;
        yield return new WaitForSeconds(5);
        _isAbility = true;

    }
    private IEnumerator StartWait() {
        yield return new WaitForSeconds(5);
        _isAlive = true;
        StartCoroutine(CoolDown());//クールタイム
    }
    //ひげに当たった時の効果
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
            //次の障害物の距離を取得
            _enemynumbers++;
        }
    }
    private void Choosecount() {
        if (_enemynumbers < 15) {
            //次の選択肢の距離取得
            _choosenumbers++;
        }
    }
    private IEnumerator EnemynumberWait() {
        yield return new WaitForSeconds(3);
        _enemynumbers = GameObject.FindGameObjectWithTag("StageRoop").GetComponent<StageRoopMan>()._enemynumber;
        _choosenumbers = GameObject.FindGameObjectWithTag("StageRoop").GetComponent<StageRoopMan>()._choosenumber;
    }
}
