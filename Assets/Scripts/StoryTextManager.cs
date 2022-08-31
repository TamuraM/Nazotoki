using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>ストーリーテキストの管理</summary>
public class StoryTextManager : MonoBehaviour
{
    [SerializeField, Tooltip("ストーリーの後ろ")] GameObject _storyPanel;
    [SerializeField, Tooltip("ストーリーのテキスト")] Text _storyText;

    void Start()
    {
        _storyText = _storyText.GetComponent<Text>();
    }

    void Update()
    {
        //ゲームが始まったらオープニングストーリーを表示
        if(GameManager.instance._gameMode == GameManager.GameMode.Opening)
        {
            //話が終わったらゲームに移行
            _storyPanel.SetActive(false);
            GameManager.instance._gameMode = GameManager.GameMode.PlayGame;
        }

        //ゲームクリアしたらエンディングストーリーを表示
        if(GameManager.instance._gameMode == GameManager.GameMode.GameClear)
        {

        }

        //時間切れになったらバッドエンドストーリーを表示
        if(GameManager.instance._gameMode == GameManager.GameMode.GameOver)
        {

        }
    }
}
