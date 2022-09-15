using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>考えるボタン　ヒントの管理</summary>
public class ThinkingButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("考えるボタン")] Image _image;
    [Tooltip("元の色")] Color _imageColor;
    [SerializeField, Header("メインカメラ")] GameObject _mainCamera;
    [Tooltip("カメラの角度")] float _angle;
    bool _change = true;
    public bool AngleChange { get => _change; set => _change = value; }
    [SerializeField, Header("テキストボックス"), Tooltip("テキストボックスのゲームオブジェクト")] GameObject _textBox;
    [SerializeField, Header("ヒントテキスト"), Tooltip("ヒントを表示するテキスト")] Text _hintText;
    [SerializeField, Header("ヒント.text"), Tooltip("ヒントが書かれてるテキスト")] TextAsset _hintTextFile;
    string[] _hint;
    bool _endReadHint;
    [SerializeField, Header("ヒントボタン")] List<GameObject> _hintButtons;

    /// <summary>どのヒントを見たのか情報</summary>
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

            //ドア側を向いている時
            if (_angle == 0)
            {

                //ラス謎が出てたら普通
                if (GameManager.instance._clearState == GameManager.Clear.LastStageStart)
                {
                    _image.color = _imageColor;
                }
                //出てなかったら半透明
                else
                {
                    _image.color = _imageColor - new Color(0, 0, 0, 0.5f);
                }

            }
            //それ以外では普通
            else
            {
                _image.color = _imageColor;
            }

            _change = false;
        }

        //------ヒントボタンの表示------
        //左側の謎
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

        //後ろ側の謎
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

        //右側の謎
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

        //ドア側の謎
        if (_angle == 0)
        {

            //ラス謎が出てる時だけ
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
        //------ここまで------

        //ヒントを読み終わっていて、左クリックしたらテキストボックスが消える
        if (_endReadHint && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _hintText.text = "";
            _textBox.SetActive(false);
            GameManager.instance._gameMode = GameManager.GameMode.PlayGame;
            _endReadHint = false;
        }

    }

    /// <summary>クリックされたら官らの角度に合わせてヒントを表示</summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(_angle);

        if(GameManager.instance._gameMode == GameManager.GameMode.PlayGame)
        {

            //左側の謎
            if (_angle == 270)
            {

                if(GameManager.instance._clearState != GameManager.Clear.LastStageStart)
                {
                    // Debug.Log("ひだり");
                    Thinking(HintCheckList.LeftHint1, HintCheckList.LeftHint2, GameManager.Clear.FirstStageClear, _hint[0], _hint[1], _hintButtons[0], _hintButtons[1]);
                }
                else
                {
                    _textBox.SetActive(true);
                    _hintText.DOText("もう考えることはないようだ", 1.5f).SetEase(Ease.Linear).OnComplete(() => _endReadHint = true).SetAutoKill();
                }

            }
            //後ろ側の謎
            else if (_angle == 180)
            {

                if (GameManager.instance._clearState != GameManager.Clear.LastStageStart)
                {
                    //Debug.Log("うしろ");
                    Thinking(HintCheckList.BackHint1, HintCheckList.BackHint2, GameManager.Clear.SecondStageClear, _hint[2], _hint[3], _hintButtons[2], _hintButtons[3]);
                }
                else
                {
                    _textBox.SetActive(true);
                    _hintText.DOText("もう考えることはないようだ", 1.5f).SetEase(Ease.Linear).OnComplete(() => _endReadHint = true).SetAutoKill();
                }

            }
            //右側の謎
            else if (_angle ==　90)
            {

                if (GameManager.instance._clearState != GameManager.Clear.LastStageStart)
                {
                    //Debug.Log("みぎ");
                    Thinking(HintCheckList.RightHint1, HintCheckList.RightHint2, GameManager.Clear.ThirdStageClear, _hint[4], _hint[5], _hintButtons[4], _hintButtons[5]);
                }
                else
                {
                    _textBox.SetActive(true);
                    _hintText.DOText("もう考えることはないようだ", 1.5f).SetEase(Ease.Linear).OnComplete(() => _endReadHint = true).SetAutoKill();
                }

            }
            //ドア側の謎
            else if (_angle == 0)
            {
                //Debug.Log("ドア");
                //ラス謎が出てる時だけ
                if (GameManager.instance._clearState == GameManager.Clear.LastStageStart)
                {
                    Thinking(HintCheckList.DoorHint1, HintCheckList.DoorHint2, GameManager.Clear.LastStageClear, _hint[6], _hint[7], _hintButtons[6], _hintButtons[7]);
                }

            }

        }
        

    }

    /// <summary>マウスが乗ったら色を暗くする</summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {

        if(GameManager.instance._gameMode == GameManager.GameMode.PlayGame)
        {

            //ドア側を向いている時
            if (_angle == 0)
            {

                //ラス謎が出てたら少し暗く
                if (GameManager.instance._clearState == GameManager.Clear.LastStageStart)
                {
                    _image.color = _imageColor - new Color(55 / 255f, 55 / 255f, 55 / 255f, 0);
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
                _image.color = _imageColor - new Color(55 / 255f, 55 / 255f, 55 / 255f, 0);
            }

        }

    }

    /// <summary>マウスが出たら色を戻す</summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {

        //ドア側を向いている時
        if (_angle == 0)
        {

            //ラス謎が出てたら元の色に戻る
            if (GameManager.instance._clearState == GameManager.Clear.LastStageStart)
            {
                _image.color = _imageColor;
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
            _image.color = _imageColor;
        }

    }

    /// <summary>考えるボタンを押したときにヒントを表示する</summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    public void Thinking(HintCheckList first, HintCheckList second, GameManager.Clear clear, string hint1, string hint2, GameObject hint1buttn, GameObject hint2buttn)
    {
        
        //まだクリアしてなかったらヒント表示
        if ((GameManager.instance._clearState & clear) != clear)
        {
            GameManager.instance._gameMode = GameManager.GameMode.Thinking;
            _image.color = _imageColor - new Color(100 / 255f, 100 / 255f, 100 / 255f, 0);

            //ヒントを見たことがなければ第1ヒント表示
            if ((_hintCheckList & first) != first)
            {
                ShowHint(hint1, first, hint1buttn);
            }
            //見たことがあれば第2ヒント表示
            else if ((_hintCheckList & first) == first && (_hintCheckList & second) != second)
            {
                ShowHint(hint2, second, hint2buttn);
            }
            //どっちも見たことあったら「もう考えることはないようだ」と表示
            else if ((_hintCheckList & first) == first && (_hintCheckList & second) == second)
            {
                //テキスト変更
                _textBox.SetActive(true);
                _hintText.DOText("もう考えることはないようだ", 1.5f).SetEase(Ease.Linear).OnComplete(() => _endReadHint = true).SetAutoKill();
            }

        }
        //もうクリアしていたらヒント出さない
        else if((GameManager.instance._clearState & clear) == clear)
        {
            _textBox.SetActive(true);
            _hintText.DOText("もう考えることはないようだ", 1.5f).SetEase(Ease.Linear).OnComplete(() => _endReadHint = true).SetAutoKill();
        }

    }

    /// <summary>ヒントテキストを表示する</summary>
    /// <param name="s"></param>
    public void ShowHint(string s, HintCheckList hintCheckList = default , GameObject hintbutton = default)
    {
        _textBox.SetActive(true);
        _hintText.DOText(s, 1.5f).SetEase(Ease.Linear).OnComplete(() => { _endReadHint = true; _hintCheckList |= hintCheckList; hintbutton.SetActive(true); }).SetAutoKill();
    }
}
