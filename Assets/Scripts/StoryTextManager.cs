using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>ストーリーテキストの管理</summary>
public class StoryTextManager : MonoBehaviour
{
    [SerializeField, Tooltip("ストーリーの後ろ")] GameObject _storyPanel;
    [SerializeField, Tooltip("ストーリーのテキスト")] Text _storyText;

    void Start()
    {
        _storyText = _storyText.GetComponent<Text>();
    }

    void Update()
    {
        //ゲームが始まったらオープニングストーリーを表示
        if(GameManager.instance.ReadStory)
        {
            _storyPanel.SetActive(false);
            GameManager.instance.ReadStory = false;
            GameManager.instance.InGame = true;
        }

        //ゲームクリアしたらエンディングストーリーを表示
        if(GameManager.instance._clearState == GameManager.Clear.AllStageClear)
        {

        }
    }
}
