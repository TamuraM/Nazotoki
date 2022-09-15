using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetireReturnButton : ButtonBase
{
    [SerializeField, Header("���^�C�A��ʂ̔w�i")] GameObject _retirePanel;

    public override void Click()
    {
        Image.color = ImageColor;
        _retirePanel.SetActive(false);
    }

}
