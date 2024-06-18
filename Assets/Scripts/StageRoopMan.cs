using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRoopMan : MonoBehaviour {
    [SerializeField] private List<GameObject> _prefabs = new List<GameObject>();
    [SerializeField] private GameObject _playerObjct=null;
    [SerializeField] private GameObject _stageParents;

    //ステージの移動距離定数
    [SerializeField] private float _stageXPosition = default;
    //ステージの現在位置
    [SerializeField] private float _stageNowPosition = default;

    //配列番号
    public int _enemynumber =default;
    public int _choosenumber = default;
    private int _arrayNumber = 10;
    private int _randomMax = default;
    private int _randomMin = 0;
    private int _playernumbers=default;
    private float _playerNowposition = default;
    public bool _isRoop = false;
    // Start is called before the first frame update
    void Start() {
        
    }                                                                                                                                                                                         

    // Update is called once per frame
    void Update() {
        if (_playerObjct == null) {

            _playerObjct = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RankingScript>()._first;
            
        } 
         if(_playerObjct!=null)
            {
            _playerNowposition = _playerObjct.transform.position.x;
            _playerObjct = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RankingScript>()._first;
            
        }
      
     
        //ランダムの値を保持
        //プレイヤーが先頭のひとつ前のステージについたらランダムに続きを移動
        if (_playerNowposition >= _stageNowPosition) {
            //現在の最後尾を避けて配列数を取得
            _randomMax = _prefabs.Count - 2;

            _arrayNumber = Random.Range(_randomMin, _randomMax);
            //idou
            _stageNowPosition += _stageXPosition;

            _isRoop = true;

            _prefabs[_arrayNumber].transform.position =
                new Vector2(_stageNowPosition, _prefabs[_arrayNumber].transform.position.y);
            
            //移動したステージを配列の末尾に追加し、行を詰める
            _prefabs.Add(_prefabs[_arrayNumber]);
            _prefabs.Remove(_prefabs[_arrayNumber]);
           

        } 
        else
        {
            _isRoop = false;
        }
        Enemynumber();
        Choosenumber();

    }
    private void Enemynumber() {
        switch (_arrayNumber) {
            case 0:
                _enemynumber = default;
                break;
            case 1:
                _enemynumber = 6;
                break;
            case 2:
                _enemynumber = 5;
                break;
            case 3:
                _enemynumber = default;
                break;
            case 4:
                _enemynumber = 3;
                break;
            case 5:
                _enemynumber = default;
                break;
            case 6:
                _enemynumber = 2;
                break;
            case 7:
                _enemynumber = 1;
                break;
            case 8:
                _enemynumber = 0;
                break;
            case 9:
                _enemynumber = default;
                break;
           
          
        }

    } private void Choosenumber() {
        switch (_arrayNumber) {
            case 0:
                _choosenumber = 4;
                break;
            case 1:
                 _choosenumber = default;
                break;
            case 2:
                _choosenumber = 8
                    ;
                break;
            case 3:
                _choosenumber = 9;
                break;
            case 4:
                _choosenumber = default;
                break;
            case 5:
                _choosenumber = default;
                break;
            case 6:
                _choosenumber = default;
                break;
            case 7:
                _choosenumber = default;
                break;
            case 8:
                _choosenumber = 12;
                break;
            case 9:
                _choosenumber = 14;
                break;

          
        }

    }

}
