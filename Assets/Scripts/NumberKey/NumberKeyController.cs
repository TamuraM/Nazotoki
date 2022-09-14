using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>テンキーからの入力を受け取って、テキスト表示・正誤判定をする</summary>
public class NumberKeyController : MonoBehaviour
{
    [SerializeField, Header("入力された数字を表示する")] Text _inputNumber;
    [Tooltip("入力された数字")] string _num = "";
    public string Num { get => _num; set => _num = value; }
    [Tooltip("答えの数字")] string _answer = "2965";
    public string Answer { get => _answer; }

    private void Update()
    {
        _inputNumber.text = _num;
    }
}
