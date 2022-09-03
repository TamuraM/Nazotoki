using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

/// <summary>ゲームマネージャー！！！！！！！！！</summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //ドアの横にあるライト
    [SerializeField, Header("ライト1のMeshRenderer"), Tooltip("1つ目のライトのメッシュレンダラー")] MeshRenderer _light1;
    [SerializeField, Header("ライト2のMeshRenderer"), Tooltip("2つ目のライトのメッシュレンダラー")] MeshRenderer _light2;
    [SerializeField, Header("ライト3のMeshRenderer"), Tooltip("3つ目のライトのメッシュレンダラー")] MeshRenderer _light3;
    [SerializeField, Header("光ってないマテリアル"), Tooltip("ライトがひかってないときのマテリアル")] Material _lightMaterial;
    [SerializeField, Header("光るMaterial"), Tooltip("ライトが光ってるマテリアル")] Material _lightEmission;

    //左側の謎関係
    [SerializeField, Header("謎の背景"), Tooltip("謎解きがかいてある画像")] GameObject _nazo;
    [SerializeField, Header("入力するテキストGameObject"), Tooltip("タイプライターをクリックしたときに出てくる入力画面パネル")] GameObject _inputText;
    [SerializeField, Header("ボタン"), Tooltip("ボタンのゲームオブジェクト")] GameObject _button;

    //後ろ側の謎関係
    [Tooltip("背面にある色付きボタンのリスト")] List<string> _colorButtons = new();
    [Tooltip("ボタンの配列 押す順番に名前が入ってる")] //黄、白、青、赤、緑
    string[] _colorButton = { "YellowButton", "WhiteButton", "BlueButton", "RedButton", "GreenButton" };
    [SerializeField, Tooltip("ボタンを正しく押せた時に光るライトのリスト")] List<MeshRenderer> _lights = new(5);
    [Tooltip("ライトのやつカウントする数字")] int _lighting = 0;

    //右側の謎関係
    [SerializeField, Header("モニターの画像")] SpriteRenderer _monitor;
    [SerializeField, Header("謎の画像たちリスト")] Sprite[] _questions = new Sprite[3];
    [SerializeField, Header("ボタンを押す順番に入れる")] GameObject[] _spriteButton = new GameObject[3];
    [Tooltip("押すボタンのList")] List<GameObject> _spriteButtons = new();
    [SerializeField, Header("まるの画像")] Sprite _circle; //正解の時
    [SerializeField, Header("ばつの画像")] Sprite _cross; //不正解の時
    [Tooltip("今何問目か")] int _clearSprite = 0;

    //ドアの謎関係
    [SerializeField, Header("ドアの謎")] GameObject _lastNazo;
    [SerializeField, Header("テンキー")] GameObject _numberKey;
    [SerializeField, Header("ドアの謎の背景")] GameObject _lastNazoBackground;
    [SerializeField, Header("番号入力画面")] GameObject _numberKeyBackground;

    public bool _isFocused;

    /// <summary>ゲーム内の状態</summary>
    public enum GameMode
    {
        Title, //タイトル画面
        Opening, //オープニング中
        PlayGame, //ゲームプレイ中
        Thinking, //ヒントを考えてる
        GameClear, //脱出成功のエンディング
        GameOver, //時間切れのエンディング
    }

    public GameMode _gameMode;

    /// <summary>謎解きの進行度</summary>
    public enum Clear
    {
        ClearSitenaiMan = 0,
        /// <summary>最初の謎解けた</summary>
        FirstStageClear = 1 << 0,
        /// <summary>二番目の謎解けた</summary>
        SecondStageClear = 1 << 1,
        /// <summary>三番目の謎解けた</summary>
        ThirdStageClear = 1 << 2,
        /// <summary>すべての謎解けて、最後の謎出現</summary>
        LastStageStart = 1 << 3,
        /// <summary>最後の謎解けた</summary>
        LastStageClear = 1 << 4,
    }

    public Clear _clearState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _clearState = Clear.ClearSitenaiMan;
        _gameMode = GameMode.Title;

        _button.SetActive(false);
        _nazo.SetActive(false);
        _inputText.SetActive(false);
        _lastNazo.SetActive(false);
        _numberKey.SetActive(false);
        _lastNazoBackground.SetActive(false);
        _numberKeyBackground.SetActive(false);

        _colorButtons = _colorButton.ToList();
        _spriteButtons = _spriteButton.ToList();
        _monitor.sprite = _questions[_clearSprite];
    }


    void Update()
    {
        //右クリックで現在のクリア状況を確認
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log(_clearState);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (_gameMode == GameMode.PlayGame)
        {
            if (Physics.Raycast(ray, out hit, 10.0f, 3) && Input.GetMouseButtonDown(0))
            {
                Debug.Log(hit.collider.gameObject.name);

                if((_clearState & Clear.FirstStageClear) != Clear.FirstStageClear)
                {
                    //----------左側にある謎解き----------
                    //机の上の紙クリックしたら謎が拡大される
                    if (hit.collider.gameObject.name == "Nazo" && !_isFocused)
                    {
                        Debug.Log("なぞだ");
                        _nazo.SetActive(true);
                        _isFocused = true; ;
                    }

                    //タイプライターをクリックしたら入力画面が出てくる
                    if (hit.collider.gameObject.name == "Typewriter" && !_isFocused)
                    {
                        Debug.Log("タイプライターだ");
                        _inputText.SetActive(true);
                        _isFocused = true; ;
                    }

                    //ボタンをクリックしたらクリア
                    if (hit.collider.gameObject == _button)
                    {
                        _light1.material = _lightEmission;
                        _clearState |= Clear.FirstStageClear;
                    }
                    //----------ここまで----------
                }

                if((_clearState & Clear.SecondStageClear) != Clear.SecondStageClear)
                {
                    //----------背面にある謎解き----------　クリックした順番があってたらクリア
                    if (hit.collider.gameObject.name == _colorButtons[0])
                    {
                        //正解のボタンを押したら、上にあるライトが順番に点く
                        _colorButtons.RemoveAt(0);
                        //Debug.Log(_colorButtons.Count);
                        _lights[_lighting].material = _lightEmission;

                        if (_lighting < 5)
                        {
                            _lighting++;
                        }

                    }
                    else if (hit.collider.gameObject.name != _colorButtons[0] && hit.collider.gameObject.tag == "ColorButton")
                    {
                        //間違ったボタンを押したら、上にあるライトが全部消える
                        _colorButtons = _colorButton.ToList();
                        Debug.Log(_colorButtons.Count);
                        _lighting = 0;
                        _lights.ForEach(light => light.material = _lightMaterial);
                    }

                    //クリアしたら、2個目のライトを光らせる
                    if (_colorButtons.Count == 0)
                    {
                        _colorButtons.Add("a");
                        _light2.material = _lightEmission;
                        _clearState |= Clear.SecondStageClear;
                    }
                    //----------ここまで----------
                }

                if((_clearState & Clear.ThirdStageClear) != Clear.ThirdStageClear)
                {
                    //----------右側にある謎解き----------　画像に対応したボタンを順番に押せたらクリア
                    if (hit.collider.gameObject == _spriteButtons[0])
                    {
                        //正解したらまるの画像がでてきて、次の問題が表示される
                        _clearSprite++;
                        _spriteButtons.RemoveAt(0);
                        StartCoroutine(SpriteQuestionSucces());
                    }
                    else
                    {
                        _clearSprite = 0;
                        _spriteButtons = _spriteButton.ToList();
                        StartCoroutine(SpriteQuestionWrong());
                    }

                    if (_spriteButtons.Count == 0)
                    {
                        _spriteButtons.Add(this.gameObject);
                        _light3.material = _lightEmission;
                        _clearState |= Clear.ThirdStageClear;
                    }
                    //----------ここまで----------
                }


                //全部の謎解いたら、最後の謎が出現
                if ((_clearState & Clear.FirstStageClear) == Clear.FirstStageClear && (_clearState & Clear.SecondStageClear) == Clear.SecondStageClear && (_clearState & Clear.ThirdStageClear) == Clear.ThirdStageClear)
                {
                    _clearState = Clear.LastStageStart;
                }


                //----------ドアにある謎解き----------　全問正解したら出てくる　答えの番号を打ち込めばクリア
                if (_clearState == Clear.LastStageStart)
                {
                    _lastNazo.SetActive(true);
                    _numberKey.SetActive(true);
                }

                if (hit.collider.gameObject == _lastNazo && !_isFocused)
                {
                    _lastNazoBackground.SetActive(true);
                    _isFocused = true;
                }

                if (hit.collider.gameObject == _numberKey && !_isFocused)
                {
                    _numberKeyBackground.SetActive(true);
                    _isFocused = true;
                }
                //----------ここまで----------

            }

        }



    }

    IEnumerator SpriteQuestionSucces()
    {
        _monitor.sprite = _circle;
        yield return new WaitForSeconds(1.0f);
        _monitor.sprite = _questions[_clearSprite];
    }

    IEnumerator SpriteQuestionWrong()
    {
        _monitor.sprite = _cross;
        yield return new WaitForSeconds(1.0f);
        _monitor.sprite = _questions[_clearSprite];
    }
}
