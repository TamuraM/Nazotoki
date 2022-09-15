using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>ボタンの基底クラス</summary>
public class ButtonBase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("これがついてるオブジェクトのイメージコンポーネント")] Image _image;
    [Tooltip("このイメージの色")] Color _imageColor;

    protected Image Image
    { get => _image; set => _image = value; }

    protected Color ImageColor
    { get => _imageColor; set => _imageColor = value; }

    private void Awake()
    {
        _image = this.gameObject.GetComponent<Image>();
        _imageColor = _image.color;
    }

    /// <summary>ボタンを押したときの処理</summary>
    public virtual void Click()
    {
        Debug.LogError("継承先の派生クラスで関数を定義してください。");
    }

    public void OnPointerClick(PointerEventData eventData) // ボタンが押され、その後ドラッグ操作が入ることなくボタンが離される
    {
        //_image.color = _imageColor - new Color(100 / 255f, 100 / 255f, 100 / 255f, 0);
        Click();
    }

    /// <summary>ボタンの上にカーソルが乗ったら透明度を下げる</summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) //ボタンの範囲にマウスカーソルが入る
    {
        _image.color = _imageColor +  new Color(0, 0, 0, 105/255f);
    }

    /// <summary>ボタンの上からカーソルがなくなったら透明度を上げる</summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) //ボタンの範囲からマウスカーソルが出る
    {
        _image.color = _imageColor;
    }
}
