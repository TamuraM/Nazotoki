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
    bool _goClear = false;
    [SerializeField, Tooltip("�Q�[���I�[�o�[���̃X�g�[���[")] TextAsset _gameoverStory;
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
        //�Q�[�����n�܂�����I�[�v�j���O�X�g�[���[��\��
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
                //�b���I�������Q�[���Ɉڍs
                _storyText.text = "";
                _storyPanel.GetComponent<Image>().DOFade(0, 2.0f).SetEase(Ease.Linear).OnComplete(() => _storyPanel.SetActive(false)).SetAutoKill();
                GameManager.instance._gameMode = GameManager.GameMode.PlayGame;
            }

        }

        //�Q�[���N���A������G���f�B���O�X�g�[���[��\��
        if (GameManager.instance._gameMode == GameManager.GameMode.GameClear)
        {
            _storyPanel.SetActive(true);
            if (!_goClear)
            {
                _goClear = true;
                _storyPanel.GetComponent<Image>().DOFade(255, 4.0f).SetEase(Ease.Linear).SetDelay(2.0f).SetAutoKill();
            }
            //�G���f�B���O�\��
        }

        //���Ԑ؂�ɂȂ�����o�b�h�G���h�X�g�[���[��\��
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
