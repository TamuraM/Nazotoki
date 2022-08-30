using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>タイプライターをクリックしたときに出てくる背景を消すスクリプト</summary>
public class InputTextReturnButton : ButtonBase
{
    [Tooltip("文字入力の後ろにあるパネル")] GameObject _inputTextBackground;

    void Start()
    {
        _inputTextBackground = GameObject.Find("InputTextBackground");
    }

    public override void Click()
    {
        Image.color = ImageColor;
        _inputTextBackground.SetActive(false);
    }
}
