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
    [SerializeField, Tooltip("�N���W�b�g�̃e�L�X�g�f�[�^")] TextAsset _creditText;
    [Tooltip("��㉺�����E���EBA")] KeyCode[] _command = { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A };
    [Tooltip("�R�}���h�̃��X�g")] List<KeyCode> _creditCommand;
    [SerializeField, Header("���g���C�{�^��")] GameObject _retryButton;
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
        //-----�N���W�b�g�֌W-----
        if (Input.GetKeyDown(_creditCommand[0]))
        {
            _creditCommand.Remove(0);
            //�Ȃ񂩉��炵����
        }
        else if(!Input.GetKeyDown(_creditCommand[0]))
        {
            _creditCommand = _command.ToList();
        }

        if(_creditCommand.Count == 0)
        {
            //�N���W�b�g����
        }
        //-----�����܂�-----

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

        //foreach (var s in _start)
        //{

        //    if (s != "��")
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

        //    if (s != "��")
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

        //    if (s != "��")
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
