using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    //�c���̔�������
    private float _timeSpawns;
    //�c���̔������ԊԊu
    [SerializeField] private float _startTimeSpawns;
    //�c���𔭐�����I�u�W�F�N�g
    [SerializeField] GameObject _echoObj;

    // Update is called once per frame
    void Update()
    {
        if(_timeSpawns <= 0)
        {
            Instantiate(_echoObj, transform.position, Quaternion.identity);
            _timeSpawns = _startTimeSpawns;
        }
        else
        {
            _timeSpawns -= Time.deltaTime;
        }
    }
}
