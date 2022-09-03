using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

/// <summary>�Q�[���}�l�[�W���[�I�I�I�I�I�I�I�I�I</summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //�h�A�̉��ɂ��郉�C�g
    [SerializeField, Header("���C�g1��MeshRenderer"), Tooltip("1�ڂ̃��C�g�̃��b�V�������_���[")] MeshRenderer _light1;
    [SerializeField, Header("���C�g2��MeshRenderer"), Tooltip("2�ڂ̃��C�g�̃��b�V�������_���[")] MeshRenderer _light2;
    [SerializeField, Header("���C�g3��MeshRenderer"), Tooltip("3�ڂ̃��C�g�̃��b�V�������_���[")] MeshRenderer _light3;
    [SerializeField, Header("�����ĂȂ��}�e���A��"), Tooltip("���C�g���Ђ����ĂȂ��Ƃ��̃}�e���A��")] Material _lightMaterial;
    [SerializeField, Header("����Material"), Tooltip("���C�g�������Ă�}�e���A��")] Material _lightEmission;

    //�����̓�֌W
    [SerializeField, Header("��̔w�i"), Tooltip("������������Ă���摜")] GameObject _nazo;
    [SerializeField, Header("���͂���e�L�X�gGameObject"), Tooltip("�^�C�v���C�^�[���N���b�N�����Ƃ��ɏo�Ă�����͉�ʃp�l��")] GameObject _inputText;
    [SerializeField, Header("�{�^��"), Tooltip("�{�^���̃Q�[���I�u�W�F�N�g")] GameObject _button;

    //��둤�̓�֌W
    [Tooltip("�w�ʂɂ���F�t���{�^���̃��X�g")] List<string> _colorButtons = new();
    [Tooltip("�{�^���̔z�� �������Ԃɖ��O�������Ă�")] //���A���A�A�ԁA��
    string[] _colorButton = { "YellowButton", "WhiteButton", "BlueButton", "RedButton", "GreenButton" };
    [SerializeField, Tooltip("�{�^���𐳂������������Ɍ��郉�C�g�̃��X�g")] List<MeshRenderer> _lights = new(5);
    [Tooltip("���C�g�̂�J�E���g���鐔��")] int _lighting = 0;

    //�E���̓�֌W
    [SerializeField, Header("���j�^�[�̉摜")] SpriteRenderer _monitor;
    [SerializeField, Header("��̉摜�������X�g")] Sprite[] _questions = new Sprite[3];
    [SerializeField, Header("�{�^�����������Ԃɓ����")] GameObject[] _spriteButton = new GameObject[3];
    [Tooltip("�����{�^����List")] List<GameObject> _spriteButtons = new();
    [SerializeField, Header("�܂�̉摜")] Sprite _circle; //�����̎�
    [SerializeField, Header("�΂̉摜")] Sprite _cross; //�s�����̎�
    [Tooltip("������ڂ�")] int _clearSprite = 0;

    //�h�A�̓�֌W
    [SerializeField, Header("�h�A�̓�")] GameObject _lastNazo;
    [SerializeField, Header("�e���L�[")] GameObject _numberKey;
    [SerializeField, Header("�h�A�̓�̔w�i")] GameObject _lastNazoBackground;
    [SerializeField, Header("�ԍ����͉��")] GameObject _numberKeyBackground;

    public bool _isFocused;

    /// <summary>�Q�[�����̏��</summary>
    public enum GameMode
    {
        Title, //�^�C�g�����
        Opening, //�I�[�v�j���O��
        PlayGame, //�Q�[���v���C��
        Thinking, //�q���g���l���Ă�
        GameClear, //�E�o�����̃G���f�B���O
        GameOver, //���Ԑ؂�̃G���f�B���O
    }

    public GameMode _gameMode;

    /// <summary>������̐i�s�x</summary>
    public enum Clear
    {
        ClearSitenaiMan = 0,
        /// <summary>�ŏ��̓������</summary>
        FirstStageClear = 1 << 0,
        /// <summary>��Ԗڂ̓������</summary>
        SecondStageClear = 1 << 1,
        /// <summary>�O�Ԗڂ̓������</summary>
        ThirdStageClear = 1 << 2,
        /// <summary>���ׂĂ̓�����āA�Ō�̓�o��</summary>
        LastStageStart = 1 << 3,
        /// <summary>�Ō�̓������</summary>
        LastStageClear = 1 << 4,
    }

    public Clear _clearState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _clearState = Clear.ClearSitenaiMan;
        _gameMode = GameMode.Title;

        _button.SetActive(false);
        _nazo.SetActive(false);
        _inputText.SetActive(false);
        _lastNazo.SetActive(false);
        _numberKey.SetActive(false);
        _lastNazoBackground.SetActive(false);
        _numberKeyBackground.SetActive(false);

        _colorButtons = _colorButton.ToList();
        _spriteButtons = _spriteButton.ToList();
        _monitor.sprite = _questions[_clearSprite];
    }


    void Update()
    {
        //�E�N���b�N�Ō��݂̃N���A�󋵂��m�F
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log(_clearState);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (_gameMode == GameMode.PlayGame)
        {
            if (Physics.Raycast(ray, out hit, 10.0f, 3) && Input.GetMouseButtonDown(0))
            {
                Debug.Log(hit.collider.gameObject.name);

                if((_clearState & Clear.FirstStageClear) != Clear.FirstStageClear)
                {
                    //----------�����ɂ�������----------
                    //���̏�̎��N���b�N������䂪�g�傳���
                    if (hit.collider.gameObject.name == "Nazo" && !_isFocused)
                    {
                        Debug.Log("�Ȃ���");
                        _nazo.SetActive(true);
                        _isFocused = true; ;
                    }

                    //�^�C�v���C�^�[���N���b�N��������͉�ʂ��o�Ă���
                    if (hit.collider.gameObject.name == "Typewriter" && !_isFocused)
                    {
                        Debug.Log("�^�C�v���C�^�[��");
                        _inputText.SetActive(true);
                        _isFocused = true; ;
                    }

                    //�{�^�����N���b�N������N���A
                    if (hit.collider.gameObject == _button)
                    {
                        _light1.material = _lightEmission;
                        _clearState |= Clear.FirstStageClear;
                    }
                    //----------�����܂�----------
                }

                if((_clearState & Clear.SecondStageClear) != Clear.SecondStageClear)
                {
                    //----------�w�ʂɂ�������----------�@�N���b�N�������Ԃ������Ă���N���A
                    if (hit.collider.gameObject.name == _colorButtons[0])
                    {
                        //�����̃{�^������������A��ɂ��郉�C�g�����Ԃɓ_��
                        _colorButtons.RemoveAt(0);
                        //Debug.Log(_colorButtons.Count);
                        _lights[_lighting].material = _lightEmission;

                        if (_lighting < 5)
                        {
                            _lighting++;
                        }

                    }
                    else if (hit.collider.gameObject.name != _colorButtons[0] && hit.collider.gameObject.tag == "ColorButton")
                    {
                        //�Ԉ�����{�^������������A��ɂ��郉�C�g���S��������
                        _colorButtons = _colorButton.ToList();
                        Debug.Log(_colorButtons.Count);
                        _lighting = 0;
                        _lights.ForEach(light => light.material = _lightMaterial);
                    }

                    //�N���A������A2�ڂ̃��C�g�����点��
                    if (_colorButtons.Count == 0)
                    {
                        _colorButtons.Add("a");
                        _light2.material = _lightEmission;
                        _clearState |= Clear.SecondStageClear;
                    }
                    //----------�����܂�----------
                }

                if((_clearState & Clear.ThirdStageClear) != Clear.ThirdStageClear)
                {
                    //----------�E���ɂ�������----------�@�摜�ɑΉ������{�^�������Ԃɉ�������N���A
                    if (hit.collider.gameObject == _spriteButtons[0])
                    {
                        //����������܂�̉摜���łĂ��āA���̖�肪�\�������
                        _clearSprite++;
                        _spriteButtons.RemoveAt(0);
                        StartCoroutine(SpriteQuestionSucces());
                    }
                    else
                    {
                        _clearSprite = 0;
                        _spriteButtons = _spriteButton.ToList();
                        StartCoroutine(SpriteQuestionWrong());
                    }

                    if (_spriteButtons.Count == 0)
                    {
                        _spriteButtons.Add(this.gameObject);
                        _light3.material = _lightEmission;
                        _clearState |= Clear.ThirdStageClear;
                    }
                    //----------�����܂�----------
                }


                //�S���̓��������A�Ō�̓䂪�o��
                if ((_clearState & Clear.FirstStageClear) == Clear.FirstStageClear && (_clearState & Clear.SecondStageClear) == Clear.SecondStageClear && (_clearState & Clear.ThirdStageClear) == Clear.ThirdStageClear)
                {
                    _clearState = Clear.LastStageStart;
                }


                //----------�h�A�ɂ�������----------�@�S�␳��������o�Ă���@�����̔ԍ���ł����߂΃N���A
                if (_clearState == Clear.LastStageStart)
                {
                    _lastNazo.SetActive(true);
                    _numberKey.SetActive(true);
                }

                if (hit.collider.gameObject == _lastNazo && !_isFocused)
                {
                    _lastNazoBackground.SetActive(true);
                    _isFocused = true;
                }

                if (hit.collider.gameObject == _numberKey && !_isFocused)
                {
                    _numberKeyBackground.SetActive(true);
                    _isFocused = true;
                }
                //----------�����܂�----------

            }

        }



    }

    IEnumerator SpriteQuestionSucces()
    {
        _monitor.sprite = _circle;
        yield return new WaitForSeconds(1.0f);
        _monitor.sprite = _questions[_clearSprite];
    }

    IEnumerator SpriteQuestionWrong()
    {
        _monitor.sprite = _cross;
        yield return new WaitForSeconds(1.0f);
        _monitor.sprite = _questions[_clearSprite];
    }
}
