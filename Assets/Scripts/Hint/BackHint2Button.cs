using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackHint2Button : HintButtonBase
{

    public override void Click()
    {
        StartCoroutine(Delay());
    }

    public override IEnumerator Delay()
    {
        yield return _hintText.DOText(_BackHint2, 0.1f * _BackHint2.Length).SetEase(Ease.Linear).SetAutoKill().WaitForCompletion();
        yield return new WaitForSeconds(0.1f);
        _endReadHint = true;
    }

}
