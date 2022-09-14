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
    bool _goEnd;
    [SerializeField, Tooltip("ゲームオーバー時のストーリー")] TextAsset _gameoverStory;
    string[] _gameover;
    [SerializeField, Tooltip("クレジットのテキストデータ")] TextAsset _creditText;
    [Tooltip("上上下下左右左右BA")] KeyCode[] _command = { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A };
    [Tooltip("コマンドのリスト")] List<KeyCode> _creditCommand;
    [SerializeField, Header("リトライボタン")] GameObject _retryButton;
    bool _goRetry;

    void Start()
    {
        _retryButton.SetActive(false);
        _storyText = _storyText.GetComponent<Text>();
        //_start = _startStory.text.Split(char.Parse("\n"));
        //_clear = _clearStory.text.Split(char.Parse("\n"));
        //_gameover = _gameoverStory.text.Split(char.Parse("\n"));
        _storyText.text = "";
        _creditCommand = _command.ToList();
    }

    void Update()
    {
        //-----クレジット関係-----
        if (Input.GetKeyDown(_creditCommand[0]))
        {
            _creditCommand.Remove(0);
            //なんか音鳴らしたい
        }
        else if(!Input.GetKeyDown(_creditCommand[0]))
        {
            _creditCommand = _command.ToList();
        }

        if(_creditCommand.Count == 0)
        {
            //クレジット流す
        }
        //-----ここまで-----

        //ゲームが始まったらオープニングストーリーを表示
        if (GameManager.instance._gameMode == GameManager.GameMode.Opening)
        {
            if (!_endStartStory)
            {

                if (!_goStart)
                {
                    _goStart = true;
                    StartCoroutine(ShowStartStory());
                    //_endStartStory = true; //テスト用
                }

            }
            else
            {
                //話が終わったらゲームに移行
                _storyPanel.GetComponent<Image>().DOFade(0, 2.0f).SetEase(Ease.Linear).OnComplete(() => _storyPanel.SetActive(false)).SetAutoKill();
                GameManager.instance._gameMode = GameManager.GameMode.PlayGame;
            }

        }

        //ゲームクリアしたらエンディングストーリーを表示
        if (GameManager.instance._gameMode == GameManager.GameMode.GameClear)
        {

            //エンディング表示
            if (!_goEnd)
            {
                _goEnd = true;
                StartCoroutine(ShowEndStory());
            }

        }

        //時間切れになったらバッドエンドストーリーを表示
        if (GameManager.instance._gameMode == GameManager.GameMode.GameOver)
        {

            //バッドエンド表示
            if (!_goEnd)
            {
                _goEnd = true;
                StartCoroutine(ShowGameoverStory());
            }

        }

        //リトライボタンをフェードインしたい
        if (_goRetry)
        {
            _goRetry = false;
            _retryButton.SetActive(true);
            _retryButton.GetComponent<Image>().DOFade(1.0f,1.0f).SetEase(Ease.Linear);
        }

    }

    IEnumerator ShowStartStory()
    {

        //foreach (var s in _start)
        //{

        //    if (s != "空白")
        //    {
        //        yield return _storyText.DOText($"{_storyText.text}{s}\n", 0.5f * (s.Length + _storyText.text.Length)).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        //    }
        //    else
        //    {
        //        yield return _storyText.DOText($"", 2.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        //    }

        //}

        yield return _storyText.DOText($"{_startStory.text}", 0.2f * _startStory.text.Length).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(1.0f);
        _storyText.text = "";
        _endStartStory = true;
    }

    IEnumerator ShowEndStory()
    {
        yield return new WaitForSeconds(2.0f);
        _storyPanel.SetActive(true);
        yield return _storyPanel.GetComponent<Image>().DOFade(1, 2.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();

        //foreach (var s in _clear)
        //{

        //    if (s != "空白")
        //    {
        //        yield return _storyText.DOText($"\n{_storyText.text}{s}", 3.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        //    }
        //    else
        //    {
        //        yield return _storyText.DOText($"", 2.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        //    }

        //}

        yield return _storyText.DOText($"{_clearStory.text}", 0.2f * _clearStory.text.Length).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(1.0f);
        _goRetry = true;
    }

    IEnumerator ShowGameoverStory()
    {
        yield return new WaitForSeconds(2.0f);
        _storyPanel.SetActive(true);
        yield return _storyPanel.GetComponent<Image>().DOFade(1, 2.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();

        //foreach (var s in _gameover)
        //{

        //    if (s != "空白")
        //    {
        //        _storyText.text = $"{_storyText.text}\n";
        //        yield return _storyText.DOText($"{_storyText.text}{s}\n", 0.5f * s.Length).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        //    }
        //    else
        //    {
        //        yield return _storyText.DOText($"", 2.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        //    }

        //}

        yield return _storyText.DOText($"{_gameoverStory.text}", 0.2f * _gameoverStory.text.Length).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(1.0f);
        _goRetry = true;
    }
}
