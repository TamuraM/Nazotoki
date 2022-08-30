using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>なぞをクリックしたときにでてくる背景を消すスクリプト
public class NazoReturnButton : ButtonBase
{
    [Tooltip("謎の後ろにあるパネル")] GameObject _nazoBackground;
    [Tooltip("入力されたテキスト")] Text _inputText;

    void Start()
    {
        _nazoBackground = GameObject.Find("NazoBackground");
        _inputText = GameObject.Find("InputText").GetComponent<Text>();
    }

    public override void Click()
    {
        //画像の透明度を戻して、背景を消す
        _inputText.text = "";
        Image.color = ImageColor;
        _nazoBackground.SetActive(false);
    }
}
