using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MucusFrog : MonoBehaviour
{
    [Header("能力発射位置")]
    [SerializeField] private Transform _spawn;
    [Header("粘液入れて")]
    [SerializeField] private GameObject _mucus;
    private bool _isAbility = false;
   
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
                Instantiate(_mucus, _spawn.position, Quaternion.identity);
                StartCoroutine(CoolDown());

            }
        }



    }
    private IEnumerator CoolDown()
    {
        _isAbility = false;
        yield return new WaitForSeconds(5);
        _isAbility = true;

    }
    private IEnumerator StartWait() {
        yield return new WaitForSeconds(5);
       
        StartCoroutine(CoolDown());//クールタイム
    }
}
