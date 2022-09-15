using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class RetireGameButton : ButtonBase
{
    [SerializeField, Header("ストーリーの背景")] GameObject _storyPanel;

    /// <summary>クリックされたら、画面がフェードアウトしてタイトルに戻る</summary>
    public override void Click()
    {
        _storyPanel.SetActive(true);
        _storyPanel.GetComponent<Image>().DOFade(1, 2.0f).SetEase(Ease.Linear).OnComplete(() => SceneManager.LoadScene("Game")).SetAutoKill();
    }

}
