using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnKey : NumberKeyBase
{
    [SerializeField, Header("�������͉��")] GameObject _inputNumber;

    public override void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }

    public override void Click()
    {
        //�摜�̓����x��߂��āA�w�i������
        _numberKeyController.Num = "";
        Image.color = ImageColor;
        _inputNumber.SetActive(false);
        GameManager.instance._isFocused = false;
    }
}
