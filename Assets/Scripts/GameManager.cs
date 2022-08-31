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

    [SerializeField, Header("ライト1のMeshRenderer"), Tooltip("1つ目のライトのメッシュレンダラー")] MeshRenderer _light1;
    [SerializeField, Header("ライト2のMeshRenderer"), Tooltip("2つ目のライトのメッシュレンダラー")] MeshRenderer _light2;
    [SerializeField, Header("ライト3のMeshRenderer"), Tooltip("3つ目のライトのメッシュレンダラー")] MeshRenderer _light3;
    [SerializeField, Header("光ってないマテリアル"), Tooltip("ライトがひかってないときのマテリアル")] Material _lightMaterial;
    [SerializeField, Header("光るMaterial"), Tooltip("ライトが光ってるマテリアル")] Material _lightEmission;

    [SerializeField, Header("謎解きのImage"), Tooltip("謎解きがかいてある画像")] GameObject _nazo;
    [SerializeField, Header("入力するテキストGameObject"), Tooltip("タイプライターをクリックしたときに出てくる入力画面パネル")] GameObject _inputText;
    [SerializeField, Header("ボタン"), Tooltip("ボタンのゲームオブジェクト")] GameObject _button;

    [Tooltip("背面にある色付きボタンのリスト")] List<string> _buttons = new();
    [Tooltip("ボタンの配列 押す順番に名前が入ってる")] //黄、白、青、赤、緑
    string[] _colorButton = { "YellowButton", "WhiteButton", "BlueButton", "RedButton", "GreenButton" };
    [SerializeField, Tooltip("ボタンを正しく押せた時に光るライトのリスト")] List<MeshRenderer> _lights = new(5);
    [Tooltip("ライトのやつカウントする数字")] int _lighting = 0;

    [Tooltip("ゲームがスタートしたかどうか")] bool _readStory;
    public bool ReadStory { get => _readStory; set => _readStory = value; }
    [SerializeField, Tooltip("オブジェクトに触れる時かどうか")] bool _inGame;
    public bool InGame { get => _inGame; set => _inGame = value; }

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
        /// <summary>すべての謎解けた</summary>
        AllStageClear = 1 << 3,
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
        _button.SetActive(false);
        _nazo.SetActive(false);
        _inputText.SetActive(false);
        _buttons = _colorButton.ToList();
    }


    void Update()
    {
        //右クリックで現在のクリア状況を確認
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log(_clearState);
        }

        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        //if(_inGame)
        //{
        if (Physics.Raycast(_ray, out _hit, 10.0f, 3) && Input.GetMouseButtonDown(0))
        {
            var hit = _hit.collider.gameObject.name;
            Debug.Log(hit);

            //----------左側にある謎解き----------
            //机の上の紙クリックしたら謎が拡大される
            if (hit == "Nazo")
            {
                Debug.Log("なぞだ");
                _nazo.SetActive(true);
            }

            //タイプライターをクリックしたら入力画面が出てくる
            if(hit == "Typewriter")
            {
                Debug.Log("タイプライターだ");
                _inputText.SetActive(true);
            }

            //ボタンをクリックしたらクリア
            if(hit == "FirstButton")
            {
                _light1.material = _lightEmission;
                _clearState |= Clear.FirstStageClear;
            }
            //----------ここまで----------

            //----------背面にある謎解き----------　クリックした順番があってたらクリア
            if (hit == _buttons[0])
            {
                //正解のボタンを押したら、上にあるライトが順番に点く
                _buttons.RemoveAt(0);
                Debug.Log(_buttons.Count);
                _lights[_lighting].material = _lightEmission;

                if (_lighting < 5)
                {
                    _lighting++;
                }

            }
            else if (hit != _buttons[0] && _hit.collider.gameObject.tag == "ColorButton")
            {
                //間違ったボタンを押したら、上にあるライトが全部消える
                _buttons = _colorButton.ToList();
                Debug.Log(_buttons.Count);
                _lighting = 0;
                _lights.ForEach(light => light.material = _lightMaterial);
            }

            //クリアしたら、2個目のライトを光らせる
            if (_buttons.Count == 0)
            {
                _buttons.Add("a");
                _light2.material = _lightEmission;
                _clearState |= Clear.SecondStageClear;
            }
            //----------ここまで----------

            //}

        }

        //全部の謎解いたら、ステータスが「すべての謎を解いた」になる
        if ((_clearState & Clear.FirstStageClear) == Clear.FirstStageClear && (_clearState & Clear.SecondStageClear) == Clear.SecondStageClear && (_clearState & Clear.ThirdStageClear) == Clear.ThirdStageClear)
        {
            _clearState = Clear.AllStageClear;
        }

    }

}
