using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>タイプライターで文字を入力するためのスクリプト</summary>
public class InputText : MonoBehaviour
{
    [Tooltip("答えを入力するテキスト")] Text _inputText;

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

    /// <summary>テキストに文字を入力する関数</summary>
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
