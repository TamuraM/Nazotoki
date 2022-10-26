using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�^�C�v���C�^�[���N���b�N�����Ƃ��ɏo�Ă���w�i�������X�N���v�g</summary>
public class InputTextReturnButton : ButtonBase
{
    [SerializeField, Tooltip("�������͂̌��ɂ���p�l��")] GameObject _inputTextBackground;
    [SerializeField, Tooltip("���͂��ꂽ�e�L�X�g")] Text _inputText;

    public override void Click()
    {
        _inputText.text = "";
        Image.color = ImageColor;
        _inputTextBackground.SetActive(false);
        GameManager.Instance._isFocused = false;
    }
}
