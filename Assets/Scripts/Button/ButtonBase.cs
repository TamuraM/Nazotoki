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

    /// <summary>ボタンをクリックしたら画像をもっと暗くする
    /// <param name="eventData"></param>
    public virtual void OnPointerClick(PointerEventData eventData) // ボタンが押され、その後ドラッグ操作が入ることなくボタンが離される
    {
        _image.color = new Color(155 / 255f, 155 / 255f, 155 / 255f, 1);
        Click();
    }

    /// <summary>ボタンの上にカーソルが乗ったら画像を暗くする</summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerEnter(PointerEventData eventData) //ボタンの範囲にマウスカーソルが入る
    {
        _image.color = new Color(200 / 255f, 200 / 255f, 200 / 255f, 1);
    }

    /// <summary>ボタンの上からカーソルがなくなったら画像を元に戻す</summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) //ボタンの範囲からマウスカーソルが出る
    {
        _image.color = _imageColor;
    }
}
