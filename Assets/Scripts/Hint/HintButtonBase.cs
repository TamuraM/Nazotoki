using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// ヒントボタンの基底クラス
/// </summary>
public class HintButtonBase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Header("ヒントテキストボックス")] GameObject _textBox;
    [SerializeField, Header("ヒントテキスト")] protected Text _hintText;
    [SerializeField, Header("ヒントのテキストデータ")] TextAsset _hintTextFile;
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
    [Tooltip("この画像の色")] Color _imageColor;
    [Tooltip("この画像")] Image _image;
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

        //ヒントを読み終わっていて、左クリックしたらテキストボックスが消える
        if (_endReadHint && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _hintText.text = "";
            _textBox.SetActive(false);
            GameManager.instance._gameMode = GameManager.GameMode.PlayGame;
            _endReadHint = false;
        }

        //ヒントを見てる時にクリックしたら、一気に最後まで行く
        if(GameManager.instance._gameMode == GameManager.GameMode.Thinking && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _hintText.DOComplete();
        }

    }

    /// <summary>クリックしたら画像を暗くしてヒント表示</summary>
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

    /// <summary>マウスカーソルが入ったら少し暗く</summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {

        if (GameManager.instance._gameMode == GameManager.GameMode.PlayGame)
        {
            _image.color = _imageColor - new Color(55 / 255f, 55 / 255f, 55 / 255f, 0);
        }
            
    }

    /// <summary>マウスが出たら元の色に</summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = _imageColor;
    }

    /// <summary>
    /// クリックしたときに、ヒントの状態とボタンに対応したヒントを表示する関数
    /// </summary>
    public virtual void Click()
    {
        Debug.LogError("継承先でオーバーロードを指定してください。");
    }

    public virtual IEnumerator Delay()
    {
        Debug.LogError("継承先でオーバーロードを指定してください");
        yield return new WaitForSeconds(0.1f);
    }

}
