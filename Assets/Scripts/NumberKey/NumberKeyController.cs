using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�e���L�[����̓��͂��󂯎���āA�e�L�X�g�\���E���딻�������</summary>
public class NumberKeyController : MonoBehaviour
{
    [SerializeField, Header("���͂��ꂽ������\������")] Text _inputNumberText;
    [Tooltip("���͂��ꂽ����")] string _inputNumber = "";
    public string InputNumber { get => _inputNumber; set => _inputNumber = value; }
    [Tooltip("�����̐���")] string _answer = "2965";
    public string Answer { get => _answer; }

    private void Update()
    {
        _inputNumberText.text = _inputNumber;
    }
}
