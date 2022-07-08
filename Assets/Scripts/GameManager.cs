using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>ゲームマネージャー！！！！！！！！！</summary>
public class GameManager : MonoBehaviour
{
    [SerializeField, Header("ライト1のMeshRenderer"), Tooltip("1つ目のライトのメッシュレンダラー")] MeshRenderer _light1;
    [SerializeField, Header("ライト2のMeshRenderer"), Tooltip("2つ目のライトのメッシュレンダラー")] MeshRenderer _light2;
    [SerializeField, Header("ライト3のMeshRenderer"), Tooltip("3つ目のライトのメッシュレンダラー")] MeshRenderer _light3;
    [SerializeField, Header("光るMaterial"), Tooltip("ライトが光ってるマテリアル")] Material _lightEmission;
    [SerializeField, Header("レバー"), Tooltip("レバーのゲームオブジェクト")] GameObject _lever;
    [SerializeField, Header("テキストボックス"), Tooltip("テキストボックスのゲームオブジェクト")] GameObject _textBox;
    [Tooltip("背面にある色付きボタンのリスト")] List<string> _button = new List<string>();
    [Tooltip("ボタンの文字列の配列")] string[] _colorButton = { "YellowButton", "WhiteButton", "BlueButton", "RedButton", "GreenButton" };
    Clear _clearState;

    /// <summary>謎解きの進行度</summary>
    enum Clear
    {
        /// <summary>最初の謎解けた</summary>
        FirstStageClear,
        /// <summary>二番目の謎解けた</summary>
        SecondStageClear,
        /// <summary>三番目の謎解けた</summary>
        ThirdStageClear,
    }

    void Start()
    {
        _lever.SetActive(false);
        _textBox.SetActive(false);
        Transfer();
    }

    
    void Update()
    {
        //背面にある謎解き　クリックした順番があってたらクリア
        //メインカメラからRayを飛ばして、オブジェクトを探す
        //左クリックしたオブジェクトが_buttonリストの0番目と同じならリストから消える
        //全部消えたらクリア
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if (Physics.Raycast(_ray, out _hit, 10.0f, 3) && Input.GetMouseButtonDown(0))
        {
            Debug.Log(_hit.collider.gameObject.name);

            if (_hit.collider.gameObject.name == _button[0])
            {
                _button.RemoveAt(0);
                Debug.Log(_button.Count);
            }
            else if(_hit.collider.gameObject.name != _button[0] && _hit.collider.gameObject.tag == "ColorButton")
            {
                Transfer();
                Debug.Log(_button.Count);
            }
        }

        //リストの中身がなくなったら、2個目のライトを光らせる
        if (_button.Count == 0)
        {
            _light2.material = _lightEmission;
            _clearState = Clear.SecondStageClear;
        }
    }

    private void Transfer()
    {
        _button.Clear();

        for (int i = 0; i < _colorButton.Length; i++)
        {
            _button.Add(_colorButton[i]);
        }
    }
}
