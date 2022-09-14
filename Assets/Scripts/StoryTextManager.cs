using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

/// <summary>�X�g�[���[�e�L�X�g�̊Ǘ�</summary>
public class StoryTextManager : MonoBehaviour
{
    [SerializeField, Tooltip("�X�g�[���[�̌��")] GameObject _storyPanel;
    [SerializeField, Tooltip("�X�g�[���[�̃e�L�X�g")] Text _storyText;
    [SerializeField, Tooltip("�X�^�[�g���̃X�g�[���[")] TextAsset _startStory;
    string[] _start;
    bool _goStart = false;
    bool _endStartStory = false;
    [SerializeField, Tooltip("�N���A���̃X�g�[���[")] TextAsset _clearStory;
    string[] _clear;
    bool _goEnd;
    [SerializeField, Tooltip("�Q�[���I�[�o�[���̃X�g�[���[")] TextAsset _gameoverStory;
    string[] _gameover;
    [SerializeField, Header("���g���C�{�^��")] GameObject _retryButton;
    bool _goRetry;

    void Start()
    {
        _retryButton.SetActive(false);
        _storyText = _storyText.GetComponent<Text>();
        _start = _startStory.text.Split(char.Parse("\n"));
        _clear = _clearStory.text.Split(char.Parse("\n"));
        _gameover = _gameoverStory.text.Split(char.Parse("\n"));
        _storyText.text = "";
    }

    void Update()
    {
        //�Q�[�����n�܂�����I�[�v�j���O�X�g�[���[��\��
        if (GameManager.instance._gameMode == GameManager.GameMode.Opening)
        {
            if (!_endStartStory)
            {

                if (!_goStart)
                {
                    _goStart = true;
                    StartCoroutine(ShowStartStory());
                    //_endStartStory = true; //�e�X�g�p
                }

            }
            else
            {
                //�b���I�������Q�[���Ɉڍs
                _storyText.text = "";
                _storyPanel.GetComponent<Image>().DOFade(0, 2.0f).SetEase(Ease.Linear).OnComplete(() => _storyPanel.SetActive(false)).SetAutoKill();
                GameManager.instance._gameMode = GameManager.GameMode.PlayGame;
            }

        }

        //�Q�[���N���A������G���f�B���O�X�g�[���[��\��
        if (GameManager.instance._gameMode == GameManager.GameMode.GameClear)
        {

            //�G���f�B���O�\��
            if (!_goEnd)
            {
                _goEnd = true;
                StartCoroutine(ShowEndStory());
            }

        }

        //���Ԑ؂�ɂȂ�����o�b�h�G���h�X�g�[���[��\��
        if (GameManager.instance._gameMode == GameManager.GameMode.GameOver)
        {

            //�o�b�h�G���h�\��
            if (!_goEnd)
            {
                _goEnd = true;
                StartCoroutine(ShowGameoverStory());
            }

        }

        //���g���C�{�^�����t�F�[�h�C��������
        if (_goRetry)
        {
            _goRetry = false;
            _retryButton.SetActive(true);
            _retryButton.GetComponent<Image>().DOFade(1.0f,1.0f).SetEase(Ease.Linear);
        }

    }

    IEnumerator ShowStartStory()
    {

        foreach (var s in _start)
        {

            if (s != "��")
            {
                yield return _storyText.DOText($"{_storyText.text}{s}", 3.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
                _storyText.text = $"{_storyText.text}\n";
            }

        }

        _endStartStory = true;
    }

    IEnumerator ShowEndStory()
    {
        yield return new WaitForSeconds(2.0f);
        _storyPanel.SetActive(true);
        yield return _storyPanel.GetComponent<Image>().DOFade(1, 2.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();

        foreach (var s in _clear)
        {

            if (s != "��")
            {
                _storyText.text = $"{_storyText.text}\n";
                yield return _storyText.DOText($"{_storyText.text}{s}", 3.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
            }

        }

        _goRetry = true;
    }

    IEnumerator ShowGameoverStory()
    {
        yield return new WaitForSeconds(2.0f);
        _storyPanel.SetActive(true);
        yield return _storyPanel.GetComponent<Image>().DOFade(1, 2.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();

        foreach (var s in _gameover)
        {

            if (s != "��")
            {
                yield return _storyText.DOText($"{_storyText.text}{s}", 3.0f).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
                _storyText.text = $"{_storyText.text}\n";
            }

        }

        _goRetry = true;
    }
}
