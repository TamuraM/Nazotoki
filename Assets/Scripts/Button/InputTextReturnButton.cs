using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>タイプライターをクリックしたときに出てくる背景を消すスクリプト</summary>
public class InputTextReturnButton : ButtonBase
{
    [SerializeField, Tooltip("文字入力の後ろにあるパネル")] GameObject _inputTextBackground;
    [SerializeField, Tooltip("入力されたテキスト")] Text _inputText;

    public override void Click()
    {
        _inputText.text = "";
        Image.color = ImageColor;
        _inputTextBackground.SetActive(false);
        GameManager.Instance._isFocused = false;
    }
}
