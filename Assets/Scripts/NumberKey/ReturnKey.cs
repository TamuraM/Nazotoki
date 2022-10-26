using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnKey : ButtonBase
{
    [SerializeField] NumberKeyController _numberKeyController;
    [SerializeField, Header("”š“ü—Í‰æ–Ê")] GameObject _inputNumber;

    public override void Click()
    {
        //‰æ‘œ‚Ì“§–¾“x‚ğ–ß‚µ‚ÄA”wŒi‚ğÁ‚·
        _numberKeyController.InputNumber = "";
        Image.color = ImageColor;
        _inputNumber.SetActive(false);
        GameManager.Instance._isFocused = false;
    }
}
