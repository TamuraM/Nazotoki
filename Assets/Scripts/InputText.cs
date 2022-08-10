using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�^�C�v���C�^�[�ŕ�������͂��邽�߂̃X�N���v�g</summary>
public class InputText : MonoBehaviour
{
    [Tooltip("��������͂���e�L�X�g")] Text _inputText;

    void Start()
    {
        _inputText = GetComponent<Text>();
        _inputText.text = "";
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            InputChar('A');
        }

        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            Delete();
        }
    }

    /// <summary>�e�L�X�g�ɕ�������͂���֐�</summary>
    /// <param name="a"></param>
    private void InputChar(char a)
    {
        string nowText = _inputText.text;
        _inputText.text = $"{nowText}{a}";
    }

    private void Delete()
    {
        _inputText.text = $"{_inputText.text.Substring(0, _inputText.text.Length - 1)}";
    }
}
