using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputNumberKey : ButtonBase
{
    [SerializeField] NumberKeyController _numberKeyController;
    [SerializeField, Range(0, 9)] int _number;

    public override void Click()
    {

        if(_numberKeyController.InputNumber.Length < 4)
        {
            _numberKeyController.InputNumber = $"{_numberKeyController.InputNumber}{_number}";
        }
        else
        {
            return;
        }
        
    }
}
