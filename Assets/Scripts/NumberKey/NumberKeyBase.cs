using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NumberKeyBase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("これがついてるオブジェクトのイメージコンポーネント")] Image _image;
    protected Image Image { get => _image; set => _image = value; }
    [Tooltip("このイメージの色")] Color _imageColor;
    protected Color ImageColor { get => _imageColor; set => _imageColor = value; }
    [SerializeField] public NumberKeyController _numberKeyController;

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

    public virtual void OnPointerClick(PointerEventData eventData) // ボタンが押され、その後ドラッグ操作が入ることなくボタンが離される
    {

        if(_numberKeyController.Num.Length < 4)
        {
            Click();
        }
        else
        {
            return;
        }
        
    }

    /// <summary>ボタンの上にカーソルが乗ったら画像を暗くする</summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) //ボタンの範囲にマウスカーソルが入る
    {
        _image.color -= new Color(200, 200, 200);
    }

    /// <summary>ボタンの上からカーソルがなくなったら画像を元に戻す</summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) //ボタンの範囲からマウスカーソルが出る
    {
        _image.color = _imageColor;
    }
}
