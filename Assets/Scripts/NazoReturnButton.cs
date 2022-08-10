using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>なぞをクリックしたときにでてくる背景を消すスクリプト
public class NazoReturnButton : ButtonBase
{
    [Tooltip("謎の後ろにあるパネル")] GameObject _nazoBackground;

    void Start()
    {
        _nazoBackground = GameObject.Find("NazoBackground");
    }

    public override void Click()
    {
        //画像の透明度を戻して、背景を消す
        Image.color = ImageColor;
        _nazoBackground.SetActive(false);
    }
}
