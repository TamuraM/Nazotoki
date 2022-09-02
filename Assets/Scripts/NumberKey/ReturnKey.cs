using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnKey : NumberKeyBase
{
    [SerializeField, Header("”š“ü—Í‰æ–Ê")] GameObject _inputNumber;

    public override void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }

    public override void Click()
    {
        //‰æ‘œ‚Ì“§–¾“x‚ğ–ß‚µ‚ÄA”wŒi‚ğÁ‚·
        _numberKeyController.Num = "";
        Image.color = ImageColor;
        _inputNumber.SetActive(false);
        GameManager.instance._isFocused = false;
    }
}
