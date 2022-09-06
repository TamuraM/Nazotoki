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
    bool _endStartStory = false;
    [SerializeField, Tooltip("�N���A���̃X�g�[���[")] TextAsset _clearStory;
    string[] _clear;
    [SerializeField, Tooltip("�Q�[���I�[�o�[���̃X�g�[���[")] TextAsset _gameoverStory;
    string[] _gameover;

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

                foreach(var s in _start)
                {
                    _storyText.text = $"{_storyText.text}{s}\n";
                }

                _endStartStory = true;
            }
            else
            {
                //�b���I�������Q�[���Ɉڍs
                _storyText.text = "";
                _storyPanel.GetComponent<Image>().DOFade(0, 2.0f).SetEase(Ease.Linear).OnComplete(() => _storyPanel.SetActive(false));
                GameManager.instance._gameMode = GameManager.GameMode.PlayGame;
            }

        }

        //�Q�[���N���A������G���f�B���O�X�g�[���[��\��
        if (GameManager.instance._gameMode == GameManager.GameMode.GameClear)
        {

        }

        //���Ԑ؂�ɂȂ�����o�b�h�G���h�X�g�[���[��\��
        if (GameManager.instance._gameMode == GameManager.GameMode.GameOver)
        {

        }
    }

    IEnumerator ShowStartStory()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
