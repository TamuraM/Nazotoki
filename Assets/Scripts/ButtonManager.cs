using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>視点を変えるボタンを作るスクリプト</summary>
public class ButtonManager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("メインカメラオブジェクト")] GameObject _mainCamera;
    [Tooltip("謎の後ろにあるパネル")] GameObject _nazoBackground;
    [Tooltip("これがついてるオブジェクトのイメージコンポーネント")] Image _image;

    private void Start()
    {
        _image = this.gameObject.GetComponent<Image>();
        _nazoBackground = GameObject.Find("NazoBackground");
        _mainCamera = GameObject.Find("Main Camera");
    }

    /// <summary>ボタンを押したらカメラが回る</summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) // ボタンが押され、その後ドラッグ操作が入ることなくボタンが離される
    {
        //左ボタン押したら左に回る
        if(this.gameObject.name == "LeftButton")
        {
                _mainCamera.transform.Rotate(0, -90, 0);
        }

        //右ボタン押したら右に回る
        if (this.gameObject.name == "RightButton")
        {
            _mainCamera.transform.Rotate(0, 90, 0);
        }

        //なぞをクリックしたときに戻るボタン押すとなぞ消える
        if(this.gameObject.name == "ReturnButton")
        {
            _nazoBackground.SetActive(false);
        }
    }

    /// <summary>ボタンの上にカーソルが乗ったら透明度を下げる</summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) //ボタンの範囲にマウスカーソルが入る
    {
        _image.color += new Color(0, 0, 0, 55);
    }

    /// <summary>ボタンの上からカーソルがなくなったら透明度を上げる</summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) //ボタンの範囲からマウスカーソルが出る
    {
        _image.color -= new Color(0, 0, 0, 55);
    }
}
