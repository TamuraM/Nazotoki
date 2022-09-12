using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>考えるボタン　ヒントの管理</summary>
public class ThinkingButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("考えるボタン")] Image _image;
    [SerializeField, Header("メインカメラ")] GameObject _mainCamera;
    [Tooltip("カメラの角度")] float _angle;
    //[SerializeField, Header("テキストボックス")] GameObject _textBox;

    //どのヒントを見たのか情報
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

        //ドア側を向いている時
        if(_angle == 0)
        {

            //ラス謎が出てたら普通
            if(GameManager.instance._clearState == GameManager.Clear.LastStageStart)
            {
                _image.color = new Color(255, 255, 255, 255);
            }
            //出てなかったら半透明
            else
            {
                _image.color = new Color(255, 255, 255, 50);
            }

        }
        //それ以外では普通
        else
        {
            _image.color = new Color(255, 255, 255, 255);
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance._gameMode = GameManager.GameMode.Thinking;

        //左側の謎
        if(_angle == -90)
        {

            //ヒントを見たことがなければ第1ヒント表示
            if((_hint & Hint.LeftHint1) != Hint.LeftHint1)
            {
                _hint |= Hint.LeftHint1;
            }
            //見たことがあれば第2ヒント表示
            else if((_hint & Hint.LeftHint1) == Hint.LeftHint1)
            {
                _hint |= Hint.LeftHint2;
            }
            //どっちも見たことあったら「もう考えることはないようだ」と表示
            else if((_hint & Hint.LeftHint1) == Hint.LeftHint1 && (_hint & Hint.LeftHint2) == Hint.LeftHint2)
            {
                //テキスト変更
            }

        }
        //後ろ側の謎
        else if(_angle == 180)
        {

            //ヒントを見たことがなければ第1ヒント表示
            if ((_hint & Hint.BackHint1) != Hint.BackHint1)
            {
                _hint |= Hint.BackHint1;
            }
            //見たことがあれば第2ヒント表示
            else if ((_hint & Hint.BackHint1) == Hint.BackHint1)
            {
                _hint |= Hint.BackHint2;
            }
            //どっちも見たことあったら「もう考えることはないようだ」と表示
            else if ((_hint & Hint.BackHint1) == Hint.BackHint1 && (_hint & Hint.BackHint2) == Hint.BackHint2)
            {
                //テキスト変更
            }

        }
        //右側の謎
        else if(_angle == 90)
        {

            //ヒントを見たことがなければ第1ヒント表示
            if ((_hint & Hint.RightHint1) != Hint.RightHint1)
            {
                _hint |= Hint.RightHint1;
            }
            //見たことがあれば第2ヒント表示
            else if ((_hint & Hint.RightHint1) == Hint.RightHint1)
            {
                _hint |= Hint.RightHint2;
            }
            //どっちも見たことあったら「もう考えることはないようだ」と表示
            else if ((_hint & Hint.RightHint1) == Hint.RightHint1 && (_hint & Hint.RightHint2) == Hint.RightHint2)
            {
                //テキスト変更
            }

        }
        //ドア側の謎
        else
        {
            //ラス謎が出てる時だけ
            if(GameManager.instance._clearState == GameManager.Clear.LastStageStart)
            {

                //ヒントを見たことがなければ第1ヒント表示
                if ((_hint & Hint.DoorHint1) != Hint.DoorHint1)
                {
                    _hint |= Hint.DoorHint1;
                }
                //見たことがあれば第2ヒント表示
                else if ((_hint & Hint.DoorHint1) == Hint.DoorHint1)
                {
                    _hint |= Hint.DoorHint2;
                }
                //どっちも見たことあったら「もう考えることはないようだ」と表示
                else if ((_hint & Hint.DoorHint1) == Hint.DoorHint1 && (_hint & Hint.DoorHint2) == Hint.DoorHint2)
                {
                    //テキスト変更
                }

            }

        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        //ドア側を向いている時
        if (_angle == 0)
        {
            //ラス謎が出てたら少し暗く
            if (GameManager.instance._clearState == GameManager.Clear.LastStageStart)
            {
                _image.color = new Color(200, 200, 200, 255);
            }
            //出てなかったらそのまま
            else
            {
                return;
            }

        }
        //それ以外では少し暗く
        else
        {
            _image.color = new Color(200, 200, 200, 255);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        //ドア側を向いている時
        if (_angle == 0)
        {
            //ラス謎が出てたら元の色に戻る
            if (GameManager.instance._clearState == GameManager.Clear.LastStageStart)
            {
                _image.color = new Color(255, 255, 255, 255);
            }
            //出てなかったらそのまま
            else
            {
                return;
            }

        }
        //それ以外では元の色に戻る
        else
        {
            _image.color = new Color(255, 255, 255, 255);
        }

    }

}
