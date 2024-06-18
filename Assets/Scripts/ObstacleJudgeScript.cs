using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleJudgeScript : MonoBehaviour
{
    // CPU‚Ì­‚µ‘O‚ÉáŠQ•¨‚ª—ˆ‚½‚Ì”»’èƒXƒNƒŠƒvƒg
    [Header("”S‰tƒKƒGƒ‹")] [SerializeField] GameObject _mucasFrog=null;
    [Header("•EƒKƒGƒ‹")] [SerializeField] GameObject _higeFrog=null;
    [Header("…ƒKƒGƒ‹")] [SerializeField] GameObject _waterFrog = null;
    [Header("—\’mƒKƒGƒ‹")] [SerializeField] GameObject _pridictionFrog=null;
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
