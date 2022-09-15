using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetireReturnButton : ButtonBase
{
    [SerializeField, Header("ƒŠƒ^ƒCƒA‰æ–Ê‚Ì”wŒi")] GameObject _retirePanel;

    public override void Click()
    {
        Image.color = ImageColor;
        _retirePanel.SetActive(false);
    }

}
