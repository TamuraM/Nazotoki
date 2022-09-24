using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class RetireGameButton : ButtonBase
{
    [SerializeField, Header("�X�g�[���[�̔w�i")] GameObject _storyPanel;
    bool _isClicked;
    public bool IsClicked { get => _isClicked; }

    /// <summary>�N���b�N���ꂽ��A��ʂ��t�F�[�h�A�E�g���ă^�C�g���ɖ߂�</summary>
    public override void Click()
    {
        _isClicked = true;
        _storyPanel.SetActive(true);
        _storyPanel.GetComponent<Image>().DOFade(1, 2.0f).SetEase(Ease.Linear).OnComplete(() => SceneManager.LoadScene("Game")).SetAutoKill();
    }

}
