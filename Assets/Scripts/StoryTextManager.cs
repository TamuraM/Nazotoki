using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�X�g�[���[�e�L�X�g�̊Ǘ�</summary>
public class StoryTextManager : MonoBehaviour
{
    [SerializeField, Tooltip("�X�g�[���[�̌��")] GameObject _storyPanel;
    [SerializeField, Tooltip("�X�g�[���[�̃e�L�X�g")] Text _storyText;

    void Start()
    {
        _storyText = _storyText.GetComponent<Text>();
    }

    void Update()
    {
        //�Q�[�����n�܂�����I�[�v�j���O�X�g�[���[��\��
        if(GameManager.instance.ReadStory)
        {
            _storyPanel.SetActive(false);
            GameManager.instance.ReadStory = false;
            GameManager.instance.InGame = true;
        }

        //�Q�[���N���A������G���f�B���O�X�g�[���[��\��
        if(GameManager.instance._clearState == GameManager.Clear.AllStageClear)
        {

        }
    }
}
