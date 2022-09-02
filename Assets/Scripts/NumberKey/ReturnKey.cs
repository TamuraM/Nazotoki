using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnKey : NumberKeyBase
{
    [SerializeField, Header("数字入力画面")] GameObject _inputNumber;

    public override void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }

    public override void Click()
    {
        //画像の透明度を戻して、背景を消す
        _numberKeyController.Num = "";
        Image.color = ImageColor;
        _inputNumber.SetActive(false);
        GameManager.instance._isFocused = false;
    }
}
