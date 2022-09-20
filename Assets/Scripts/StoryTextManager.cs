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
    [SerializeField, Tooltip("�X�^�[�g���̃X�g�[���[")] TextAsset _startStory;
    bool _goStart = false;
    bool _endStartStory = false;
    [SerializeField, Tooltip("�N���A���̃X�g�[���[")] TextAsset _clearStory;
    bool _goEnd;
    [SerializeField, Tooltip("�Q�[���I�[�o�[���̃X�g�[���[")] TextAsset _gameoverStory;
    [SerializeField, Tooltip("�N���W�b�g�̃e�L�X�g�f�[�^")] TextAsset _creditText;
    bool _goCredit;
    [SerializeField, Header("���g���C�{�^��")] GameObject _retryButton;
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

        //�Ȃɂ��X�g�[���[�𗬂��Ă鎞�ɃN���b�N������A��C�ɍŌ�܂ōs��
        if(Input.GetKeyDown(KeyCode.Mouse0) && (GameManager.instance._gameMode == GameManager.GameMode.Opening || GameManager.instance._gameMode == GameManager.GameMode.GameClear || GameManager.instance._gameMode == GameManager.GameMode.GameOver || GameManager.instance._gameMode == GameManager.GameMode.Credit))
        {
            _storyText.DOComplete();
        }

        //-----�N���W�b�g�֌W-----
        if (GameManager.instance._gameMode == GameManager.GameMode.Credit && !_goCredit)
        {
            Debug.Log("�N���W�b�g");
            _goCredit = true;
            //�N���W�b�g����
            StartCoroutine(ShowCredit());
        }

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
