using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnKey : ButtonBase
{
    [SerializeField] NumberKeyController _numberKeyController;
    [SerializeField, Header("�������͉��")] GameObject _inputNumber;

    public override void Click()
    {
        //�摜�̓����x��߂��āA�w�i������
        _numberKeyController.InputNumber = "";
        Image.color = ImageColor;
        _inputNumber.SetActive(false);
        GameManager.Instance._isFocused = false;
    }
}
