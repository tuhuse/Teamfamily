using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PridictionFrogControll : MonoBehaviour
{
    private Rigidbody2D _rb;

    private bool _isAraive;
    private bool _isJump;
    public bool _isinvincible = false;�@//���G����

    private float _downMultipl = 1f;�@//��Q���ɓ����������̒�R��
    private float _downSpeed;�@//���ۂ̌�������X�s�[�h
    private float _returnSpeed = 0.025f;//�X�s�[�h�_�E�����痧�Ē�������

    [Header("�v���C���[���x")]
    [SerializeField] private float _movespeed = 10;

    [Header("�v���C���[�W�����v")]
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

        if (_isAraive)//�����Ă鎞�ɓ�����悤��
        {
            //�ړ�
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (_movespeed >= 10)//�ʏ�̈ړ�
                {
                    _rb.velocity = new Vector3(-_movespeed, _rb.velocity.y, 0);//*Time.deltaTime;
                }
                else�@//�ړ����x�����X�Ɍ��ɖ߂�
                {
                    _rb.velocity = new Vector3(-_movespeed, _rb.velocity.y, 0); //* Time.deltaTime ;
                    _movespeed = Mathf.Abs(_movespeed + 0.075f) * Time.deltaTime ;
                }
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (_movespeed >= 10)//�ʏ�̈ړ�
                {
                    _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0); //* Time.deltaTime ;
                }
                else�@//�ړ����x�����X�Ɍ��ɖ߂�
                {
                    _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0); //* Time.deltaTime ;
                    _movespeed = Mathf.Abs(_movespeed + _returnSpeed); //* Time.deltaTime ;
                }
            }

            //�W�����v
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
    {//��i�W�����v���Ȃ��悤�ɂ��邽��
        if (collision.gameObject.CompareTag("Flor"))
        {

            _isJump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("edge"))
        {
            _isAraive = false; //�ǒ[�ŃW�����v���Ȃ��悤�ɂ�����
        }
    }

    public void ObstacleCollision(float speedDownValue, float jumpDownValue)
    {
        if (_isinvincible == false) //��Q���ɓ���������
        {
            _downSpeed = speedDownValue * _downMultipl;
            _movespeed = Mathf.Abs(_movespeed - _downSpeed);
        }

        else//��Q���ɂ������Ă��A�r���e�B������
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