using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�Ȃ����N���b�N�����Ƃ��ɂłĂ���w�i�������X�N���v�g
public class NazoReturnButton : ButtonBase
{
    [Tooltip("��̌��ɂ���p�l��")] GameObject _nazoBackground;

    void Start()
    {
        _nazoBackground = GameObject.Find("NazoBackground");
    }

    public override void Click()
    {
        //�摜�̓����x��߂��āA�w�i������
        Image.color = ImageColor;
        _nazoBackground.SetActive(false);
    }
}
