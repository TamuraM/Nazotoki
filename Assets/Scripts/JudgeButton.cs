using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>���͂��ꂽ�e�L�X�g���������ǂ������肷��X�N���v�g</summary>
public class JudgeButton : ButtonBase
{
    [Tooltip("���͂��ꂽ�e�L�X�g")] Text _inputText;

    // Start is called before the first frame update
    void Start()
    {
        _inputText = GameObject.Find("InputText").GetComponent<Text>();
    }

    public override void Click()
    {
        //���͂��ꂽ�e�L�X�g���������ǂ������肷��
        base.Click();
    }
}
