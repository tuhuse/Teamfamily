using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueScriptCpuAi : MonoBehaviour
{
    private Vector3 _tongueSca;
    //private Transform _tongueTra;
    private bool _istongueReduction = false; //falseは拡大中　//trueは縮小中

    private float _speedDown = 10f;
    private float _jumpDown = 10f;

    private  float _scaleChange = 0.05f;
    private float _positionChange = 0.025f;
    private BoxCollider2D _aiBoxColl = default;
    // Start is called before the first frame update
    void Start()
    {
        _tongueSca = this.GetComponent<Transform>().localScale;
        //_tongueTra = this.transform;
        _aiBoxColl = this.gameObject.GetComponent<BoxCollider2D>();
        _aiBoxColl.enabled = false;
    }

    private void Update()
    {
        
            if (!_istongueReduction)
            {
            if (_aiBoxColl.enabled==false) {
                _aiBoxColl.enabled = true;
            }
                //舌を伸ばす
                this.transform.localScale+= new Vector3(_scaleChange, 0, 0) * Time.deltaTime * 500;
                this.transform.localPosition+= new Vector3(_positionChange, 0, 0) * Time.deltaTime * 500;
            }
            else
            {
                if (this.transform.localScale.x <= _tongueSca.x)
                {
                    //攻撃を止める
                    _istongueReduction = false;
                    _scaleChange = 0.05f;
                    _positionChange = 0.025f;

                if (_aiBoxColl.enabled == true) {
                    _aiBoxColl.enabled = false;
                }
            }
                else
                {
                    //舌を縮める
                    this.transform.localScale += new Vector3(_scaleChange, 0, 0) * Time.deltaTime * 500;
                    this.transform.localPosition += new Vector3(_positionChange, 0, 0) * Time.deltaTime * 500;
                    _scaleChange -= 0.001f;
                    _positionChange -= 0.0005f;
                
                }
            }

            if (this.transform.localScale.x >= 3 && !_istongueReduction)
            {
                //舌が最大まで伸びたら縮小開始
                _istongueReduction = true;


        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        //プレイヤー用
        if (collision.gameObject.layer == 6)//Mucusflog用
        {
            collision.gameObject.GetComponent<MucusFrogCntrolle>().ObstacleCollision(_speedDown, _jumpDown);
        } else if (collision.gameObject.layer == 8)//HigeFlog用
          {
            collision.gameObject.GetComponent<BeardFrogContlloer>().ObstacleCollision(_speedDown, _jumpDown);
        } else if (collision.gameObject.layer == 10)//WaterFrog用
          {
            collision.gameObject.GetComponent<WarterFlog>().ObstacleCollision(_speedDown, _jumpDown);
        } else if (collision.gameObject.layer == 12)//PridictionFrog用
          {

            collision.gameObject.GetComponent<PridictionFrogControll>().ObstacleCollision(_speedDown, _jumpDown);
        }
        //ここまで

        //CPU用
        if (collision.gameObject.layer == 14)//Mucusflog用
        {
            collision.gameObject.GetComponent<MucusFrogCpuAi>().ObstacleCollision(_speedDown, _jumpDown);
        } else if (collision.gameObject.layer == 15)//HigeFlog用
          {
            collision.gameObject.GetComponent<BeardFrogCpuAi>().ObstacleCollision(_speedDown, _jumpDown);
        } else if (collision.gameObject.layer == 16)//WaterFrog用
          {
            collision.gameObject.GetComponent<WaterFrogCpuAi>().ObstacleCollision(_speedDown, _jumpDown);
        } else if (collision.gameObject.layer == 17)//PridictionFrog用
          {
            collision.gameObject.GetComponent<PridictionFrogAIScript>().CPUObstacleCollision(_speedDown, _jumpDown);
        }
        //ここまで
    }
}
