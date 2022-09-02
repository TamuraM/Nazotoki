using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key1 : NumberKeyBase
{
    public override void Click()
    {
        base._numberKeyController.Num = $"{_numberKeyController.Num}1";
    }
}
