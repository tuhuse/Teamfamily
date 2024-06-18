using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //��Q���ɓ����������̋���
    private float _speedDown = 5f;
    private float _jumpDown = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[�p
        if (collision.gameObject.layer == 6)//Mucusflog�p
        {
            collision.gameObject.GetComponent<MucusFrogCntrolle>().ObstacleCollision(_speedDown, _jumpDown);
        }
        else if(collision.gameObject.layer == 8)//HigeFlog�p
        {
            collision.gameObject.GetComponent<BeardFrogContlloer>().ObstacleCollision(_speedDown, _jumpDown);
        }
        else if(collision.gameObject.layer == 10)//WaterFrog�p
        {
            collision.gameObject.GetComponent<WarterFlog>().ObstacleCollision(_speedDown, _jumpDown);
        }
        else if(collision.gameObject.layer == 12)//PridictionFrog�p
        {
            
            collision.gameObject.GetComponent<PridictionFrogControll>().ObstacleCollision(_speedDown  ,_jumpDown);
        }
        //�����܂�

        //CPU�p
        if (collision.gameObject.layer == 14)//Mucusflog�p
        {
            collision.gameObject.GetComponent<MucusFrogCpuAi>().ObstacleCollision(_speedDown, _jumpDown);
        }
        else if (collision.gameObject.layer == 15)//HigeFlog�p
        {
            collision.gameObject.GetComponent<BeardFrogCpuAi>().ObstacleCollision(_speedDown, _jumpDown);
        }
        else if (collision.gameObject.layer == 16)//WaterFrog�p
        {
            collision.gameObject.GetComponent<WaterFrogCpuAi>().ObstacleCollision(_speedDown, _jumpDown);
        }
        else if (collision.gameObject.layer == 17)//PridictionFrog�p
        {
            collision.gameObject.GetComponent<PridictionFrogAIScript>().CPUObstacleCollision(_speedDown, _jumpDown);
        }
        //�����܂�

        if (collision.gameObject.tag == "ObstacleJudge")//CPU�̉��p
        {
            collision.gameObject.GetComponent<ObstacleJudgeScript>().AIAvoidance();
        }
    }
}
