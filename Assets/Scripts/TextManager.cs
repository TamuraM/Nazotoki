using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�e�L�X�g���Ǘ����Ă܂�</summary>
public class TextManager : MonoBehaviour
{
    [SerializeField, Header("�e�L�X�g�{�b�N�X"), Tooltip("�e�L�X�g�{�b�N�X�̃Q�[���I�u�W�F�N�g")] GameObject _textBox;
    [Header("�e�L�X�g"), Tooltip("�e�L�X�g�̃I�u�W�F�N�g")] Text _text;

    private void Start()
    {
        _textBox.SetActive(false);
        _text = GetComponent<Text>();
    }

    private void Update()
    {

    }
}
