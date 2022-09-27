using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField, Header("BGM")] AudioSource[] _sounds;
    [Tooltip("スタートしたかどうか")] bool _goStart;
    [Tooltip("ゲーム画面にいったかどうか")] bool _goGame;
    [Tooltip("クレジット画面にいったかどうか")] bool _goCredit;
    [SerializeField, Header("エンターキー押したときのSE")] AudioClip _start;
    [SerializeField, Header("エスケープキー押したときのSE")] AudioClip _credit;
    [SerializeField, Header("リタイアボタン")] RetireGameButton _retire;

    private void Start()
    {
        _sounds[0].Play();
    }

    void Update()
    {
        
        //タイトル画面でエンターキーを押したらSE再生してBGMを止める
        if(GameManager.instance._gameMode == GameManager.GameMode.Title && Input.GetKeyDown(KeyCode.Return) && !_goStart)
        {
            _goStart = true;
            _sounds[3].PlayOneShot(_start);
            _sounds[0].Stop();
        }

        //タイトル画面でエスケープキーを押したらSE再生してBGMを止める
        if (GameManager.instance._gameMode == GameManager.GameMode.Title && Input.GetKeyDown(KeyCode.Escape))
        {
            _sounds[3].PlayOneShot(_credit);
            _sounds[0].Stop();
        }

        //ゲームプレイ画面になったらゲーム時のBGMを流す
        if (GameManager.instance._gameMode == GameManager.GameMode.PlayGame && !_goGame)
        {
            _goGame = true;
            Debug.Log("ゲームBGM");
            _sounds[1].Play();
        }

        //クレジット画面になったらクレジット時のBGMを流す
        if(GameManager.instance._gameMode == GameManager.GameMode.Credit && !_goCredit)
        {
            _goCredit = true;
            Debug.Log("クレジットBGM");
            _sounds[2].Play();
        }

        //ゲームが終わるか、リタイアしたらBGMを止める
        if(GameManager.instance._gameMode == GameManager.GameMode.GameClear || GameManager.instance._gameMode == GameManager.GameMode.GameOver || _retire.IsClicked)
        {
            _sounds[1].Stop();
        }

    }
}
