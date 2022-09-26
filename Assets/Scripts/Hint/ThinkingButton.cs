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
    string _LeftHint1;
    string _LeftHint2;
    string _BackHint1;
    string _BackHint2;
    string _RightHint1;
    string _RightHint2;
    string _DoorHint1;
    string _DoorHint2;
    bool _endReadHint;
    [SerializeField, Header("�q���g�{�^��")] List<GameObject> _hintButtons;

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
        _LeftHint1 = $"{_hint[1]}\n{_hint[2]}\n{_hint[3]}";
        _LeftHint2 = $"{_hint[5]}\n{_hint[6]}\n{_hint[7]}";
        _BackHint1 = $"{_hint[9]}\n{_hint[10]}\n{_hint[11]}";
        _BackHint2 = $"{_hint[13]}\n{_hint[14]}\n{_hint[15]}";
        _RightHint1 = $"{_hint[17]}\n{_hint[18]}\n{_hint[19]}";
        _RightHint2 = $"{_hint[21]}\n{_hint[22]}\n{_hint[23]}";
        _DoorHint1 = $"{_hint[25]}\n{_hint[26]}\n{_hint[27]}";
        _DoorHint2 = $"{_hint[29]}\n{_hint[30]}\n{_hint[31]}";
        _hintText.text = "";
    }

    void Update()
    {
        _angle = _mainCamera.transform.rotation.eulerAngles.y;

        //�J�����̊p�x�ɂ���ē����x��ς���
        if (_change)
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

        //------�q���g�{�^���̕\��------
        //�����̓�
        if (_angle == 270)
        {

            if((_hintCheckList & HintCheckList.LeftHint1) == HintCheckList.LeftHint1)
            {
                _hintButtons[0].SetActive(true);
            }
            
            if((_hintCheckList & HintCheckList.LeftHint2) == HintCheckList.LeftHint2)
            {
                _hintButtons[1].SetActive(true);
            }

        }
        else
        {
            _hintButtons[0].SetActive(false);
            _hintButtons[1].SetActive(false);
        }

        //��둤�̓�
        if (_angle == 180)
        {

            if ((_hintCheckList & HintCheckList.BackHint1) == HintCheckList.BackHint1)
            {
                _hintButtons[2].SetActive(true);
            }
            
            if ((_hintCheckList & HintCheckList.LeftHint2) == HintCheckList.LeftHint2)
            {
                _hintButtons[3].SetActive(true);
            }

        }
        else
        {
            _hintButtons[2].SetActive(false);
            _hintButtons[3].SetActive(false);
        }

        //�E���̓�
        if (_angle == 90)
        {
            
            if ((_hintCheckList & HintCheckList.RightHint1) == HintCheckList.RightHint1)
            {
                _hintButtons[4].SetActive(true);
            }
            
            if ((_hintCheckList & HintCheckList.RightHint2) == HintCheckList.RightHint2)
            {
                _hintButtons[5].SetActive(true);
            }

        }
        else
        {
            _hintButtons[4].SetActive(false);
            _hintButtons[5].SetActive(false);
        }

        //�h�A���̓�
        if (_angle == 0)
        {

            //���X�䂪�o�Ă鎞����
            if (GameManager.instance._clearState == GameManager.Clear.LastStageStart)
            {

                if ((_hintCheckList & HintCheckList.DoorHint1) == HintCheckList.DoorHint1)
                {
                    _hintButtons[6].SetActive(true);
                }

                if ((_hintCheckList & HintCheckList.DoorHint2) == HintCheckList.DoorHint2)
                {
                    _hintButtons[7].SetActive(true);
                }

            }

        }
        else
        {
            _hintButtons[6].SetActive(false);
            _hintButtons[7].SetActive(false);
        }
        //------�����܂�------

        //�q���g��ǂݏI����Ă��āA���N���b�N������e�L�X�g�{�b�N�X��������
        if (_endReadHint && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _hintText.text = "";
            _textBox.SetActive(false);
            GameManager.instance._gameMode = GameManager.GameMode.PlayGame;
            _endReadHint = false;
        }

        //�q���g�����Ă鎞�ɃN���b�N������A��C�ɍŌ�܂ōs��
        if (GameManager.instance._gameMode == GameManager.GameMode.Thinking && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _hintText.DOComplete();
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

                if(GameManager.instance._clearState != GameManager.Clear.LastStageStart)
                {
                    // Debug.Log("�Ђ���");
                    Thinking(HintCheckList.LeftHint1, HintCheckList.LeftHint2, GameManager.Clear.FirstStageClear, _LeftHint1, _LeftHint2, _hintButtons[0], _hintButtons[1]);
                }
                else
                {
                    _textBox.SetActive(true);
                    GameManager.instance._gameMode = GameManager.GameMode.Thinking;
                    _hintText.DOText("�����l���邱�Ƃ͂Ȃ��悤��", 0.1f * 13).SetEase(Ease.Linear).OnComplete(() => _endReadHint = true).SetAutoKill();
                }

            }
            //��둤�̓�
            else if (_angle == 180)
            {

                if (GameManager.instance._clearState != GameManager.Clear.LastStageStart)
                {
                    //Debug.Log("������");
                    Thinking(HintCheckList.BackHint1, HintCheckList.BackHint2, GameManager.Clear.SecondStageClear, _BackHint1, _BackHint2, _hintButtons[2], _hintButtons[3]);
                }
                else
                {
                    _textBox.SetActive(true);
                    GameManager.instance._gameMode = GameManager.GameMode.Thinking;
                    _hintText.DOText("�����l���邱�Ƃ͂Ȃ��悤��", 0.1f * 13).SetEase(Ease.Linear).OnComplete(() => _endReadHint = true).SetAutoKill();
                }

            }
            //�E���̓�
            else if (_angle ==�@90)
            {

                if (GameManager.instance._clearState != GameManager.Clear.LastStageStart)
                {
                    //Debug.Log("�݂�");
                    Thinking(HintCheckList.RightHint1, HintCheckList.RightHint2, GameManager.Clear.ThirdStageClear, _RightHint1, _RightHint2, _hintButtons[4], _hintButtons[5]);
                }
                else
                {
                    _textBox.SetActive(true);
                    GameManager.instance._gameMode = GameManager.GameMode.Thinking;
                    _hintText.DOText("�����l���邱�Ƃ͂Ȃ��悤��", 0.1f * 13).SetEase(Ease.Linear).OnComplete(() => _endReadHint = true).SetAutoKill();
                }

            }
            //�h�A���̓�
            else if (_angle == 0)
            {
                //Debug.Log("�h�A");
                //���X�䂪�o�Ă鎞����
                if (GameManager.instance._clearState == GameManager.Clear.LastStageStart)
                {
                    Thinking(HintCheckList.DoorHint1, HintCheckList.DoorHint2, GameManager.Clear.LastStageClear, _DoorHint1, _DoorHint2, _hintButtons[6], _hintButtons[7]);
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
    public void Thinking(HintCheckList first, HintCheckList second, GameManager.Clear clear, string hint1, string hint2, GameObject hint1buttn, GameObject hint2buttn)
    {
        GameManager.instance._gameMode = GameManager.GameMode.Thinking;

        //�܂��N���A���ĂȂ�������q���g�\��
        if ((GameManager.instance._clearState & clear) != clear)
        {
            _image.color = _imageColor - new Color(100 / 255f, 100 / 255f, 100 / 255f, 0);

            //�q���g���������Ƃ��Ȃ���Α�1�q���g�\��
            if ((_hintCheckList & first) != first)
            {
                ShowHint(hint1, first, hint1buttn);
            }
            //�������Ƃ�����Α�2�q���g�\��
            else if ((_hintCheckList & first) == first && (_hintCheckList & second) != second)
            {
                ShowHint(hint2, second, hint2buttn);
            }
            //�ǂ������������Ƃ�������u�����l���邱�Ƃ͂Ȃ��悤���v�ƕ\��
            else if ((_hintCheckList & first) == first && (_hintCheckList & second) == second)
            {
                //�e�L�X�g�ύX
                _textBox.SetActive(true);
                _hintText.DOText("�����l���邱�Ƃ͂Ȃ��悤��", 0.1f * 13).SetEase(Ease.Linear).OnComplete(() => _endReadHint = true).SetAutoKill();
            }

        }
        //�����N���A���Ă�����q���g�o���Ȃ�
        else if((GameManager.instance._clearState & clear) == clear)
        {
            _textBox.SetActive(true);
            _hintText.DOText("�����l���邱�Ƃ͂Ȃ��悤��", 0.1f * 13).SetEase(Ease.Linear).OnComplete(() => _endReadHint = true).SetAutoKill();
        }

    }

    /// <summary>�q���g�e�L�X�g��\������</summary>
    /// <param name="s"></param>
    public void ShowHint(string s, HintCheckList hintCheckList = default , GameObject hintbutton = default)
    {
        _textBox.SetActive(true);
        _hintText.DOText(s, 0.1f * s.Length).SetEase(Ease.Linear).OnComplete(() => { _endReadHint = true; _hintCheckList |= hintCheckList; hintbutton.SetActive(true); }).SetAutoKill();
    }
}
