using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// �q���g�{�^���̊��N���X
/// </summary>
public class HintButtonBase : ButtonBase
{
    [SerializeField, Header("�q���g�e�L�X�g�{�b�N�X")] GameObject _textBox;
    [SerializeField, Header("�q���g��\��������e�L�X�g")] Text _hintText;
    protected Text HintText { get => _hintText; }
    [SerializeField, Header("�\��������q���g�̃e�L�X�g")] TextAsset _hint;
    protected TextAsset Hint { get => _hint; }
    protected bool _endReadHint;

    protected void Start()
    {
        gameObject.SetActive(false);
    }

    protected void Update()
    {

        //�q���g��ǂݏI����Ă��āA���N���b�N������e�L�X�g�{�b�N�X��������
        if (_endReadHint && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _hintText.text = "";
            _textBox.SetActive(false);
            GameManager.Instance._gameMode = GameManager.GameMode.PlayGame;
            _endReadHint = false;
        }

        //�q���g�����Ă鎞�ɃN���b�N������A��C�ɍŌ�܂ōs��
        if(GameManager.Instance._gameMode == GameManager.GameMode.Thinking && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _hintText.DOComplete();
        }

    }

    /// <summary>�N���b�N������摜���Â����ăq���g�\��</summary>
    /// <param name="eventData"></param>
    public override void OnPointerClick(PointerEventData eventData)
    {

        if(GameManager.Instance._gameMode == GameManager.GameMode.PlayGame)
        {
            GameManager.Instance._gameMode = GameManager.GameMode.Thinking;
            Image.color = new Color(155 / 255f, 155 / 255f, 155 / 255f, 1);
            _textBox.SetActive(true);
            Click();
        }
        
    }

    /// <summary>�}�E�X�J�[�\�����������班���Â�</summary>
    /// <param name="eventData"></param>
    public override void OnPointerEnter(PointerEventData eventData)
    {

        if (GameManager.Instance._gameMode == GameManager.GameMode.PlayGame)
        {
            Image.color = new Color(200 / 255f, 200 / 255f, 200 / 255f, 1);
        }
            
    }

    /// <summary>
    ///�@���ׂĕ\�����ꂽ��ɃN���b�N������e�L�X�g�{�b�N�X�������邪�A
    ///�@�q���g�̓r���ŃN���b�N�������C�ɍŌ�܂ōs���Ă��̂܂܏����Ă��܂����߁A
    ///�@�ǂݏI���������0.1�b�҂��Ă���I���ɂ���������R���[�`�����g��
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator ShowHint()
    {
        Debug.LogError("�p����ŃI�[�o�[���[�h���w�肵�Ă�������");
        yield return new WaitForSeconds(0.1f);
    }

}
