using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NumberKeyBase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("���ꂪ���Ă�I�u�W�F�N�g�̃C���[�W�R���|�[�l���g")] Image _image;
    [Tooltip("���̃C���[�W�̐F")] Color _imageColor;
    [SerializeField] public NumberKeyController _numberKeyController;

    private void Awake()
    {
        _image = this.gameObject.GetComponent<Image>();
        _imageColor = _image.color;
    }

    /// <summary>�{�^�����������Ƃ��̏���</summary>
    public virtual void Click()
    {
        Debug.LogError("�p����̔h���N���X�Ŋ֐����`���Ă��������B");
    }

    public void OnPointerClick(PointerEventData eventData) // �{�^����������A���̌�h���b�O���삪���邱�ƂȂ��{�^�����������
    {

        if(_numberKeyController.Num.Length < 4)
        {
            Click();
        }
        else
        {
            return;
        }
        
    }

    /// <summary>�{�^���̏�ɃJ�[�\�����������摜���Â�����</summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) //�{�^���͈̔͂Ƀ}�E�X�J�[�\��������
    {
        _image.color = new Color(200, 200, 200);
    }

    /// <summary>�{�^���̏ォ��J�[�\�����Ȃ��Ȃ�����摜�����ɖ߂�</summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) //�{�^���͈̔͂���}�E�X�J�[�\�����o��
    {
        _image.color = _imageColor;
    }
}
