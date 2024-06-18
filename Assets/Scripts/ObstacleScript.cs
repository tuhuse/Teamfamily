using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //障害物に当たった時の挙動
    private float _speedDown = 5f;
    private float _jumpDown = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤー用
        if (collision.gameObject.layer == 6)//Mucusflog用
        {
            collision.gameObject.GetComponent<MucusFrogCntrolle>().ObstacleCollision(_speedDown, _jumpDown);
        }
        else if(collision.gameObject.layer == 8)//HigeFlog用
        {
            collision.gameObject.GetComponent<BeardFrogContlloer>().ObstacleCollision(_speedDown, _jumpDown);
        }
        else if(collision.gameObject.layer == 10)//WaterFrog用
        {
            collision.gameObject.GetComponent<WarterFlog>().ObstacleCollision(_speedDown, _jumpDown);
        }
        else if(collision.gameObject.layer == 12)//PridictionFrog用
        {
            
            collision.gameObject.GetComponent<PridictionFrogControll>().ObstacleCollision(_speedDown  ,_jumpDown);
        }
        //ここまで

        //CPU用
        if (collision.gameObject.layer == 14)//Mucusflog用
        {
            collision.gameObject.GetComponent<MucusFrogCpuAi>().ObstacleCollision(_speedDown, _jumpDown);
        }
        else if (collision.gameObject.layer == 15)//HigeFlog用
        {
            collision.gameObject.GetComponent<BeardFrogCpuAi>().ObstacleCollision(_speedDown, _jumpDown);
        }
        else if (collision.gameObject.layer == 16)//WaterFrog用
        {
            collision.gameObject.GetComponent<WaterFrogCpuAi>().ObstacleCollision(_speedDown, _jumpDown);
        }
        else if (collision.gameObject.layer == 17)//PridictionFrog用
        {
            collision.gameObject.GetComponent<PridictionFrogAIScript>().CPUObstacleCollision(_speedDown, _jumpDown);
        }
        //ここまで

        if (collision.gameObject.tag == "ObstacleJudge")//CPUの回避用
        {
            collision.gameObject.GetComponent<ObstacleJudgeScript>().AIAvoidance();
        }
    }
}
