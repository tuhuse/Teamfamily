using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueScript : MonoBehaviour
{
    private Vector3 _tongueSca;
    private Transform _tongueTra;
    private bool _istongueReduction = false; //false�͊g�咆�@//true�͏k����
    private bool _isAttack;

    private float _speedDown = 10f;
    private float _jumpDown = 10f;
    private  float _scaleChange = 0.05f;
    private float _positionChange = 0.025f;

    private BoxCollider2D _boxColl = default;
    // Start is called before the first frame update
    void Start()
    {
        _tongueSca = this.GetComponent<Transform>().localScale;
        _tongueTra = this.transform;
        _isAttack = false;
        _boxColl = this.GetComponent<BoxCollider2D>();
        _boxColl.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            //�U��������Ȃ�������U���J�n
            if (!_isAttack) {
                _isAttack = true;
                _boxColl.enabled = true;
            }
        }
        if (_isAttack)
        {
            if (!_istongueReduction)
            {
                //���L�΂�
                this.transform.localScale+= new Vector3(_scaleChange, 0, 0) * Time.deltaTime * 500;
                this.transform.localPosition+= new Vector3(_positionChange, 0, 0) * Time.deltaTime * 500;

            }
            else
            {
                if (this.transform.localScale.x <= _tongueSca.x)
                {
                    //�U�����~�߂�
                    _istongueReduction = false;
                    _isAttack = false;
                    _scaleChange = 0.05f;
                    _positionChange = 0.025f;
                }
                else
                {
                    //����k�߂�
                    this.transform.localScale += new Vector3(_scaleChange, 0, 0) * Time.deltaTime * 1000;
                    this.transform.localPosition += new Vector3(_positionChange, 0, 0) * Time.deltaTime *1000;
                    _scaleChange -= 0.001f;
                    _positionChange -= 0.0005f;
                }
            }

            if (this.transform.localScale.x >= 3 && !_istongueReduction)
            {
                //�オ�ő�܂ŐL�т���k���J�n
                _istongueReduction = true;
                _boxColl.enabled = false;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //�v���C���[�p
        if (collision.gameObject.layer == 6)//Mucusflog�p
        {
            collision.gameObject.GetComponent<MucusFrogCntrolle>().ObstacleCollision(_speedDown, _jumpDown);
        } else if (collision.gameObject.layer == 8)//HigeFlog�p
          {
            collision.gameObject.GetComponent<BeardFrogContlloer>().ObstacleCollision(_speedDown, _jumpDown);
        } else if (collision.gameObject.layer == 10)//WaterFrog�p
          {
            collision.gameObject.GetComponent<WarterFlog>().ObstacleCollision(_speedDown, _jumpDown);
        } else if (collision.gameObject.layer == 12)//PridictionFrog�p
          {

            collision.gameObject.GetComponent<PridictionFrogControll>().ObstacleCollision(_speedDown, _jumpDown);
        }
        //�����܂�

        //CPU�p
        if (collision.gameObject.layer == 14)//Mucusflog�p
        {
            collision.gameObject.GetComponent<MucusFrogCpuAi>().ObstacleCollision(_speedDown, _jumpDown);
        } else if (collision.gameObject.layer == 15)//HigeFlog�p
          {
            collision.gameObject.GetComponent<BeardFrogCpuAi>().ObstacleCollision(_speedDown, _jumpDown);
        } else if (collision.gameObject.layer == 16)//WaterFrog�p
          {
            collision.gameObject.GetComponent<WaterFrogCpuAi>().ObstacleCollision(_speedDown, _jumpDown);
        } else if (collision.gameObject.layer == 17)//PridictionFrog�p
          {
            collision.gameObject.GetComponent<PridictionFrogAIScript>().CPUObstacleCollision(_speedDown, _jumpDown);
        }
        //�����܂�
    }

    public void AttackStart()
    {
        //�U���X�^�[�g
       _isAttack = true;
    }
}
