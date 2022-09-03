using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnterKey : NumberKeyBase
{
    [SerializeField, Header("数字入力画面")] GameObject _inputNumber;
    [SerializeField] Animator _numberKey;
    [SerializeField, Header("ドア")] Animator _door;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (_numberKeyController.Num.Length == 4)
        {
            Click();
        }
        else
        {
            return;
        }
    }

    public override void Click()
    {
        if (_numberKeyController.Num == _numberKeyController.Answer)
        {
            //画像の透明度を戻して、背景を消す
            Image.color = ImageColor;
            //ドアが開く
            _door.SetBool("EnterAnswer", true);
            _inputNumber.SetActive(false);
            GameManager.instance._isFocused = false;
            GameManager.instance._clearState = GameManager.Clear.LastStageClear;
        }
        else
        {
            StartCoroutine(Wrong());
        }
    }

    IEnumerator Wrong()
    {
        _numberKey.SetBool("Wrong", true);
        yield return new WaitForSeconds(0.3f);
        _numberKeyController.Num = "";
        _numberKey.SetBool("Wrong", false);
    }
}
