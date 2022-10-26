using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>なぞをクリックしたときにでてくる背景を消すスクリプト
public class NazoReturnButton : ButtonBase
{
    [SerializeField, Tooltip("謎の後ろにあるパネル")] GameObject _nazoBackground;

    public override void Click()
    {
            //画像の透明度を戻して、背景を消す
            Image.color = ImageColor;
            _nazoBackground.SetActive(false);
            GameManager.Instance._isFocused = false;
    }
}
