using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>なぞをクリックしたときにでてくる背景を消すスクリプト
public class NazoReturnButton : ButtonBase
{
    [Tooltip("謎の後ろにあるパネル")] GameObject _nazoBackground;
    [Tooltip("ドアの謎の後ろにあるパネル")] GameObject _lastNazoBackground;

    void Start()
    {
        _nazoBackground = GameObject.Find("NazoBackground");
        _lastNazoBackground = GameObject.Find("LastNazoBackground");
    }

    public override void Click()
    {

        if(this.gameObject.name == "NazoReturnButton")
        {
            //画像の透明度を戻して、背景を消す
            Image.color = ImageColor;
            _nazoBackground.SetActive(false);
            GameManager.instance._isFocused = false;
        }
        else if(this.gameObject.name == "LastNazoReturnButton")
        {
            Image.color = ImageColor;
            _lastNazoBackground.SetActive(false);
            GameManager.instance._isFocused = false;
        }

    }
}
