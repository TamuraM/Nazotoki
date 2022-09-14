using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�e���L�[����̓��͂��󂯎���āA�e�L�X�g�\���E���딻�������</summary>
public class NumberKeyController : MonoBehaviour
{
    [SerializeField, Header("���͂��ꂽ������\������")] Text _inputNumber;
    [Tooltip("���͂��ꂽ����")] string _num = "";
    public string Num { get => _num; set => _num = value; }
    [Tooltip("�����̐���")] string _answer = "2965";
    public string Answer { get => _answer; }

    private void Update()
    {
        _inputNumber.text = _num;
    }
}
