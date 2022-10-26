using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

/// <summary>�X�g�[���[�e�L�X�g�̊Ǘ�</summary>
public class StoryTextManager : MonoBehaviour
{
    [SerializeField, Tooltip("�X�g�[���[�̌��")] GameObject _storyPanel;
    [SerializeField, Tooltip("�X�g�[���[�̃e�L�X�g")] Text _storyText;

    //�X�^�[�g�֌W
    [SerializeField, Tooltip("�X�^�[�g���̃X�g�[���[")] TextAsset _startStory;
    [Tooltip("�X�^�[�g���̃X�g�[���[�𗬂������ǂ���" )] bool _goStart = false;
    [Tooltip("�X�^�[�g���̃X�g�[���[�𗬂��I�������ǂ���")] bool _endStartStory = false;

    //�G���h�֌W
    [SerializeField, Tooltip("�N���A���̃X�g�[���[")] TextAsset _clearStory;
    [SerializeField, Tooltip("�Q�[���I�[�o�[���̃X�g�[���[")] TextAsset _gameoverStory;
    [Tooltip("�G���f�B���O���̃X�g�[���[�𗬂������ǂ���")] bool _goEnd;

    //�^�C�g���{�^���֌W
    [SerializeField, Header("�^�C�g���{�^��")] GameObject _titleButton;
    [Tooltip("�^�C�g���{�^����\���������ǂ���")] bool _goTitle;
    [Tooltip("�^�C�g���{�^����\�����I�������ǂ���")] bool _discoverTitleButton;

    //�N���W�b�g�֌W
    [SerializeField, Tooltip("�N���W�b�g�̃e�L�X�g�f�[�^")] TextAsset _creditText;
    [Tooltip("�N���W�b�g�𗬂������ǂ���")] bool _goCredit;
    
    public bool DiscoverTitleButton { get => _discoverTitleButton; }

    void Start()
    {
        _titleButton.SetActive(false);
        _storyText = _storyText.GetComponent<Text>();
        _storyText.text = "";
    }

    void Update()
    {

        //�Ȃɂ��X�g�[���[�𗬂��Ă鎞�ɃN���b�N������A��C�ɍŌ�܂ōs��
        if(Input.GetKeyDown(KeyCode.Mouse0) && (GameManager.Instance._gameMode == GameManager.GameMode.Opening || GameManager.Instance._gameMode == GameManager.GameMode.GameClear || GameManager.Instance._gameMode == GameManager.GameMode.GameOver || GameManager.Instance._gameMode == GameManager.GameMode.Credit))
        {
            _storyText.DOComplete();
        }

        //-----�N���W�b�g�֌W-----
        if (GameManager.Instance._gameMode == GameManager.GameMode.Credit && !_goCredit)
        {
            Debug.Log("�N���W�b�g");
            _goCredit = true;
            //�N���W�b�g����
            StartCoroutine(ShowCredit());
        }

        //�Q�[�����n�܂�����I�[�v�j���O�X�g�[���[��\��
        if (GameManager.Instance._gameMode == GameManager.GameMode.Opening)
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
                GameManager.Instance._gameMode = GameManager.GameMode.PlayGame;
            }

        }

        //�Q�[���N���A������N���A�X�g�[���[��\��
        if (GameManager.Instance._gameMode == GameManager.GameMode.GameClear)
        {

            //�G���f�B���O�\��
            if (!_goEnd)
            {
                _goEnd = true;
                StartCoroutine(ShowClearStory());
            }

        }

        //���Ԑ؂�ɂȂ�����o�b�h�G���h�X�g�[���[��\��
        if (GameManager.Instance._gameMode == GameManager.GameMode.GameOver)
        {

            //�o�b�h�G���h�\��
            if (!_goEnd)
            {
                _goEnd = true;
                StartCoroutine(ShowGameoverStory());
            }

        }

        //���g���C�{�^�����t�F�[�h�C��������
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
