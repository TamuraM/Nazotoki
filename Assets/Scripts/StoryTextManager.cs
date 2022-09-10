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
    bool _goStart = false;
    bool _endStartStory = false;
    [SerializeField, Tooltip("クリア時のストーリー")] TextAsset _clearStory;
    string[] _clear;
    bool _goClear = false;
    [SerializeField, Tooltip("ゲームオーバー時のストーリー")] TextAsset _gameoverStory;
    string[] _gameover;
    bool _goGameover = false;


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

                //foreach (var s in _start)
                //{
                //    _storyText.text = $"{_storyText.text}{s}\n";
                //}

                //_endStartStory = true;

                //if (!_goStart)
                //{
                //    _goStart = true;
                //    _storyText.DOText(_startStory.text, 10.0f).SetEase(Ease.Linear).OnComplete(() => _endStartStory = true).SetAutoKill();
                //}

                if (!_goStart)
                {
                    _goStart = true;
                    StartCoroutine(ShowStartStory());
                }

            }
            else
            {
                //話が終わったらゲームに移行
                _storyText.text = "";
                _storyPanel.GetComponent<Image>().DOFade(0, 2.0f).SetEase(Ease.Linear).OnComplete(() => _storyPanel.SetActive(false)).SetAutoKill();
                GameManager.instance._gameMode = GameManager.GameMode.PlayGame;
            }

        }

        //ゲームクリアしたらエンディングストーリーを表示
        if (GameManager.instance._gameMode == GameManager.GameMode.GameClear)
        {
            _storyPanel.SetActive(true);
            if (!_goClear)
            {
                _goClear = true;
                _storyPanel.GetComponent<Image>().DOFade(255, 4.0f).SetEase(Ease.Linear).SetDelay(2.0f).SetAutoKill();
            }
            //エンディング表示
        }

        //時間切れになったらバッドエンドストーリーを表示
        if (GameManager.instance._gameMode == GameManager.GameMode.GameOver)
        {
            _storyPanel.SetActive(true);
            if (!_goGameover)
            {
                _goGameover = true;
                _storyPanel.GetComponent<Image>().DOFade(255, 2.0f).SetEase(Ease.Linear).SetDelay(1.0f);
            }
        }
    }

    IEnumerator ShowStartStory()
    {

        foreach (var s in _start)
        {
            //_storyText.DOText($"{_storyText.text}{s}", 3.0f).SetEase(Ease.Linear).SetAutoKill();
            yield return _storyText.DOText($"{_storyText.text}{s}", 3.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
            _storyText.text = $"{_storyText.text}\n";
        }

        _endStartStory = true;
    }
}
