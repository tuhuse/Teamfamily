using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleJudgeScript : MonoBehaviour
{
    // CPU�̏����O�ɏ�Q�����������̔���X�N���v�g
    [Header("�S�t�K�G��")] [SerializeField] GameObject _mucasFrog=null;
    [Header("�E�K�G��")] [SerializeField] GameObject _higeFrog=null;
    [Header("���K�G��")] [SerializeField] GameObject _waterFrog = null;
    [Header("�\�m�K�G��")] [SerializeField] GameObject _pridictionFrog=null;
    // Start is called before the first frame update
   public void AIAvoidance()
    {
       if(_mucasFrog != null)
        {
            print("a");
        }
        else if(_higeFrog != null)
        {
            print("b");
        }
       else if(_waterFrog != null)
        {
            print("c");
        }
       else if(_pridictionFrog != null)
        {
            GetComponentInParent<PridictionFrogAIScript>().DistanceCalculation();
        }
    }
}
