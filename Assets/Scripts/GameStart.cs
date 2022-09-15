using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameStart : MonoBehaviour
{
    [SerializeField, Header("タイトル")] GameObject _title;
    [SerializeField, Header("スタート")] GameObject _start;
    Image _titleImage;
    Text _startText;
    [SerializeField, Header("ストーリーのText")] GameObject _storyText;
    [SerializeField, Header("フェードした後の間隔")] float _fade;
    [SerializeField, Tooltip("押されたか")] bool _isPushed;

    private void Start()
    {
        _titleImage = _title.GetComponent<Image>();
        _startText = _start.GetComponent<Text>();
        _storyText.SetActive(false);
        _isPushed = false;
        //タイトルをフェード
        _title.GetComponent<Image>().DOFade(1, 2.5f).SetEase(Ease.Linear).SetAutoKill();
        //スタートテキストをフェード後にアニメーション
        _startText.DOFade(1, 2.5f).SetEase(Ease.Linear)
            .OnComplete(() =>
            { _startText.DOFade(0.1f, 1.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetAutoKill(); _start.transform.DOMoveY(-6.0f, 1.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetRelative().SetAutoKill(); })
                .SetAutoKill();
        //_startText.DOFade(0.1f, 1.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetAutoKill();
        //_start.transform.DOMoveY(-6.0f, 1.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetRelative().SetAutoKill();
    }

    void Update()
    {
        //エンターキーが押されたらシーン移行
        if (Input.GetKeyDown(KeyCode.Return) && !_isPushed)
        {
            _isPushed = !_isPushed;
            _startText.DORewind();
            _start.transform.DORewind();
            StartCoroutine(Fade(GameManager.GameMode.Opening));
        }

        //エスケープキーが押されたらクレジットを流す
        if (GameManager.instance._gameMode == GameManager.GameMode.Title && !_isPushed && Input.GetKeyDown(KeyCode.Escape))
        {
            _isPushed = !_isPushed;
            _startText.DORewind();
            _start.transform.DORewind();
            StartCoroutine(Fade(GameManager.GameMode.Credit));
        }

    }

    IEnumerator Fade(GameManager.GameMode gameMode)
    {
        _titleImage.DOFade(0, _fade).SetEase(Ease.Linear).OnComplete(() => _title.SetActive(false)).SetAutoKill();
        _startText.DOFade(0, _fade).SetEase(Ease.Linear).OnComplete(() => _start.SetActive(false)).SetAutoKill();

        yield return new WaitForSeconds(_fade);

        _storyText.SetActive(true);
        GameManager.instance._gameMode = gameMode;
    }

}
