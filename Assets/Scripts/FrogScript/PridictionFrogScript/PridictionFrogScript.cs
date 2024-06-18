using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PridictionFrogScript : MonoBehaviour
{
    private PridictionFrogControll _contScri;
    private bool _isCoolDown = false;
    private float _coolDownTime = 20f;
    private const float COOLDOWNVALUE = 20;
    // Start is called before the first frame update
    void Start()
    {
        _contScri = GetComponent<PridictionFrogControll>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)&&_isCoolDown==false)//�A�r���e�B����
        {
            _isCoolDown = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 255);
            _contScri._isinvincible = true;
            StartCoroutine(AbilityStop());
        }
        else if (Input.GetKeyDown(KeyCode.Return) && _isCoolDown == true)
        {
            //�N�[���_�E����
            GetFlies();
        }

    }

    private IEnumerator AbilityStop()
    {
        yield return new WaitForSeconds(2f);//�A�r���e�B�I��
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        _contScri._isinvincible = false;
        StartCoroutine(CoolDownOut());
    }

    private IEnumerator CoolDownOut()//�N�[���_�E������
    {
        yield return new WaitForSeconds(_coolDownTime);
        _isCoolDown = false;
        print("a");
    }

    public void GetFlies()
    {
        _coolDownTime -= COOLDOWNVALUE*0.1f;
        print(_coolDownTime);
    }
}
