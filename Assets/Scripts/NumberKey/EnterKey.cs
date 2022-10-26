using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnterKey : ButtonBase
{
    [SerializeField, Header("�������͉��")] GameObject _inputNumber;
    [SerializeField] Animator _numberKey;
    [SerializeField, Header("�h�A")] AudioSource _audio;
    [SerializeField, Header("�h�A")] Animator _door;
    [SerializeField] NumberKeyController _numberKeyController;

    public override void Click()
    {

        if (_numberKeyController.InputNumber.Length == 4)
        {

            if (_numberKeyController.InputNumber == _numberKeyController.Answer)
            {
                //�摜�̓����x��߂��āA�w�i������
                Image.color = ImageColor;
                _inputNumber.SetActive(false);
                GameManager.Instance._isFocused = false;
                //�h�A���J��
                _door.SetBool("EnterAnswer", true);
                _audio.Play();
                GameManager.Instance._clearState = GameManager.Clear.LastStageClear;
                GameManager.Instance._gameMode = GameManager.GameMode.GameClear;
            }
            else
            {
                StartCoroutine(Wrong());
            }

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
        _numberKeyController.InputNumber = "";
        _numberKey.SetBool("Wrong", false);
    }

}
