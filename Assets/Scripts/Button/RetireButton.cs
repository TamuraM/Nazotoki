using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RetireButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("���̃{�^���̉摜")] Image _image;
    [Tooltip("���̃{�^���̌��̐F")] Color _imageColor;
    [SerializeField, Header("���^�C�A���邩�̉��")] GameObject _retire;

    void Start()
    {
        _image = GetComponent<Image>();
        _imageColor = _image.color;
        _retire.SetActive(false);
    }

    /// <summary>�N���b�N�����Ƃ��ɁA���^�C�A���邩�̉�ʂ��o�Ă���</summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        _image.color = _imageColor;
        _retire.SetActive(true);
    }

    /// <summary>�}�E�X�J�[�\������������A�����Â�����</summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = _imageColor - new Color(55 / 255f, 55 / 255f, 55 / 255f, 0);
    }

    /// <summary>�}�E�X�J�[�\�����o����A���̐F�ɖ߂�</summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = _imageColor;
    }

    
}
