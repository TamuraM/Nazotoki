using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>�Q�[���}�l�[�W���[�I�I�I�I�I�I�I�I�I</summary>
public class GameManager : MonoBehaviour
{
    [SerializeField, Header("���C�g1��MeshRenderer"), Tooltip("1�ڂ̃��C�g�̃��b�V�������_���[")] MeshRenderer _light1;
    [SerializeField, Header("���C�g2��MeshRenderer"), Tooltip("2�ڂ̃��C�g�̃��b�V�������_���[")] MeshRenderer _light2;
    [SerializeField, Header("���C�g3��MeshRenderer"), Tooltip("3�ڂ̃��C�g�̃��b�V�������_���[")] MeshRenderer _light3;
    [SerializeField, Header("����Material"), Tooltip("���C�g�������Ă�}�e���A��")] Material _lightEmission;
    [SerializeField, Header("���o�["), Tooltip("���o�[�̃Q�[���I�u�W�F�N�g")] GameObject _lever;
    [SerializeField, Header("�e�L�X�g�{�b�N�X"), Tooltip("�e�L�X�g�{�b�N�X�̃Q�[���I�u�W�F�N�g")] GameObject _textBox;
    [Tooltip("�w�ʂɂ���F�t���{�^���̃��X�g")] List<string> _button = new List<string>();
    [Tooltip("�{�^���̕�����̔z��")] string[] _colorButton = { "YellowButton", "WhiteButton", "BlueButton", "RedButton", "GreenButton" };
    Clear _clearState;

    /// <summary>������̐i�s�x</summary>
    enum Clear
    {
        /// <summary>�ŏ��̓������</summary>
        FirstStageClear,
        /// <summary>��Ԗڂ̓������</summary>
        SecondStageClear,
        /// <summary>�O�Ԗڂ̓������</summary>
        ThirdStageClear,
    }

    void Start()
    {
        _lever.SetActive(false);
        _textBox.SetActive(false);
        Transfer();
    }

    
    void Update()
    {
        //�w�ʂɂ��������@�N���b�N�������Ԃ������Ă���N���A
        //���C���J��������Ray���΂��āA�I�u�W�F�N�g��T��
        //���N���b�N�����I�u�W�F�N�g��_button���X�g��0�ԖڂƓ����Ȃ烊�X�g���������
        //�S����������N���A
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if (Physics.Raycast(_ray, out _hit, 10.0f, 3) && Input.GetMouseButtonDown(0))
        {
            Debug.Log(_hit.collider.gameObject.name);

            if (_hit.collider.gameObject.name == _button[0])
            {
                _button.RemoveAt(0);
                Debug.Log(_button.Count);
            }
            else if(_hit.collider.gameObject.name != _button[0] && _hit.collider.gameObject.tag == "ColorButton")
            {
                Transfer();
                Debug.Log(_button.Count);
            }
        }

        //���X�g�̒��g���Ȃ��Ȃ�����A2�ڂ̃��C�g�����点��
        if (_button.Count == 0)
        {
            _light2.material = _lightEmission;
            _clearState = Clear.SecondStageClear;
        }
    }

    private void Transfer()
    {
        _button.Clear();

        for (int i = 0; i < _colorButton.Length; i++)
        {
            _button.Add(_colorButton[i]);
        }
    }
}
