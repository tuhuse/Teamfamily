using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMan : MonoBehaviour
{
    [SerializeField] private GameObject _returnButton;
    [SerializeField] private GameObject _moveTitleButton;
    private int _switchNumber = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        switch (_switchNumber)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.T))
                {
                    Time.timeScale = 0f;
                    _switchNumber = 2;
                }

                break;
            case 2:

                if (Input.GetKeyUp(KeyCode.T))
                {
                    _switchNumber = 3;
                }
                break;

            case 3:


                if(Input.GetKeyDown(KeyCode.T))
                {
                    Time.timeScale = 1f;
                    _switchNumber = 1;
                }

                break;
            case 4:
                { 
                
                }
                break;
            default:
                break;
        }
    }
}
