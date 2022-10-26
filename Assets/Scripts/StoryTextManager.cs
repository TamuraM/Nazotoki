using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

/// <summary>ストーリーテキストの管理</summary>
public class StoryTextManager : MonoBehaviour
{
    [SerializeField, Tooltip("ストーリーの後ろ")] GameObject _storyPanel;
    [SerializeField, Tooltip("ストーリーのテキスト")] Text _storyText;

    //スタート関係
    [SerializeField, Tooltip("スタート時のストーリー")] TextAsset _startStory;
    [Tooltip("スタート時のストーリーを流したかどうか" )] bool _goStart = false;
    [Tooltip("スタート時のストーリーを流し終えたかどうか")] bool _endStartStory = false;

    //エンド関係
    [SerializeField, Tooltip("クリア時のストーリー")] TextAsset _clearStory;
    [SerializeField, Tooltip("ゲームオーバー時のストーリー")] TextAsset _gameoverStory;
    [Tooltip("エンディング時のストーリーを流したかどうか")] bool _goEnd;

    //タイトルボタン関係
    [SerializeField, Header("タイトルボタン")] GameObject _titleButton;
    [Tooltip("タイトルボタンを表示したかどうか")] bool _goTitle;
    [Tooltip("タイトルボタンを表示し終えたかどうか")] bool _discoverTitleButton;

    //クレジット関係
    [SerializeField, Tooltip("クレジットのテキストデータ")] TextAsset _creditText;
    [Tooltip("クレジットを流したかどうか")] bool _goCredit;
    
    public bool DiscoverTitleButton { get => _discoverTitleButton; }

    void Start()
    {
        _titleButton.SetActive(false);
        _storyText = _storyText.GetComponent<Text>();
        _storyText.text = "";
    }

    void Update()
    {

        //なにかストーリーを流してる時にクリックしたら、一気に最後まで行く
        if(Input.GetKeyDown(KeyCode.Mouse0) && (GameManager.Instance._gameMode == GameManager.GameMode.Opening || GameManager.Instance._gameMode == GameManager.GameMode.GameClear || GameManager.Instance._gameMode == GameManager.GameMode.GameOver || GameManager.Instance._gameMode == GameManager.GameMode.Credit))
        {
            _storyText.DOComplete();
        }

        //-----クレジット関係-----
        if (GameManager.Instance._gameMode == GameManager.GameMode.Credit && !_goCredit)
        {
            Debug.Log("クレジット");
            _goCredit = true;
            //クレジット流す
            StartCoroutine(ShowCredit());
        }

        //ゲームが始まったらオープニングストーリーを表示
        if (GameManager.Instance._gameMode == GameManager.GameMode.Opening)
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
                GameManager.Instance._gameMode = GameManager.GameMode.PlayGame;
            }

        }

        //ゲームクリアしたらクリアストーリーを表示
        if (GameManager.Instance._gameMode == GameManager.GameMode.GameClear)
        {

            //エンディング表示
            if (!_goEnd)
            {
                _goEnd = true;
                StartCoroutine(ShowClearStory());
            }

        }

        //時間切れになったらバッドエンドストーリーを表示
        if (GameManager.Instance._gameMode == GameManager.GameMode.GameOver)
        {

            //バッドエンド表示
            if (!_goEnd)
            {
                _goEnd = true;
                StartCoroutine(ShowGameoverStory());
            }

        }

        //リトライボタンをフェードインしたい
        if (_goTitle)
        {
            _goTitle = false;
            _titleButton.SetActive(true);
            _titleButton.GetComponent<Image>().DOFade(1.0f, 1.0f).SetEase(Ease.Linear).OnComplete(() => _discoverTitleButton = true).SetAutoKill();
        }

    }

    IEnumerator ShowStartStory()
    {
        yield return _storyText.DOText($"{_startStory.text}", 0.15f * _startStory.text.Length).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(1.0f);
        _storyText.text = "";
        _endStartStory = true;
    }

    IEnumerator ShowClearStory()
    {
        yield return new WaitForSeconds(2.0f);
        _storyPanel.SetActive(true);
        yield return _storyPanel.GetComponent<Image>().DOFade(1, 2.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();

        yield return _storyText.DOText($"{_clearStory.text}", 0.15f * _clearStory.text.Length).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(1.0f);
        _goTitle = true;
    }

    IEnumerator ShowGameoverStory()
    {
        _storyPanel.SetActive(true);
        yield return _storyPanel.GetComponent<Image>().DOFade(1, 2.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();

        yield return _storyText.DOText($"{_gameoverStory.text}", 0.15f * _gameoverStory.text.Length).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(3.0f);
        yield return _storyText.DOFade(0, 2.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }

    IEnumerator ShowCredit()
    {
        yield return _storyText.DOText($"{_creditText}", 0.15f * _creditText.text.Length).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(3.0f);
        yield return _storyText.DOFade(0, 2.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }
}
