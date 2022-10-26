using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// ヒントボタンの基底クラス
/// </summary>
public class HintButtonBase : ButtonBase
{
    [SerializeField, Header("ヒントテキストボックス")] GameObject _textBox;
    [SerializeField, Header("ヒントを表示させるテキスト")] Text _hintText;
    protected Text HintText { get => _hintText; }
    [SerializeField, Header("表示させるヒントのテキスト")] TextAsset _hint;
    protected TextAsset Hint { get => _hint; }
    protected bool _endReadHint;

    protected void Start()
    {
        gameObject.SetActive(false);
    }

    protected void Update()
    {

        //ヒントを読み終わっていて、左クリックしたらテキストボックスが消える
        if (_endReadHint && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _hintText.text = "";
            _textBox.SetActive(false);
            GameManager.Instance._gameMode = GameManager.GameMode.PlayGame;
            _endReadHint = false;
        }

        //ヒントを見てる時にクリックしたら、一気に最後まで行く
        if(GameManager.Instance._gameMode == GameManager.GameMode.Thinking && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _hintText.DOComplete();
        }

    }

    /// <summary>クリックしたら画像を暗くしてヒント表示</summary>
    /// <param name="eventData"></param>
    public override void OnPointerClick(PointerEventData eventData)
    {

        if(GameManager.Instance._gameMode == GameManager.GameMode.PlayGame)
        {
            GameManager.Instance._gameMode = GameManager.GameMode.Thinking;
            Image.color = new Color(155 / 255f, 155 / 255f, 155 / 255f, 1);
            _textBox.SetActive(true);
            Click();
        }
        
    }

    /// <summary>マウスカーソルが入ったら少し暗く</summary>
    /// <param name="eventData"></param>
    public override void OnPointerEnter(PointerEventData eventData)
    {

        if (GameManager.Instance._gameMode == GameManager.GameMode.PlayGame)
        {
            Image.color = new Color(200 / 255f, 200 / 255f, 200 / 255f, 1);
        }
            
    }

    /// <summary>
    ///　すべて表示された後にクリックしたらテキストボックスが消えるが、
    ///　ヒントの途中でクリックしたら一気に最後まで行ってそのまま消えてしまうため、
    ///　読み終えた判定を0.1秒待ってからオンにしたいからコルーチンを使う
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator ShowHint()
    {
        Debug.LogError("継承先でオーバーロードを指定してください");
        yield return new WaitForSeconds(0.1f);
    }

}
