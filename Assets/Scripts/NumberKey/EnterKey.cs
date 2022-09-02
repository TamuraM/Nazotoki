using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnterKey : NumberKeyBase
{
    [SerializeField, Header("”š“ü—Í‰æ–Ê")] GameObject _inputNumber;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (_numberKeyController.Num.Length == 4)
        {
            Click();
        }
        else
        {
            return;
        }
    }

    public override void Click()
    {
        if (_numberKeyController.Num == _numberKeyController.Answer)
        {
            //‰æ‘œ‚Ì“§–¾“x‚ğ–ß‚µ‚ÄA”wŒi‚ğÁ‚·
            Image.color = ImageColor;
            _inputNumber.SetActive(false);
            GameManager.instance._isFocused = false;
            GameManager.instance._clearState = GameManager.Clear.LastStageClear;
        }
        else
        {
            _numberKeyController.Num = "";
        }
    }
}
