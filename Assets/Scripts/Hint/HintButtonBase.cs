using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �q���g�{�^���̊��N���X
/// </summary>
public class HintButtonBase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Header("�q���g�e�L�X�g�{�b�N�X")] GameObject _textBox;
    [SerializeField, Header("�q���g�e�L�X�g")] protected Text _hintText;
    [SerializeField, Header("�q���g�̃e�L�X�g�f�[�^")] TextAsset _hintTextFile;
    protected string[] _hint;
    [Tooltip("���̉摜�̐F")] Color _imageColor;
    [Tooltip("���̉摜")] Image _image;
    protected bool _endReadHint;

    protected void Start()
    {
        _hint = _hintTextFile.text.Split(char.Parse("\n"));
        _image = GetComponent<Image>();
        _imageColor = _image.color;
        gameObject.SetActive(false);
    }

    protected void Update()
    {

        //�q���g��ǂݏI����Ă��āA���N���b�N������e�L�X�g�{�b�N�X��������
        if (_endReadHint && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _hintText.text = "";
            _textBox.SetActive(false);
            GameManager.instance._gameMode = GameManager.GameMode.PlayGame;
            _endReadHint = false;
        }

    }

    /// <summary>�N���b�N������摜���Â����ăq���g�\��</summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {

        if(GameManager.instance._gameMode == GameManager.GameMode.PlayGame)
        {
            GameManager.instance._gameMode = GameManager.GameMode.Thinking;
            _image.color = _imageColor - new Color(100 / 255f, 100 / 255f, 100 / 255f, 0);
            _textBox.SetActive(true);
            Click();
        }
        
    }

    /// <summary>�}�E�X�J�[�\�����������班���Â�</summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = _imageColor - new Color(55 / 255f, 55 / 255f, 55 / 255f, 0);
    }

    /// <summary>�}�E�X���o���猳�̐F��</summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = _imageColor;
    }

    /// <summary>
    /// �N���b�N�����Ƃ��ɁA�q���g�̏�Ԃƃ{�^���ɑΉ������q���g��\������֐�
    /// </summary>
    public virtual void Click()
    {
        Debug.LogError("�p����ŃI�[�o�[���[�h���w�肵�Ă��������B");
    }
}
