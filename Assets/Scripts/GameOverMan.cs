using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMan : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _cameraSizeAdjust = default;
    //[SerializeField] private float _cameraYSizeAdjust = default;
    [SerializeField] private GameObject _gameOverUI;

    private float _playerFallMin = -30f;
    private float _sizeLimit =  20;
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
                if (_playerObject.transform.position.y < _playerFallMin /*||*/
                            /*_playerObject.transform.position.x < _camera.transform.position.x*/)
                {
                    Debug.Log(_switchNumber);
                    _switchNumber = 2;
                }
                break;
            case 2:

                Debug.Log(_switchNumber);
                Time.timeScale = 0f;
                _switchNumber = 3;

                break;

            case 3:
                Debug.Log(_switchNumber);
                _camera.transform.position -= Vector3.down;

                _camera.GetComponent<Camera>().orthographicSize -= _cameraSizeAdjust;

                if (_camera.GetComponent<Camera>().orthographicSize == _sizeLimit)
                {
                    _switchNumber = 4;
                }

                break;
            case 4:
                Debug.Log(_switchNumber);



                break;

            default:
                break;
        }


        

    }
}
