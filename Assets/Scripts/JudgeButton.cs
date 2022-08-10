using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>入力されたテキストが正解かどうか判定するスクリプト</summary>
public class JudgeButton : ButtonBase
{
    [Tooltip("入力されたテキスト")] Text _inputText;

    // Start is called before the first frame update
    void Start()
    {
        _inputText = GameObject.Find("InputText").GetComponent<Text>();
    }

    public override void Click()
    {
        //入力されたテキストが正解かどうか判定する
        base.Click();
    }
}
