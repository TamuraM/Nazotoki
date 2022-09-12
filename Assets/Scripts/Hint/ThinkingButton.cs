using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>�l����{�^���@�q���g�̊Ǘ�</summary>
public class ThinkingButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("�l����{�^��")] Image _image;
    [SerializeField, Header("���C���J����")] GameObject _mainCamera;
    [Tooltip("�J�����̊p�x")] float _angle;
    //[SerializeField, Header("�e�L�X�g�{�b�N�X")] GameObject _textBox;

    //�ǂ̃q���g�������̂����
    public enum Hint
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

    public Hint _hint;

    void Start()
    {
        _image = GetComponent<Image>();
        //_angle = _mainCamera.GetComponent<Transform>().rotation.y;
    }

    void Update()
    {
        _angle = _mainCamera.transform.rotation.y;

        //�h�A���������Ă��鎞
        if(_angle == 0)
        {

            //���X�䂪�o�Ă��畁��
            if(GameManager.instance._clearState == GameManager.Clear.LastStageStart)
            {
                _image.color = new Color(255, 255, 255, 255);
            }
            //�o�ĂȂ������甼����
            else
            {
                _image.color = new Color(255, 255, 255, 50);
            }

        }
        //����ȊO�ł͕���
        else
        {
            _image.color = new Color(255, 255, 255, 255);
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance._gameMode = GameManager.GameMode.Thinking;

        //�����̓�
        if(_angle == -90)
        {

            //�q���g���������Ƃ��Ȃ���Α�1�q���g�\��
            if((_hint & Hint.LeftHint1) != Hint.LeftHint1)
            {
                _hint |= Hint.LeftHint1;
            }
            //�������Ƃ�����Α�2�q���g�\��
            else if((_hint & Hint.LeftHint1) == Hint.LeftHint1)
            {
                _hint |= Hint.LeftHint2;
            }
            //�ǂ������������Ƃ�������u�����l���邱�Ƃ͂Ȃ��悤���v�ƕ\��
            else if((_hint & Hint.LeftHint1) == Hint.LeftHint1 && (_hint & Hint.LeftHint2) == Hint.LeftHint2)
            {
                //�e�L�X�g�ύX
            }

        }
        //��둤�̓�
        else if(_angle == 180)
        {

            //�q���g���������Ƃ��Ȃ���Α�1�q���g�\��
            if ((_hint & Hint.BackHint1) != Hint.BackHint1)
            {
                _hint |= Hint.BackHint1;
            }
            //�������Ƃ�����Α�2�q���g�\��
            else if ((_hint & Hint.BackHint1) == Hint.BackHint1)
            {
                _hint |= Hint.BackHint2;
            }
            //�ǂ������������Ƃ�������u�����l���邱�Ƃ͂Ȃ��悤���v�ƕ\��
            else if ((_hint & Hint.BackHint1) == Hint.BackHint1 && (_hint & Hint.BackHint2) == Hint.BackHint2)
            {
                //�e�L�X�g�ύX
            }

        }
        //�E���̓�
        else if(_angle == 90)
        {

            //�q���g���������Ƃ��Ȃ���Α�1�q���g�\��
            if ((_hint & Hint.RightHint1) != Hint.RightHint1)
            {
                _hint |= Hint.RightHint1;
            }
            //�������Ƃ�����Α�2�q���g�\��
            else if ((_hint & Hint.RightHint1) == Hint.RightHint1)
            {
                _hint |= Hint.RightHint2;
            }
            //�ǂ������������Ƃ�������u�����l���邱�Ƃ͂Ȃ��悤���v�ƕ\��
            else if ((_hint & Hint.RightHint1) == Hint.RightHint1 && (_hint & Hint.RightHint2) == Hint.RightHint2)
            {
                //�e�L�X�g�ύX
            }

        }
        //�h�A���̓�
        else
        {
            //���X�䂪�o�Ă鎞����
            if(GameManager.instance._clearState == GameManager.Clear.LastStageStart)
            {

                //�q���g���������Ƃ��Ȃ���Α�1�q���g�\��
                if ((_hint & Hint.DoorHint1) != Hint.DoorHint1)
                {
                    _hint |= Hint.DoorHint1;
                }
                //�������Ƃ�����Α�2�q���g�\��
                else if ((_hint & Hint.DoorHint1) == Hint.DoorHint1)
                {
                    _hint |= Hint.DoorHint2;
                }
                //�ǂ������������Ƃ�������u�����l���邱�Ƃ͂Ȃ��悤���v�ƕ\��
                else if ((_hint & Hint.DoorHint1) == Hint.DoorHint1 && (_hint & Hint.DoorHint2) == Hint.DoorHint2)
                {
                    //�e�L�X�g�ύX
                }

            }

        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        //�h�A���������Ă��鎞
        if (_angle == 0)
        {
            //���X�䂪�o�Ă��班���Â�
            if (GameManager.instance._clearState == GameManager.Clear.LastStageStart)
            {
                _image.color = new Color(200, 200, 200, 255);
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
            _image.color = new Color(200, 200, 200, 255);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        //�h�A���������Ă��鎞
        if (_angle == 0)
        {
            //���X�䂪�o�Ă��猳�̐F�ɖ߂�
            if (GameManager.instance._clearState == GameManager.Clear.LastStageStart)
            {
                _image.color = new Color(255, 255, 255, 255);
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
            _image.color = new Color(255, 255, 255, 255);
        }

    }

}
