using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MucusFrogCntrolle : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool _isAlive;
    private bool _isJump;

    [Header("プレイヤー速度")]
    [SerializeField] private float _movespeed = 5;
    [Header("プレイヤージャンプ")]
    [SerializeField] private float _jumppower = 10;
    private float _downMultipl = 1f;　//障害物に当たった時の抵抗力
    private float _downSpeed; //実際の減少するスピード

   

   

    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //JumpOrbitTra = JumpOrbit.transform.position;//ジャンプ軌道のポジション初期値を入れる
        StartCoroutine(StartWait());
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAlive)//生きてる時に動けるように
        {
            //移動
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                if (_movespeed >= 5)//通常の移動
                {
                    _rb.velocity = new Vector3(-_movespeed, _rb.velocity.y, 0);
                } else //移動速度を徐々に元に戻す
                  {
                    _rb.velocity = new Vector3(-_movespeed, _rb.velocity.y, 0);
                    _movespeed = Mathf.Abs(_movespeed + 0.025f);
                }
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                if (_movespeed >= 5)//通常の移動
                {
                    _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0);
                } else //移動速度を徐々に元に戻す
                  {
                    _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0);
                    _movespeed = Mathf.Abs(_movespeed + 0.025f);
                }
            }

            //ジャンプ
            if (_isJump)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _rb.velocity = new Vector3(_rb.velocity.x, _jumppower, 0);

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
            _isAlive = false;//壁端でジャンプしないようにするやつ
        }
        
        if (collision.gameObject.layer == 9)//髭
        {
            collision.gameObject.SetActive(false);//髭消える
            StartCoroutine(Beard());
        }
       
    }
    private IEnumerator Beard()
    {
       
        _movespeed = 3;
        yield return new WaitForSeconds(1);
        _movespeed = 5;
    }
    private IEnumerator StartWait() {
        yield return new WaitForSeconds(5);
      _isAlive  = true;

    }
    public void ObstacleCollision(float speedDownValue, float jumpDownValue) {

        _downSpeed = speedDownValue * _downMultipl;
        _movespeed = Mathf.Abs(_movespeed - _downSpeed);

    }
}

