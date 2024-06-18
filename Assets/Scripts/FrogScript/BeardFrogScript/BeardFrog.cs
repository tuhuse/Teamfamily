using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeardFrog : MonoBehaviour
{
    [Header("�\�͔��ˈʒu")]
    [SerializeField] private Transform _spawn;
    [Header("�E�����")]
    [SerializeField] private GameObject _beard;
    private bool _isAbility = false;

    private float _coolDownTime = 5f;
    private const float COOLDOWNVALUE = 5f;
    private const float COOLDOWNMULTIPLE = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWait());

    }

    // Update is called once per frame
    void Update()
    {
        if (_isAbility)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Instantiate(_beard, _spawn.position, Quaternion.identity);
                StartCoroutine(CoolDown());

            }
        }
    }
    private IEnumerator CoolDown()
    {
        _isAbility = false;
        yield return new WaitForSeconds(_coolDownTime);
        _isAbility = true;

    }
     private IEnumerator StartWait() {
        yield return new WaitForSeconds(5);
        
        StartCoroutine(CoolDown());//�N�[���^�C��
    }

    private void GetFly() {

        _coolDownTime -= COOLDOWNVALUE * COOLDOWNMULTIPLE;
    }
}
