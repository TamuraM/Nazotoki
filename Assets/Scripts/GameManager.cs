using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>�Q�[���}�l�[�W���[�I�I�I�I�I�I�I�I�I</summary>
public class GameManager : MonoBehaviour
{
    [SerializeField, Header("�������Image"), Tooltip("������������Ă���摜")] GameObject _nazo;
    [SerializeField, Header("���C�g1��MeshRenderer"), Tooltip("1�ڂ̃��C�g�̃��b�V�������_���[")] MeshRenderer _light1;
    [SerializeField, Header("���C�g2��MeshRenderer"), Tooltip("2�ڂ̃��C�g�̃��b�V�������_���[")] MeshRenderer _light2;
    [SerializeField, Header("���C�g3��MeshRenderer"), Tooltip("3�ڂ̃��C�g�̃��b�V�������_���[")] MeshRenderer _light3;
    [SerializeField, Header("�����ĂȂ��}�e���A��"), Tooltip("���C�g���Ђ����ĂȂ��Ƃ��̃}�e���A��")] Material _lightMaterial;
    [SerializeField, Header("����Material"), Tooltip("���C�g�������Ă�}�e���A��")] Material _lightEmission;
    [SerializeField, Header("���o�["), Tooltip("���o�[�̃Q�[���I�u�W�F�N�g")] GameObject _lever;
    [SerializeField, Header("�e�L�X�g�{�b�N�X"), Tooltip("�e�L�X�g�{�b�N�X�̃Q�[���I�u�W�F�N�g")] GameObject _textBox;
    [Tooltip("�w�ʂɂ���F�t���{�^���̃��X�g")] List<string> _button = new();
    [SerializeField, Tooltip("�{�^���𐳂������������Ɍ��郉�C�g�̃��X�g")] List<MeshRenderer> _lights = new(5);
    [Tooltip("���C�g�̂�J�E���g���鐔��")] int _lighting = 0;
    [Tooltip("�{�^���̕�����̔z��")] string[] _colorButton = { "YellowButton", "WhiteButton", "BlueButton", "RedButton", "GreenButton" };
    Clear _clearState;

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

    void Start()
    {
        _clearState = Clear.ClearSitenaiMan;
        _lever.SetActive(false);
        _textBox.SetActive(false);
        _nazo.SetActive(false);
        Transfer();
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log(_clearState);
        }

        //�S���̓��������A�X�e�[�^�X���u���ׂĂ̓���������v�ɂȂ�
        if((_clearState & Clear.FirstStageClear) == Clear.FirstStageClear && (_clearState & Clear.SecondStageClear) == Clear.SecondStageClear && (_clearState & Clear.ThirdStageClear) == Clear.ThirdStageClear)
        {
            _clearState = Clear.AllStageClear;
        }

        
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(_ray, out _hit, 10.0f, 3) && Input.GetMouseButtonDown(0))
        {
            Debug.Log(_hit.collider.gameObject.name);

            //�����ɂ��������@���̏�̎��N���b�N���ē�����ĂȂ񂩂����烌�o�[�����
            if (_hit.collider.gameObject.name == "Nazo")
            {
                Debug.Log("�Ȃ���");
                _nazo.SetActive(true);
            }

            //�^�C�v���C�^�[�N���b�N������A���͉�ʂłĂ���
            if(_hit.collider.gameObject.name == "Typewriter")
            {
                Debug.Log("�^�C�v���C�^�[��");

            }

            if(_hit.collider.gameObject == _lever)
            {
                _clearState |= Clear.FirstStageClear;
            }

            //�w�ʂɂ��������@�N���b�N�������Ԃ������Ă���N���A
            //���C���J��������Ray���΂��āA�I�u�W�F�N�g��T��
            //���N���b�N�����I�u�W�F�N�g��_button���X�g��0�ԖڂƓ����Ȃ烊�X�g���������
            //�S����������N���A
            if (_hit.collider.gameObject.name == _button[0])
            {
                _button.RemoveAt(0);
                Debug.Log(_button.Count);
                _lights[_lighting].material = _lightEmission;

                if(_lighting < 5)
                {
                    _lighting++;
                }

            }
            else if(_hit.collider.gameObject.name != _button[0] && _hit.collider.gameObject.tag == "ColorButton")
            {
                Transfer();
                Debug.Log(_button.Count);
                _lighting = 0;

                foreach(MeshRenderer light in _lights)
                {
                    light.material = _lightMaterial;
                }

            }

            //if(_hit.collider.gameObject.name == "Plane")
            //{
            //    _clearState |= Clear.FirstStageClear;
            //}

            //if (_hit.collider.gameObject.name == "Monitor")
            //{
            //    _clearState |= Clear.ThirdStageClear;
            //}

        }

        //���X�g�̒��g���Ȃ��Ȃ�����A2�ڂ̃��C�g�����点��
        if (_button.Count == 0)
        {
            _button.Add("a");
            _light2.material = _lightEmission;
            _clearState |= Clear.SecondStageClear;
        }

    }

    /// <summary>�{�^�����X�g�ɗv�f������֐�</summary>
    private void Transfer()
    {
        _button.Clear();

        for (int i = 0; i < _colorButton.Length; i++)
        {
            _button.Add(_colorButton[i]);
        }
    }
}
