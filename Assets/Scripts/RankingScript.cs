using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RankingScript : MonoBehaviour
{
    [Header("カメラ")] [SerializeField] GameObject _camera=default;//カメラ入れ
    [SerializeField]  GameObject[] _ranking; //プレイヤーのランキング
    public GameObject _first = default; //１位のプレイヤー
    private int _rankingValue = 0;

    private bool _isGameStart = false;

    private float _playerDistance=default; 
    private const float CAMPOSIPLUSX = 16f;
    private const float CAMPOSIY = 20f;
    private const float CAMPOSIZ = -10;
    private const float RASTPLAYER = 4;
   

    // Start is called before the first frame update
    void Start()
    {
        Array.Resize(ref _ranking, 4);
    }
    // Update is called once per frame
    void Update()
    {
        if (_isGameStart)
        {
            //現在１位のプレイヤーと場所を比較
            if (_playerDistance >= Mathf.Abs(_ranking[_rankingValue].transform.position.x - this.transform.position.x))
            {
                _playerDistance = Mathf.Abs(_ranking[_rankingValue].transform.position.x - this.transform.position.x);
                _first = _ranking[_rankingValue];
            }
            _rankingValue++;

            if (_playerDistance >= Mathf.Abs(_ranking[_rankingValue].transform.position.x - this.transform.position.x))
            {
                _playerDistance = Mathf.Abs(_ranking[_rankingValue].transform.position.x - this.transform.position.x);
                _first = _ranking[_rankingValue];
            }
            _rankingValue++;

            if (_playerDistance >= Mathf.Abs(_ranking[_rankingValue].transform.position.x - this.transform.position.x))
            {
                _playerDistance = Mathf.Abs(_ranking[_rankingValue].transform.position.x - this.transform.position.x);
                _first = _ranking[_rankingValue];
            }
            _rankingValue++;

            if (_playerDistance >= Mathf.Abs(_ranking[_rankingValue].transform.position.x - this.transform.position.x))
            {
                _playerDistance = Mathf.Abs(_ranking[_rankingValue].transform.position.x - this.transform.position.x);
                _first = _ranking[_rankingValue];
            }
            //ここまで

            //１位のプレイヤーをカメラで追う
            _rankingValue = 0;

            if (_first.transform.position.y >= CAMPOSIY - 0.1) //プレイヤーのＹ軸が一定の高さまで行ったらプレイヤーのＹ軸に合わせる
            {
                _camera.transform.position = new Vector3(_first.transform.position.x + CAMPOSIPLUSX, _first.transform.position.y, CAMPOSIZ);
            }
            else
            {

                _camera.transform.position = new Vector3(_first.transform.position.x + CAMPOSIPLUSX, CAMPOSIY, CAMPOSIZ);
            }
        }
    }

    public void SceneStart(GameObject player)
    {
        _ranking[_rankingValue] = player;
        _rankingValue++;

        if (_rankingValue == RASTPLAYER&&!_isGameStart)
        {
            _rankingValue = 0;
            _playerDistance = Mathf.Abs(_ranking[_rankingValue].transform.position.x - this.transform.position.x);
            _first = _ranking[_rankingValue];
            _isGameStart = true;
           
        }
    }
}
