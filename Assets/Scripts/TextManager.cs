using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>ヒントとかのテキストを管理してます</summary>
public class TextManager : MonoBehaviour
{
    [SerializeField, Header("テキストボックス"), Tooltip("テキストボックスのゲームオブジェクト")] GameObject _textBox;
    [Tooltip("テキストのオブジェクト")] Text _text;

    private void Start()
    {
        _textBox.SetActive(false);
        _text = GetComponent<Text>();
    }

    private void Update()
    {

        if(GameManager.instance._clearState == GameManager.Clear.LastStageClear)
        {
            _textBox.SetActive(true);
            //主人公が少し話す
            GameManager.instance._gameMode = GameManager.GameMode.GameClear;
        }

    }
}
