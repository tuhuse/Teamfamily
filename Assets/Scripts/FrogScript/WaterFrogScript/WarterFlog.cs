using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarterFlog : MonoBehaviour {


    private bool _isAbillity = false;//falseだったら発動可能
    private bool _isAlive;
    private bool _isJump;
    private bool _isWaterball;
    private string _situation;
    private Rigidbody2D _rb;
    private Animator _waterFrog;
    [Header("水玉入れて")]
    [SerializeField] private GameObject _waterBall;
    [Header("発射位置入れてね！")]
    [SerializeField] private Transform _waterSpawn;
    [Header("アビリティ速度")]
    [SerializeField] private float _abillitySpeed;
    [Header("通常プレイヤー速度")]
    [SerializeField] private float _movespeed = 5;
    [Header("プレイヤージャンプ")]
    [SerializeField] private float _jumppower = 10;
    [Header("プレイヤー速度")]
    [SerializeField] private float _warterspeed = 5;
    private float _downMultipl = 1f;　//障害物に当たった時の抵抗力
    private float _downSpeed; //実際の減少するスピード

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StartWait());
       
        _waterFrog = GetComponent<Animator>();
        _situation = "Stay";
        
    }

    // Update is called once per frame
    void Update() {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down * 2);
        if (_isWaterball) {
            if (Input.GetKeyDown(KeyCode.G))//水玉生成
            {
                Instantiate(_waterBall, _waterSpawn.position, Quaternion.identity, this.transform);
                _isWaterball = false;
            }
        }

        if (_isAlive) {
          

            NomalController();

            if (_isAbillity && _isAlive) { //abillityがtrueの時発動

                AbillityController();
            }
        }
       
        Froganimation();
    }
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.layer == 11) {
            StartCoroutine(Cooldown());
            collision.gameObject.SetActive(false);
        }
    }
    private IEnumerator Cooldown() {

        _isAbillity = true;
        yield return new WaitForSeconds(3);
        _isAbillity = false;
        yield return new WaitForSeconds(8);
        _isWaterball = true;
    }
    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Flor")) {
            _isJump = true;
            
            if (_isAlive) {

                _situation = "Stay";
                //移動
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
                    Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {

                    _situation = "Move";//通常アニメーション
                }


                if (_isAbillity && _isAlive) { //abillityがtrueの時発動

                    AbillityControllerAnimation();//アビリティアニメーション
                }
            }

        }
        if (collision.gameObject.layer == 7)//粘液の床
       {
            _jumppower = 2;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {

        //二段ジャンプしないようにするため
        if (collision.gameObject.CompareTag("Flor")) {
            _isJump = false;
            _situation = "Jump";
        }
        if (collision.gameObject.layer == 7)//粘液の床
       {
            _jumppower = 10;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("edge")) {
            _isAlive = false;//壁端でジャンプしないようにするやつ
        }

        if (collision.gameObject.layer == 9)//髭
       {
            collision.gameObject.SetActive(false);//髭消える
            StartCoroutine(Beard());
        }
    }
    private void NomalController() {

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
        if (_isJump) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                
                _rb.velocity = new Vector3(_rb.velocity.x, _jumppower, 0);
             
            }
        }



    }
    private IEnumerator Beard() {

        _movespeed = 3;
        yield return new WaitForSeconds(1);
        _movespeed = 5;
    }
    private IEnumerator FirstCooldown() {
        _isWaterball = false;
        yield return new WaitForSeconds(8);
        _isWaterball = true;
    }
    private IEnumerator StartWait() {
        yield return new WaitForSeconds(5);
        _isAlive = true;
        StartCoroutine(FirstCooldown());
    }
    private void Froganimation() {
        switch (_situation) {
            case "Stay":
                _waterFrog.SetBool("Stay", true);
                _waterFrog.SetBool("Move", false);
                _waterFrog.SetBool("Jump", false);
                _waterFrog.SetBool("Skill", false);
                break;
            case "Move":
                _waterFrog.SetBool("Stay", false);
                _waterFrog.SetBool("Move", true);
                _waterFrog.SetBool("Jump", false);
                _waterFrog.SetBool("Skill", false);
                break;
            case "Jump":
                _waterFrog.SetBool("Stay", false);
                _waterFrog.SetBool("Move", false);
                _waterFrog.SetBool("Jump", true);
                _waterFrog.SetBool("Skill", false);
                break;
            case "Skill":
                _waterFrog.SetBool("Stay", false);
                _waterFrog.SetBool("Move", false);
                _waterFrog.SetBool("Jump", false);
                _waterFrog.SetBool("Skill", true);
                break;
        }
    }
    private void AbillityController() {
        //移動
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            _rb.velocity = new Vector3(-_warterspeed * _abillitySpeed, _rb.velocity.y, 0);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            _situation = "Skill";
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            _rb.velocity = new Vector3(_warterspeed * _abillitySpeed, _rb.velocity.y, 0);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            _situation = "Skill";
        }
        if (_isJump && Input.GetKeyDown(KeyCode.Space)) {

            _rb.velocity = new Vector3(_rb.velocity.x, _jumppower, 0);

        }
    }
    private void AbillityControllerAnimation() {
        //移動
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {           
            _situation = "Skill";
        }
      
    }
    public void ObstacleCollision(float speedDownValue, float jumpDownValue) {

        _downSpeed = speedDownValue * _downMultipl;
        _movespeed = Mathf.Abs(_movespeed - _downSpeed);

    }
}
