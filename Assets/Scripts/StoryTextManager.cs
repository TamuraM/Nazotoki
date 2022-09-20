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
    [SerializeField, Tooltip("スタート時のストーリー")] TextAsset _startStory;
    bool _goStart = false;
    bool _endStartStory = false;
    [SerializeField, Tooltip("クリア時のストーリー")] TextAsset _clearStory;
    bool _goEnd;
    [SerializeField, Tooltip("ゲームオーバー時のストーリー")] TextAsset _gameoverStory;
    [SerializeField, Tooltip("クレジットのテキストデータ")] TextAsset _creditText;
    bool _goCredit;
    [SerializeField, Header("リトライボタン")] GameObject _retryButton;
    bool _goRetry;
    bool _discoverRetry;
    public bool DiscoverRetry { get => _discoverRetry; }

    void Start()
    {
        _retryButton.SetActive(false);
        _storyText = _storyText.GetComponent<Text>();
        _storyText.text = "";
    }

    void Update()
    {

        //なにかストーリーを流してる時にクリックしたら、一気に最後まで行く
        if(Input.GetKeyDown(KeyCode.Mouse0) && (GameManager.instance._gameMode == GameManager.GameMode.Opening || GameManager.instance._gameMode == GameManager.GameMode.GameClear || GameManager.instance._gameMode == GameManager.GameMode.GameOver || GameManager.instance._gameMode == GameManager.GameMode.Credit))
        {
            _storyText.DOComplete();
        }

        //-----クレジット関係-----
        if (GameManager.instance._gameMode == GameManager.GameMode.Credit && !_goCredit)
        {
            Debug.Log("クレジット");
            _goCredit = true;
            //クレジット流す
            StartCoroutine(ShowCredit());
        }

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
            _retryButton.GetComponent<Image>().DOFade(1.0f, 1.0f).SetEase(Ease.Linear).OnComplete(() => _discoverRetry = true).SetAutoKill();
        }

    }

    IEnumerator ShowStartStory()
    {
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

        yield return _storyText.DOText($"{_clearStory.text}", 0.2f * _clearStory.text.Length).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(1.0f);
        _goRetry = true;
    }

    IEnumerator ShowGameoverStory()
    {
        //yield return new WaitForSeconds(2.0f);
        _storyPanel.SetActive(true);
        yield return _storyPanel.GetComponent<Image>().DOFade(1, 2.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();

        yield return _storyText.DOText($"{_gameoverStory.text}", 0.2f * _gameoverStory.text.Length).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(3.0f);
        yield return _storyText.DOFade(0, 2.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }

    IEnumerator ShowCredit()
    {
        yield return _storyText.DOText($"{_creditText}", 0.2f * _creditText.text.Length).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(3.0f);
        yield return _storyText.DOFade(0, 2.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }
}
