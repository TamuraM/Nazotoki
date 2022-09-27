using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField, Header("BGM")] AudioSource[] _sounds;
    [Tooltip("�X�^�[�g�������ǂ���")] bool _goStart;
    [Tooltip("�Q�[����ʂɂ��������ǂ���")] bool _goGame;
    [Tooltip("�N���W�b�g��ʂɂ��������ǂ���")] bool _goCredit;
    [SerializeField, Header("�G���^�[�L�[�������Ƃ���SE")] AudioClip _start;
    [SerializeField, Header("�G�X�P�[�v�L�[�������Ƃ���SE")] AudioClip _credit;
    [SerializeField, Header("���^�C�A�{�^��")] RetireGameButton _retire;

    private void Start()
    {
        _sounds[0].Play();
    }

    void Update()
    {
        
        //�^�C�g����ʂŃG���^�[�L�[����������SE�Đ�����BGM���~�߂�
        if(GameManager.instance._gameMode == GameManager.GameMode.Title && Input.GetKeyDown(KeyCode.Return) && !_goStart)
        {
            _goStart = true;
            _sounds[3].PlayOneShot(_start);
            _sounds[0].Stop();
        }

        //�^�C�g����ʂŃG�X�P�[�v�L�[����������SE�Đ�����BGM���~�߂�
        if (GameManager.instance._gameMode == GameManager.GameMode.Title && Input.GetKeyDown(KeyCode.Escape))
        {
            _sounds[3].PlayOneShot(_credit);
            _sounds[0].Stop();
        }

        //�Q�[���v���C��ʂɂȂ�����Q�[������BGM�𗬂�
        if (GameManager.instance._gameMode == GameManager.GameMode.PlayGame && !_goGame)
        {
            _goGame = true;
            Debug.Log("�Q�[��BGM");
            _sounds[1].Play();
        }

        //�N���W�b�g��ʂɂȂ�����N���W�b�g����BGM�𗬂�
        if(GameManager.instance._gameMode == GameManager.GameMode.Credit && !_goCredit)
        {
            _goCredit = true;
            Debug.Log("�N���W�b�gBGM");
            _sounds[2].Play();
        }

        //�Q�[�����I��邩�A���^�C�A������BGM���~�߂�
        if(GameManager.instance._gameMode == GameManager.GameMode.GameClear || GameManager.instance._gameMode == GameManager.GameMode.GameOver || _retire.IsClicked)
        {
            _sounds[1].Stop();
        }

    }
}
