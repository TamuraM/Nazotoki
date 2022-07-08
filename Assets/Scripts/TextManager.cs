using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>テキストを管理してます</summary>
public class TextManager : MonoBehaviour
{
    [Header("テキスト"), Tooltip("テキストのオブジェクト")] Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    private void Update()
    {

    }
}
