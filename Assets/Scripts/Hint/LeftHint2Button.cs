using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LeftHint2Button : HintButtonBase
{

    public override void Click()
    {
        StartCoroutine(Delay());
    }

    public override IEnumerator Delay()
    {
        yield return _hintText.DOText(_LeftHint2, 0.1f * _LeftHint2.Length).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(0.1f);
        _endReadHint = true;
    }

}
