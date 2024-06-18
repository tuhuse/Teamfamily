using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterExplain : MonoBehaviour
{   
    [SerializeField] private GameObject _txt;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter()
    {
        _txt.SetActive(true);
    }
    public void OnPointerExit()
    {
        _txt.SetActive(false);
    }
    public void OnPointerClick()
    {
        
        _txt.SetActive(false);
    }
}
