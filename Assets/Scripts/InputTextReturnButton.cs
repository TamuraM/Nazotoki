using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�^�C�v���C�^�[���N���b�N�����Ƃ��ɏo�Ă���w�i�������X�N���v�g</summary>
public class InputTextReturnButton : ButtonBase
{
    [Tooltip("�������͂̌��ɂ���p�l��")] GameObject _inputTextBackground;

    void Start()
    {
        _inputTextBackground = GameObject.Find("InputTextBackground");
    }

    public override void Click()
    {
        Image.color = ImageColor;
        _inputTextBackground.SetActive(false);
    }
}
