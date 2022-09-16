using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RightHint1Button : HintButtonBase
{

    public override void Click()
    {
        StartCoroutine(Delay());
    }

    public override IEnumerator Delay()
    {
        yield return _hintText.DOText(_RightHint1, 0.1f * _RightHint1.Length).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(0.1f);
        _endReadHint = true;
    }

}
