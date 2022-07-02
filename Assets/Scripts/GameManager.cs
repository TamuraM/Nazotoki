using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ゲームマネージャー！！！！！！！！！</summary>
public class GameManager : MonoBehaviour
{
    [SerializeField, Header("レバー"), Tooltip("レバーのゲームオブジェクト")] GameObject _lever;

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
    }

    void Update()
    {

    }
}
