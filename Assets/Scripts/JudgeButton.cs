using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>���͂��ꂽ�e�L�X�g���������ǂ������肷��X�N���v�g</summary>
public class JudgeButton : ButtonBase
{
    [Tooltip("���͂��ꂽ�e�L�X�g")] Text _inputText;
    [Tooltip("��̓���")] string seikai = "";
    [SerializeField, Header("���o�["), Tooltip("���o�[�̃Q�[���I�u�W�F�N�g")] GameObject _lever;
    [Tooltip("�������͂̌��ɂ���p�l��")] GameObject _inputTextBackground;

    // Start is called before the first frame update
    void Start()
    {
        _inputTextBackground = GameObject.Find("InputTextBackground");
        _inputText = GameObject.Find("InputText").GetComponent<Text>();
        _lever.SetActive(false);
    }

    public override void Click()
    {
        //���͂��ꂽ�e�L�X�g���������ǂ������肷��
        if(_inputText.text == seikai)
        {
            //�����������烌�o�[�������
            _lever.SetActive(true);
            _inputTextBackground.SetActive(false);
        }
    }
}
