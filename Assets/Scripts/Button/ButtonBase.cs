using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>�{�^���̊��N���X</summary>
public class ButtonBase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("���ꂪ���Ă�I�u�W�F�N�g�̃C���[�W�R���|�[�l���g")] Image _image;
    [Tooltip("���̃C���[�W�̐F")] Color _imageColor;

    protected Image Image
    { get => _image; set => _image = value; }

    protected Color ImageColor
    { get => _imageColor; set => _imageColor = value; }

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

    /// <summary>�{�^�����N���b�N������摜�������ƈÂ�����
    /// <param name="eventData"></param>
    public virtual void OnPointerClick(PointerEventData eventData) // �{�^����������A���̌�h���b�O���삪���邱�ƂȂ��{�^�����������
    {
        _image.color = new Color(155 / 255f, 155 / 255f, 155 / 255f, 1);
        Click();
    }

    /// <summary>�{�^���̏�ɃJ�[�\�����������摜���Â�����</summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerEnter(PointerEventData eventData) //�{�^���͈̔͂Ƀ}�E�X�J�[�\��������
    {
        _image.color = new Color(200 / 255f, 200 / 255f, 200 / 255f, 1);
    }

    /// <summary>�{�^���̏ォ��J�[�\�����Ȃ��Ȃ�����摜�����ɖ߂�</summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) //�{�^���͈̔͂���}�E�X�J�[�\�����o��
    {
        _image.color = _imageColor;
    }
}
