using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

/// <summary>ストーリーテキストの管理</summary>
public class StoryTextManager : MonoBehaviour
{
    [SerializeField, Tooltip("ストーリーの後ろ")] GameObject _storyPanel;
    [SerializeField, Tooltip("ストーリーのテキスト")] Text _storyText;
    [SerializeField, Tooltip("スタート時のストーリー")] TextAsset _startStory;
    string[] _start;
    bool _endStartStory = false;
    [SerializeField, Tooltip("クリア時のストーリー")] TextAsset _clearStory;
    string[] _clear;
    [SerializeField, Tooltip("ゲームオーバー時のストーリー")] TextAsset _gameoverStory;
    string[] _gameover;

    void Start()
    {
        _storyText = _storyText.GetComponent<Text>();
        _start = _startStory.text.Split(char.Parse("\n"));
        _storyText.text = "";
    }

    void Update()
    {
        //ゲームが始まったらオープニングストーリーを表示
        if (GameManager.instance._gameMode == GameManager.GameMode.Opening)
        {
            if (!_endStartStory)
            {

                foreach(var s in _start)
                {
                    _storyText.text = $"{_storyText.text}{s}\n";
                }

                _endStartStory = true;
            }
            else
            {
                //話が終わったらゲームに移行
                _storyText.text = "";
                _storyPanel.GetComponent<Image>().DOFade(0, 2.0f).SetEase(Ease.Linear).OnComplete(() => _storyPanel.SetActive(false));
                GameManager.instance._gameMode = GameManager.GameMode.PlayGame;
            }

        }

        //ゲームクリアしたらエンディングストーリーを表示
        if (GameManager.instance._gameMode == GameManager.GameMode.GameClear)
        {

        }

        //時間切れになったらバッドエンドストーリーを表示
        if (GameManager.instance._gameMode == GameManager.GameMode.GameOver)
        {

        }
    }

    IEnumerator ShowStartStory()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
