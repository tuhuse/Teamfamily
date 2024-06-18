using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mucus : MonoBehaviour
{
    private Animator _mucus;
    private Rigidbody2D _rb;
    [SerializeField] private float _movemucus;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mucus = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    { //����
        _rb.velocity = (Vector2.right * _movemucus*2) + (Vector2.down * _movemucus);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Flor"))
        {
            _movemucus = 0;
            _mucus.SetBool("Start",true);

        }
    }
}
