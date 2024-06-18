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
        if (Input.GetKeyDown(KeyCode.Return)&&_isCoolDown==false)//アビリティ発動
        {
            _isCoolDown = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 255);
            _contScri._isinvincible = true;
            StartCoroutine(AbilityStop());
        }
        else if (Input.GetKeyDown(KeyCode.Return) && _isCoolDown == true)
        {
            //クールダウン中
            GetFlies();
        }

    }

    private IEnumerator AbilityStop()
    {
        yield return new WaitForSeconds(2f);//アビリティ終了
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        _contScri._isinvincible = false;
        StartCoroutine(CoolDownOut());
    }

    private IEnumerator CoolDownOut()//クールダウン解除
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
