using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MucusFrogCntrolle : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool _isAlive;
    private bool _isJump;

    [Header("�v���C���[���x")]
    [SerializeField] private float _movespeed = 5;
    [Header("�v���C���[�W�����v")]
    [SerializeField] private float _jumppower = 10;
    private float _downMultipl = 1f;�@//��Q���ɓ����������̒�R��
    private float _downSpeed; //���ۂ̌�������X�s�[�h

   

   

    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //JumpOrbitTra = JumpOrbit.transform.position;//�W�����v�O���̃|�W�V���������l������
        StartCoroutine(StartWait());
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAlive)//�����Ă鎞�ɓ�����悤��
        {
            //�ړ�
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                if (_movespeed >= 5)//�ʏ�̈ړ�
                {
                    _rb.velocity = new Vector3(-_movespeed, _rb.velocity.y, 0);
                } else //�ړ����x�����X�Ɍ��ɖ߂�
                  {
                    _rb.velocity = new Vector3(-_movespeed, _rb.velocity.y, 0);
                    _movespeed = Mathf.Abs(_movespeed + 0.025f);
                }
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                if (_movespeed >= 5)//�ʏ�̈ړ�
                {
                    _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0);
                } else //�ړ����x�����X�Ɍ��ɖ߂�
                  {
                    _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0);
                    _movespeed = Mathf.Abs(_movespeed + 0.025f);
                }
            }

            //�W�����v
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
            _isAlive = false;//�ǒ[�ŃW�����v���Ȃ��悤�ɂ�����
        }
        
        if (collision.gameObject.layer == 9)//�E
        {
            collision.gameObject.SetActive(false);//�E������
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

