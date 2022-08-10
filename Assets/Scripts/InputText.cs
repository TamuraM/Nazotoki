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
    }

    void Update()
    {
        
    }

    private char Input(char )
}
