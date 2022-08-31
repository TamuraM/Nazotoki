using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>タイプライターをクリックしたときに出てくる背景を消すスクリプト</summary>
public class InputTextReturnButton : ButtonBase
{
    [Tooltip("文字入力の後ろにあるパネル")] GameObject _inputTextBackground;
    [Tooltip("入力されたテキスト")] Text _inputText;

    void Start()
    {
        _inputTextBackground = GameObject.Find("InputTextBackground");
        _inputText = GameObject.Find("InputText").GetComponent<Text>();
    }

    public override void Click()
    {
        _inputText.text = "";
        Image.color = ImageColor;
        _inputTextBackground.SetActive(false);
    }
}
