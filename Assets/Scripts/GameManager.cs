using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>ゲームマネージャー！！！！！！！！！</summary>
public class GameManager : MonoBehaviour
{

    public static GameManager instance; 

    Clear _clearState;

    [SerializeField, Header("ライト1のMeshRenderer"), Tooltip("1つ目のライトのメッシュレンダラー")] MeshRenderer _light1;
    [SerializeField, Header("ライト2のMeshRenderer"), Tooltip("2つ目のライトのメッシュレンダラー")] MeshRenderer _light2;
    [SerializeField, Header("ライト3のMeshRenderer"), Tooltip("3つ目のライトのメッシュレンダラー")] MeshRenderer _light3;
    [SerializeField, Header("光ってないマテリアル"), Tooltip("ライトがひかってないときのマテリアル")] Material _lightMaterial;
    [SerializeField, Header("光るMaterial"), Tooltip("ライトが光ってるマテリアル")] Material _lightEmission;

    [SerializeField, Header("謎解きのImage"), Tooltip("謎解きがかいてある画像")] GameObject _nazo;
    [SerializeField, Header("入力するテキストGameObject"), Tooltip("タイプライターをクリックしたときに出てくる入力画面パネル")] GameObject _inputText;
    [SerializeField, Header("レバー"), Tooltip("レバーのゲームオブジェクト")] GameObject _lever ;

    [Tooltip("背面にある色付きボタンのリスト")] List<string> _button = new();
    [Tooltip("ボタンの配列 押す順番に名前が入ってる")]
    string[] _colorButton = { "YellowButton", "WhiteButton", "BlueButton", "RedButton", "GreenButton" };
    [SerializeField, Tooltip("ボタンを正しく押せた時に光るライトのリスト")] List<MeshRenderer> _lights = new(5);
    [Tooltip("ライトのやつカウントする数字")] int _lighting = 0;

    /// <summary>謎解きの進行度</summary>
    enum Clear
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

    private void Awake()
    {
        if(instance == null)
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
        _lever.SetActive(false);
        _nazo.SetActive(false);
        _inputText.SetActive(false);
        Transfer();
    }

    
    void Update()
    {
        //右クリックで現在のクリア状況を確認
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log(_clearState);
        }

        //全部の謎解いたら、ステータスが「すべての謎を解いた」になる
        if((_clearState & Clear.FirstStageClear) == Clear.FirstStageClear && (_clearState & Clear.SecondStageClear) == Clear.SecondStageClear && (_clearState & Clear.ThirdStageClear) == Clear.ThirdStageClear)
        {
            _clearState = Clear.AllStageClear;
        }

        
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(_ray, out _hit, 10.0f, 3) && Input.GetMouseButtonDown(0))
        {
            //Debug.Log(_hit.collider.gameObject.name);

            //左側にある謎解き　机の上の紙クリックして謎解いてなんかしたらレバー現れる
            if (_hit.collider.gameObject.name == "Nazo")
            {
                Debug.Log("なぞだ");
                _nazo.SetActive(true);
            }

            //タイプライタークリックしたら、入力画面でてくる
            if(_hit.collider.gameObject.name == "Typewriter")
            {
                Debug.Log("タイプライターだ");
                _inputText.SetActive(true);
            }

            if(_hit.collider.gameObject == _lever)
            {
                _clearState |= Clear.FirstStageClear;
            }

            //背面にある謎解き　クリックした順番があってたらクリア
            //メインカメラからRayを飛ばして、オブジェクトを探す
            //左クリックしたオブジェクトが_buttonリストの0番目と同じならリストから消える
            //全部消えたらクリア
            if (_hit.collider.gameObject.name == _button[0])
            {
                _button.RemoveAt(0);
                Debug.Log(_button.Count);
                _lights[_lighting].material = _lightEmission;

                if(_lighting < 5)
                {
                    _lighting++;
                }

            }
            else if(_hit.collider.gameObject.name != _button[0] && _hit.collider.gameObject.tag == "ColorButton")
            {
                Transfer();
                Debug.Log(_button.Count);
                _lighting = 0;

                foreach(MeshRenderer light in _lights)
                {
                    light.material = _lightMaterial;
                }

            }

        }

        //リストの中身がなくなったら、2個目のライトを光らせる
        if (_button.Count == 0)
        {
            _button.Add("a");
            _light2.material = _lightEmission;
            _clearState |= Clear.SecondStageClear;
        }

    }

    /// <summary>ボタンリストに要素を入れる関数</summary>
    private void Transfer()
    {
        _button.Clear();

        for (int i = 0; i < _colorButton.Length; i++)
        {
            _button.Add(_colorButton[i]);
        }
    }

    public GameObject GetLever()
    {
        return _lever;
    }
}
