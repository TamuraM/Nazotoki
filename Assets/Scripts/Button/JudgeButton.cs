using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>入力されたテキストが正解かどうか判定するスクリプト</summary>
public class JudgeButton : ButtonBase
{
    [Tooltip("入力されたテキスト")] Text _inputText;
    [Tooltip("謎の答え(表記揺れ有り)")] List<string> _answers = new List<string>{ "samon", "samonn", "Samon", "Samonn", "SAMON", "SAMONN"};
    [SerializeField, Header("ボタン"), Tooltip("ボタンのゲームオブジェクト")] GameObject _button;
    [Tooltip("文字入力の後ろにあるパネル")] GameObject _inputTextBackground;

    void Start()
    {
        _inputTextBackground = GameObject.Find("InputTextBackground");
        _inputText = GameObject.Find("InputText").GetComponent<Text>();
        _button.SetActive(false);
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Click();
        }

    }

    public override void Click()
    {
        //入力されたテキストが正解かどうか判定する
        if(_answers.Contains(_inputText.text))
        {
            //正解だったらボタンが現れる
            _inputTextBackground.SetActive(false);
            _button.SetActive(true);
            _button.GetComponent<SpriteRenderer>().DOFade(1, 1.0f).SetEase(Ease.Linear).SetAutoKill();
            GameManager.instance._isFocused = false;
        }
        else
        {
            _inputText.text = "";
        }
    }
}