using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// �q���g�{�^���̊��N���X
/// </summary>
public class HintButtonBase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Header("�q���g�e�L�X�g�{�b�N�X")] GameObject _textBox;
    [SerializeField, Header("�q���g�e�L�X�g")] protected Text _hintText;
    [SerializeField, Header("�q���g�̃e�L�X�g�f�[�^")] TextAsset _hintTextFile;
    protected Tween _tween = default;
    protected string[] _hint;
    protected string _LeftHint1;
    protected string _LeftHint2;
    protected string _BackHint1;
    protected string _BackHint2;
    protected string _RightHint1;
    protected string _RightHint2;
    protected string _DoorHint1;
    protected string _DoorHint2;
    [Tooltip("���̉摜�̐F")] Color _imageColor;
    [Tooltip("���̉摜")] Image _image;
    protected bool _endReadHint;

    protected void Start()
    {
        _hint = _hintTextFile.text.Split(char.Parse("\n"));
        _LeftHint1 = $"{_hint[1]}\n{_hint[2]}\n{_hint[3]}";
        _LeftHint2 = $"{_hint[5]}\n{_hint[6]}\n{_hint[7]}";
        _BackHint1 = $"{_hint[9]}\n{_hint[10]}\n{_hint[11]}";
        _BackHint2 = $"{_hint[13]}\n{_hint[14]}\n{_hint[15]}";
        _RightHint1 = $"{_hint[17]}\n{_hint[18]}\n{_hint[19]}";
        _RightHint2 = $"{_hint[21]}\n{_hint[22]}\n{_hint[23]}";
        _DoorHint1 = $"{_hint[25]}\n{_hint[26]}\n{_hint[27]}";
        _DoorHint2 = $"{_hint[29]}\n{_hint[30]}\n{_hint[31]}";
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

        //�q���g�����Ă鎞�ɃN���b�N������A��C�ɍŌ�܂ōs��
        if(GameManager.instance._gameMode == GameManager.GameMode.Thinking && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _hintText.DOComplete();
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

        if (GameManager.instance._gameMode == GameManager.GameMode.PlayGame)
        {
            _image.color = _imageColor - new Color(55 / 255f, 55 / 255f, 55 / 255f, 0);
        }
            
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

    public virtual IEnumerator Delay()
    {
        Debug.LogError("�p����ŃI�[�o�[���[�h���w�肵�Ă�������");
        yield return new WaitForSeconds(0.1f);
    }

}
