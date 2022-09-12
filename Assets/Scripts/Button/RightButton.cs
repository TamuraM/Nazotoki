using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>���_���E�ɉ�]������X�N���v�g</summary>
public class RightButton : ButtonBase
{
    [Tooltip("���C���J�����I�u�W�F�N�g")] GameObject _mainCamera;
    [SerializeField] ThinkingButton _thinkingButton;

    private void Start()
    {
        _mainCamera = GameObject.Find("Main Camera");
    }

    public override void Click()
    {
        _thinkingButton.AngleChange = true;
        _mainCamera.transform.Rotate(0, 90, 0);
    }
}
