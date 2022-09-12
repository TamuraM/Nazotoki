using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>�l����{�^���@�q���g�̊Ǘ�</summary>
public class ThinkingButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("�l����{�^��")] Image _image;
    [Tooltip("���̐F")] Color _imageColor;
    [SerializeField, Header("���C���J����")] GameObject _mainCamera;
    [Tooltip("�J�����̊p�x")] float _angle;
    bool _change = true;
    public bool AngleChange { get => _change; set => _change = value; }
    [SerializeField, Header("�e�L�X�g�{�b�N�X"), Tooltip("�e�L�X�g�{�b�N�X�̃Q�[���I�u�W�F�N�g")] GameObject _textBox;
    [SerializeField, Header("�q���g�e�L�X�g"), Tooltip("�q���g��\������e�L�X�g")] Text _hintText;
    [SerializeField, Header("�q���g.text"), Tooltip("�q���g��������Ă�e�L�X�g")] TextAsset _hintTextFile;
    string[] _hint;
    bool _endReadHint;

    /// <summary>�ǂ̃q���g�������̂����</summary>
    public enum HintCheckList
    {
        LeftHint1 = 1 << 0,
        LeftHint2 = 1 << 1,
        BackHint1 = 1 << 2,
        BackHint2 = 1 << 3,
        RightHint1 = 1 << 4,
        RightHint2 = 1 << 5,
        DoorHint1 = 1 << 6,
        DoorHint2 = 1 << 7,
    }

    public HintCheckList _hintCheckList;

    void Start()
    {
        _image = GetComponent<Image>();
        _imageColor = GetComponent<Image>().color;
        _textBox.SetActive(false);
        _hint = _hintTextFile.text.Split(char.Parse("\n"));
        _hintText.text = "";
    }

    void Update()
    {
        _angle = _mainCamera.transform.rotation.eulerAngles.y;

        if(_change)
        {

            //�h�A���������Ă��鎞
            if (_angle == 0)
            {

                //���X�䂪�o�Ă��畁��
                if (GameManager.instance._clearState == GameManager.Clear.LastStageStart)
                {
                    _image.color = _imageColor;
                }
                //�o�ĂȂ������甼����
                else
                {
                    _image.color = _imageColor - new Color(0, 0, 0, 0.5f);
                }

            }
            //����ȊO�ł͕���
            else
            {
                _image.color = _imageColor;
            }

            _change = false;
        }
        
        //�q���g��ǂݏI����Ă��āA���N���b�N������e�L�X�g�{�b�N�X��������
        if(_endReadHint && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _hintText.text = "";
            _textBox.SetActive(false);
            GameManager.instance._gameMode = GameManager.GameMode.PlayGame;
            _endReadHint = false;
        }

    }

    /// <summary>�N���b�N���ꂽ�犯��̊p�x�ɍ��킹�ăq���g��\��</summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(_angle);

        if(GameManager.instance._gameMode == GameManager.GameMode.PlayGame)
        {

            //�����̓�
            if (_angle == 270)
            {
                    Debug.Log("�Ђ���");
                    Thinking(HintCheckList.LeftHint1, HintCheckList.LeftHint2, GameManager.Clear.FirstStageClear, _hint[0], _hint[1]);                
            }
            //��둤�̓�
            else if (_angle == 180)
            {
                Debug.Log("������");
                Thinking(HintCheckList.BackHint1, HintCheckList.BackHint2, GameManager.Clear.SecondStageClear, _hint[2], _hint[3]);
            }
            //�E���̓�
            else if (_angle ==�@90)
            {
                Debug.Log("�݂�");
                Thinking(HintCheckList.RightHint1, HintCheckList.RightHint2, GameManager.Clear.ThirdStageClear, _hint[4], _hint[5]);
            }
            //�h�A���̓�
            else if (_angle == 0)
            {
                Debug.Log("�h�A");
                //���X�䂪�o�Ă鎞����
                if (GameManager.instance._clearState == GameManager.Clear.LastStageStart)
                {
                    Thinking(HintCheckList.DoorHint1, HintCheckList.DoorHint2, GameManager.Clear.LastStageClear, _hint[6], _hint[7]);
                }

            }

        }
        

    }

    /// <summary>�}�E�X���������F���Â�����</summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {

        if(GameManager.instance._gameMode == GameManager.GameMode.PlayGame)
        {

            //�h�A���������Ă��鎞
            if (_angle == 0)
            {

                //���X�䂪�o�Ă��班���Â�
                if (GameManager.instance._clearState == GameManager.Clear.LastStageStart)
                {
                    _image.color = _imageColor - new Color(55 / 255f, 55 / 255f, 55 / 255f, 0);
                }
                //�o�ĂȂ������炻�̂܂�
                else
                {
                    return;
                }

            }
            //����ȊO�ł͏����Â�
            else
            {
                _image.color = _imageColor - new Color(55 / 255f, 55 / 255f, 55 / 255f, 0);
            }

        }

    }

    /// <summary>�}�E�X���o����F��߂�</summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {

        //�h�A���������Ă��鎞
        if (_angle == 0)
        {

            //���X�䂪�o�Ă��猳�̐F�ɖ߂�
            if (GameManager.instance._clearState == GameManager.Clear.LastStageStart)
            {
                _image.color = _imageColor;
            }
            //�o�ĂȂ������炻�̂܂�
            else
            {
                return;
            }

        }
        //����ȊO�ł͌��̐F�ɖ߂�
        else
        {
            _image.color = _imageColor;
        }

    }

    /// <summary>�l����{�^�����������Ƃ��Ƀq���g��\������</summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    public void Thinking(HintCheckList first, HintCheckList second, GameManager.Clear clear, string hint1, string hint2)
    {

        if(GameManager.instance._clearState != clear)
        {
            GameManager.instance._gameMode = GameManager.GameMode.Thinking;
            _image.color = _imageColor - new Color(100 / 255f, 100 / 255f, 100 / 255f, 0);

            //�q���g���������Ƃ��Ȃ���Α�1�q���g�\��
            if ((_hintCheckList & first) != first)
            {
                _hintCheckList |= first;
                ShowHint(hint1);
            }
            //�������Ƃ�����Α�2�q���g�\��
            else if ((_hintCheckList & first) == first && (_hintCheckList & second) != second)
            {
                _hintCheckList |= second;
                ShowHint(hint2);
            }
            //�ǂ������������Ƃ�������u�����l���邱�Ƃ͂Ȃ��悤���v�ƕ\��
            else if ((_hintCheckList & first) == first && (_hintCheckList & second) == second)
            {
                //�e�L�X�g�ύX
                ShowHint("�����l���邱�Ƃ͂Ȃ��悤��");
            }

        }

    }

    /// <summary>�q���g�e�L�X�g��\������</summary>
    /// <param name="s"></param>
    public void ShowHint(string s)
    {
        _textBox.SetActive(true);
        _hintText.DOText(s, 1.5f).SetEase(Ease.Linear).OnComplete(() => _endReadHint = true).SetAutoKill();
    }
}
