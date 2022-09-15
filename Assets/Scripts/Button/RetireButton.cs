using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RetireButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("このボタンの画像")] Image _image;
    [Tooltip("このボタンの元の色")] Color _imageColor;
    [SerializeField, Header("リタイアするかの画面")] GameObject _retire;

    void Start()
    {
        _image = GetComponent<Image>();
        _imageColor = _image.color;
        _retire.SetActive(false);
    }

    /// <summary>クリックしたときに、リタイアするかの画面が出てくる</summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        _image.color = _imageColor;
        _retire.SetActive(true);
    }

    /// <summary>マウスカーソルが入ったら、少し暗くする</summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = _imageColor - new Color(55 / 255f, 55 / 255f, 55 / 255f, 0);
    }

    /// <summary>マウスカーソルが出たら、元の色に戻す</summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = _imageColor;
    }

    
}
