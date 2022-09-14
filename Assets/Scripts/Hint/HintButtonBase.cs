using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ヒントボタンの基底クラス
/// </summary>
public class HintButtonBase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Header("ヒントテキストボックス")] GameObject _textBox;
    [SerializeField, Header("ヒントテキスト")] protected Text _hintText;
    [SerializeField, Header("ヒントのテキストデータ")] TextAsset _hintTextFile;
    protected string[] _hint;
    [Tooltip("この画像の色")] Color _imageColor;
    [Tooltip("この画像")] Image _image;
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

        //ヒントを読み終わっていて、左クリックしたらテキストボックスが消える
        if (_endReadHint && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _hintText.text = "";
            _textBox.SetActive(false);
            GameManager.instance._gameMode = GameManager.GameMode.PlayGame;
            _endReadHint = false;
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
        _image.color = _imageColor - new Color(55 / 255f, 55 / 255f, 55 / 255f, 0);
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
}
