using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PridictionFrogAIScript : MonoBehaviour
{
    Rigidbody2D _aIrb;

    private int _randomAvoidance = 0;//ランダム変数入れ

    private float _moveCPUSpeed = 10f;　//移動速度
    private float _jumpSpeed = 20f;　//ジャンプ力
    private float _plaDistance = 0;//プレイヤーとの距離
    private float _returnCPUSpeed = 0.025f;//スピードダウンから立て直す速さ
    private float _downCPUMultiple = 1; //カエルごとの抵抗力
    private float _downCPUSpeed;//実際に落ちるスピード

    private bool _isAlive = false;
    private bool _isJump = false; //ジャンプをする
    private bool _isRun = true;
    private bool _isCPUInvincible = false;//無敵
    private bool _isCoolDown = false;

    [Header("プレイヤー")] [SerializeField] GameObject _pla;
    // Start is called before the first frame update
    void Start()
    {
        _aIrb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(StartWait());

    }

    // Update is called once per frame
    void Update() {
        if (_isAlive) {
            if (_isJump == true) //ジャンプ
     {
                _aIrb.velocity = new Vector3(_aIrb.velocity.x, _jumpSpeed);
                _isJump = false;
            } else if (_isRun == true) {
                if (_moveCPUSpeed >= 10)//普段は横移動
                {
                    _aIrb.velocity = new Vector3(_moveCPUSpeed, _aIrb.velocity.y, 0); //* Time.deltaTime ;
                } else //移動速度を徐々に元に戻す
                  {
                    _aIrb.velocity = new Vector3(_moveCPUSpeed, _aIrb.velocity.y, 0); //* Time.deltaTime ;
                    _moveCPUSpeed = Mathf.Abs(_moveCPUSpeed + _returnCPUSpeed); //* Time.deltaTime;
                }
            }
        }

     
    }

    public void DistanceCalculation() //自身とプレイヤーとの距離の計算
    {
        _plaDistance = Mathf.Abs(this.transform.position.x) - Mathf.Abs(_pla.transform.position.x);
        //結果が+ならCPUよりも前にいる(手加減度↓)
        //結果が-ならCPUよりも後ろにいる（手加減度↑)

        if (_plaDistance >= 0)//本気度↑
        {
            print("A");
            AvoidanceProbability();
        }
        else　//手加減度↑
        {
            print("B");  
            AvoidanceProbabilityLaxity();
        }
        print(_plaDistance);
    }



    private void AvoidanceProbability()  //障害物を確率でよける
    {

        _randomAvoidance = Random.Range(1, 11);
        if (_randomAvoidance >= 3) //80％で障害物をよける
        {
            _randomAvoidance = Random.Range(1, 4);

            if (_isCoolDown == false && _randomAvoidance == 1)//33％でアビリティ発動＆parry
            {
                print("A Parry");
                _isCoolDown = true;
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 255);
               _isCPUInvincible = true;
                StartCoroutine(AbilityStop());
            }
            else //66%でただ障害物をよける
            {
                print("A" + _randomAvoidance);
                Jump();
            }
            
        }
        else //20％でよけるのに失敗する
        {
            print("A-B" + _randomAvoidance);
        }
    }

    private void AvoidanceProbabilityLaxity()
    {
        _randomAvoidance = Random.Range(1, 20);
        if (_randomAvoidance <= 13)//65%で障害物をよける
        {
            _randomAvoidance = Random.Range(1, 6);
            if (_randomAvoidance == 1)//20％でアビリティ発動＆parry
            {
                print("B Parry");
                _isCoolDown = true;
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 255);
                _isCPUInvincible = true;
                StartCoroutine(AbilityStop());
            }
            else  //80%でただ障害物をよける
            {
                print("B" + _randomAvoidance);
                Jump();
            }
        }
        else //35%で障害物をよけれない
        {
            print("A-B" + _randomAvoidance);
        }
    }

    private IEnumerator AbilityStop()
    {
        yield return new WaitForSeconds(2f);//アビリティ終了
        print("アビリティ解除");
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
        if (collision.gameObject.tag == "Flor" && _isRun == false)　//ジャンプした時に地面に着地したら横移動に戻す
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
        if (_isCPUInvincible == false) //障害物に当たった時
        {
            _downCPUSpeed = speedDownValue * _downCPUMultiple;
            _moveCPUSpeed = Mathf.Abs(_moveCPUSpeed - _downCPUSpeed);

        }
        else//障害物にあたってかつアビリティ発動時
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
