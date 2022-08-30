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

    Clear _clearState;

    [SerializeField, Header("���C�g1��MeshRenderer"), Tooltip("1�ڂ̃��C�g�̃��b�V�������_���[")] MeshRenderer _light1;
    [SerializeField, Header("���C�g2��MeshRenderer"), Tooltip("2�ڂ̃��C�g�̃��b�V�������_���[")] MeshRenderer _light2;
    [SerializeField, Header("���C�g3��MeshRenderer"), Tooltip("3�ڂ̃��C�g�̃��b�V�������_���[")] MeshRenderer _light3;
    [SerializeField, Header("�����ĂȂ��}�e���A��"), Tooltip("���C�g���Ђ����ĂȂ��Ƃ��̃}�e���A��")] Material _lightMaterial;
    [SerializeField, Header("����Material"), Tooltip("���C�g�������Ă�}�e���A��")] Material _lightEmission;

    [SerializeField, Header("�������Image"), Tooltip("������������Ă���摜")] GameObject _nazo;
    [SerializeField, Header("���͂���e�L�X�gGameObject"), Tooltip("�^�C�v���C�^�[���N���b�N�����Ƃ��ɏo�Ă�����͉�ʃp�l��")] GameObject _inputText;
    [SerializeField, Header("�{�^��"), Tooltip("�{�^���̃Q�[���I�u�W�F�N�g")] GameObject _button;

    [Tooltip("�w�ʂɂ���F�t���{�^���̃��X�g")] List<string> _buttons = new();
    [Tooltip("�{�^���̔z�� �������Ԃɖ��O�������Ă�")] //���A���A�A�ԁA��
    string[] _colorButton = { "YellowButton", "WhiteButton", "BlueButton", "RedButton", "GreenButton" };
    [SerializeField, Tooltip("�{�^���𐳂������������Ɍ��郉�C�g�̃��X�g")] List<MeshRenderer> _lights = new(5);
    [Tooltip("���C�g�̂�J�E���g���鐔��")] int _lighting = 0;

    [Tooltip("�I�u�W�F�N�g�ɐG��鎞���ǂ���")] bool _inGame;
    public bool InGame { get => _inGame; set => _inGame = value; }

    /// <summary>������̐i�s�x</summary>
    enum Clear
    {
        ClearSitenaiMan = 0,
        /// <summary>�ŏ��̓������</summary>
        FirstStageClear = 1 << 0,
        /// <summary>��Ԗڂ̓������</summary>
        SecondStageClear = 1 << 1,
        /// <summary>�O�Ԗڂ̓������</summary>
        ThirdStageClear = 1 << 2,
        /// <summary>���ׂĂ̓������</summary>
        AllStageClear = 1 << 3, 
    }

    private void Awake()
    {
        if(instance == null)
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
        _button.SetActive(false);
        _nazo.SetActive(false);
        _inputText.SetActive(false);
        _buttons = _colorButton.ToList();
    }

    
    void Update()
    {
        //�E�N���b�N�Ō��݂̃N���A�󋵂��m�F
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log(_clearState);
        }
        
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if(_inGame)
        {
            if (Physics.Raycast(_ray, out _hit, 10.0f, 3) && Input.GetMouseButtonDown(0))
            {
                //Debug.Log(_hit.collider.gameObject.name);

                if((_clearState & Clear.FirstStageClear) != Clear.FirstStageClear)
                {

                    //�����ɂ��������@���̏�̎��N���b�N���ē�����ĂȂ񂩂����烌�o�[�����
                    switch (_hit.collider.gameObject.name)
                    {
                        //����N���b�N������g��
                        case "Nazo":
                            Debug.Log("�Ȃ���");
                            _nazo.SetActive(true);
                            break;
                        //�^�C�v���C�^�[���N���b�N��������͉�ʂłĂ���
                        case "Typewriter":
                            Debug.Log("�^�C�v���C�^�[��");
                            _inputText.SetActive(true);
                            break;
                        //�{�^����������N���A�@���C�g����
                        case "FirstButton":
                            _light2.material = _lightEmission;
                            _clearState |= Clear.FirstStageClear;
                            break;
                        default: return;
                    }
 
                }
                
                if((_clearState & Clear.SecondStageClear) == Clear.SecondStageClear)
                {

                    //�w�ʂɂ��������@�N���b�N�������Ԃ������Ă���N���A
                    if (_hit.collider.gameObject.name == _buttons[0])
                    {
                        //�����̃{�^������������A��ɂ��郉�C�g�����Ԃɓ_��
                        _buttons.RemoveAt(0);
                        Debug.Log(_buttons.Count);
                        _lights[_lighting].material = _lightEmission;
                        _lighting = _lighting < 5 ? _lighting++ : _lighting;
                    }
                    else if (_hit.collider.gameObject.name != _buttons[0] && _hit.collider.gameObject.tag == "ColorButton")
                    {
                        //�Ԉ�����{�^������������A��ɂ��郉�C�g���S��������
                        _buttons = _colorButton.ToList();
                        Debug.Log(_buttons.Count);
                        _lighting = 0;
                        _lights.ForEach(light => light.material = _lightMaterial);
                    }

                    //�N���A������A2�ڂ̃��C�g�����点��
                    if (_buttons.Count == 0)
                    {
                        _buttons.Add("a");
                        _light2.material = _lightEmission;
                        _clearState |= Clear.SecondStageClear;
                    }

                }
                

            }

        }

        //�S���̓��������A�X�e�[�^�X���u���ׂĂ̓���������v�ɂȂ�
        if ((_clearState & Clear.FirstStageClear) == Clear.FirstStageClear && (_clearState & Clear.SecondStageClear) == Clear.SecondStageClear && (_clearState & Clear.ThirdStageClear) == Clear.ThirdStageClear)
        {
            _clearState = Clear.AllStageClear;
        }

    }

}
