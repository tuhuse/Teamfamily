using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PridictionFrogControll : MonoBehaviour
{
    private Rigidbody2D _rb;

    private bool _isAraive;
    private bool _isJump;
    public bool _isinvincible = false;　//無敵判定

    private float _downMultipl = 1f;　//障害物に当たった時の抵抗力
    private float _downSpeed;　//実際の減少するスピード
    private float _returnSpeed = 0.025f;//スピードダウンから立て直す速さ

    [Header("プレイヤー速度")]
    [SerializeField] private float _movespeed = 10;

    [Header("プレイヤージャンプ")]
    [SerializeField] private float _jumppower = 20;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StartWait());

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print(_rb.velocity.x);
        }

        if (_isAraive)//生きてる時に動けるように
        {
            //移動
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (_movespeed >= 10)//通常の移動
                {
                    _rb.velocity = new Vector3(-_movespeed, _rb.velocity.y, 0);//*Time.deltaTime;
                }
                else　//移動速度を徐々に元に戻す
                {
                    _rb.velocity = new Vector3(-_movespeed, _rb.velocity.y, 0); //* Time.deltaTime ;
                    _movespeed = Mathf.Abs(_movespeed + 0.075f) * Time.deltaTime ;
                }
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (_movespeed >= 10)//通常の移動
                {
                    _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0); //* Time.deltaTime ;
                }
                else　//移動速度を徐々に元に戻す
                {
                    _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0); //* Time.deltaTime ;
                    _movespeed = Mathf.Abs(_movespeed + _returnSpeed); //* Time.deltaTime ;
                }
            }

            //ジャンプ
            if (_isJump)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _rb.velocity = new Vector3(_rb.velocity.x, _jumppower, 0); //* Time.deltaTime ;

                }
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Flor"))
        {
            _isJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {//二段ジャンプしないようにするため
        if (collision.gameObject.CompareTag("Flor"))
        {

            _isJump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("edge"))
        {
            _isAraive = false; //壁端でジャンプしないようにするやつ
        }
    }

    public void ObstacleCollision(float speedDownValue, float jumpDownValue)
    {
        if (_isinvincible == false) //障害物に当たった時
        {
            _downSpeed = speedDownValue * _downMultipl;
            _movespeed = Mathf.Abs(_movespeed - _downSpeed);
        }

        else//障害物にあたってかつアビリティ発動時
        {
            _movespeed += 7.5f;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            _isinvincible = false;
            StartCoroutine(SpeedReset());
            print("parry");
        }
    }

    private IEnumerator SpeedReset()
    {
        yield return new WaitForSeconds(6f);
        _movespeed = 20;
    }
    private IEnumerator StartWait() {
        yield return new WaitForSeconds(5);
        _isAraive = true;
      
    }
}