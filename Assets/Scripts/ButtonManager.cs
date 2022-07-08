using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>���_��ς���{�^�������X�N���v�g</summary>
public class ButtonManager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Header("���C���J����"), Tooltip("���C���J�����I�u�W�F�N�g")] Camera _mainCamera;
    [Tooltip("���ꂪ���Ă�I�u�W�F�N�g�̃C���[�W�R���|�[�l���g")] Image _image;

    private void Start()
    {
        _image = this.gameObject.GetComponent<Image>();
    }

    /// <summary>�{�^������������J���������</summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) // �{�^����������A���̌�h���b�O���삪���邱�ƂȂ��{�^�����������
    {
        //���{�^���������獶�ɉ��
        if(this.gameObject.name == "LeftButton")
        {
                _mainCamera.transform.Rotate(0, -90, 0);
        }
        //�E�{�^����������E�ɉ��
        if (this.gameObject.name == "RightButton")
        {
            _mainCamera.transform.Rotate(0, 90, 0);
        }
    }

    /// <summary>�{�^���̏�ɃJ�[�\����������瓧���x��������</summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) //�{�^���͈̔͂Ƀ}�E�X�J�[�\��������
    {
        _image.color += new Color(0, 0, 0, 55);
    }

    /// <summary>�{�^���̏ォ��J�[�\�����Ȃ��Ȃ����瓧���x���グ��</summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) //�{�^���͈̔͂���}�E�X�J�[�\�����o��
    {
        _image.color -= new Color(0, 0, 0, 55);
    }
}
