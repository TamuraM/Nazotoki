using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�Ȃ����N���b�N�����Ƃ��ɂłĂ���w�i�������X�N���v�g
public class NazoReturnButton : ButtonBase
{
    [SerializeField, Tooltip("��̌��ɂ���p�l��")] GameObject _nazoBackground;

    public override void Click()
    {
            //�摜�̓����x��߂��āA�w�i������
            Image.color = ImageColor;
            _nazoBackground.SetActive(false);
            GameManager.Instance._isFocused = false;
    }
}
