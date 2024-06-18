using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    private int _selectCharacterNumber = default;
    [SerializeField] private GameObject[] _readyTxtCanvas;
    //��������J�G��
    [SerializeField] private GameObject[] _frog; 
    //��������J�G���̏ꏊ
    [SerializeField] private Transform[] _frogspawn;
    private AudioSource _audiosource;
    [SerializeField]
    private AudioClip[] _audioclip;
    private bool _tukure = false;
   public int _playernumber=0;
    private RankingScript _rank;
    private GameObject _first;
    ////[SerializeField] private GameObject[] _flogButtons; f
    // Start is called before the first frame update
    void Start()
    {
        _audiosource = GetComponent<AudioSource>();
       _rank= GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RankingScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
   
    public void GoTxt()//GoButton���������烁�C����ʂɍs��
    {
        _audiosource.PlayOneShot(_audioclip[2]);//�ǉ�
        _tukure = true;
        _readyTxtCanvas[1].SetActive(false);
        _playernumber = _selectCharacterNumber;
        if (_tukure) {
            StartCoroutine(Ko());
        }
        print(_playernumber);
    }
    public void ReturnButton()//ReturnButton����������O�̉�ʂɖ߂�
    {
        _readyTxtCanvas[0].SetActive(true);
        _readyTxtCanvas[1].SetActive(false);
        _audiosource.PlayOneShot(_audioclip[1]);//�ǉ�
    }
    public void ReadyTxt()//�L�����N�^�[�ύX���̒l�ƃL�����N�^�[Canvas��false�ɂ���
    {
        _audiosource.PlayOneShot(_audioclip[0]);//�ǉ�
        _readyTxtCanvas[0].SetActive(false);
        _readyTxtCanvas[1].SetActive(true);
        // �L�����N�^�[���I�����ꂽ���� _selectCharacterNumber ��ύX����
        switch (_selectCharacterNumber)
        {
            case 0:
                Chara1Selected();
                break;
            case 1:
                Chara2Selected();
                break;
            case 2:
                Chara3Selected();
                break;
            case 3:
                Chara4Selected();
                break;
            default:
                break;
        }
    }
   

    public void Chara1Selected()
    {
        
        _selectCharacterNumber = 0;
        
      
    }

    public void Chara2Selected()
    {
        _selectCharacterNumber = 1;
        
    }

    public void Chara3Selected()
    {
        _selectCharacterNumber = 2;
       
    }

    public void Chara4Selected()
    {
        _selectCharacterNumber = 3;
        
    }
    private IEnumerator Ko() {
        _tukure = false;
        yield return new WaitForSeconds(2);
        switch (_playernumber) {
            case 0:
             _first=   Instantiate(_frog[0], _frogspawn[0].position, Quaternion.identity);
                _rank.SceneStart(_first);
                _first = Instantiate(_frog[5], _frogspawn[1].position, Quaternion.identity);
                _rank.SceneStart(_first );
                _first = Instantiate(_frog[6], _frogspawn[2].position, Quaternion.identity);
                _rank.SceneStart(_first );
                _first = Instantiate(_frog[7], _frogspawn[3].position, Quaternion.identity);
                _rank.SceneStart(_first );
                break;  
            case 1:
                _first = Instantiate(_frog[1], _frogspawn[1].position, Quaternion.identity);
                _rank.SceneStart(_first );
                _first = Instantiate(_frog[4], _frogspawn[0].position, Quaternion.identity);
                _rank.SceneStart(_first );
                _first = Instantiate(_frog[6], _frogspawn[2].position, Quaternion.identity);
                _rank.SceneStart(_first );
                _first = Instantiate(_frog[7], _frogspawn[3].position, Quaternion.identity);
                _rank.SceneStart(_first );
                break; 
            case 2:
                _first = Instantiate(_frog[2], _frogspawn[2].position, Quaternion.identity);
                _rank.SceneStart(_first );
                _first = Instantiate(_frog[4], _frogspawn[0].position, Quaternion.identity);
                _rank.SceneStart(_first );
                _first = Instantiate(_frog[5], _frogspawn[1].position, Quaternion.identity);
                _rank.SceneStart(_first );
                _first = Instantiate(_frog[7], _frogspawn[3].position, Quaternion.identity);
                _rank.SceneStart(_first );
                break; 
            case 3:
                _first = Instantiate(_frog[3], _frogspawn[3].position, Quaternion.identity);
                _rank.SceneStart(_first );
                _first = Instantiate(_frog[4], _frogspawn[0].position, Quaternion.identity);
                _rank.SceneStart(_first );
                _first = Instantiate(_frog[5], _frogspawn[1].position, Quaternion.identity);
                _rank.SceneStart(_first );
                _first = Instantiate(_frog[6], _frogspawn[2].position, Quaternion.identity);
                _rank.SceneStart(_first );
                break;
        }

      
        
        yield return new WaitForSeconds(60);
    }
        

   
}
