using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key8 : NumberKeyBase
{
    public override void Click()
    {
        base._numberKeyController.Num = $"{_numberKeyController.Num}8";
    }
}
