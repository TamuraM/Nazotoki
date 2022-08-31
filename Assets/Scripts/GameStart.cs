using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField, Header("�^�C�g��")] GameObject _title;
    [SerializeField, Header("�X�^�[�g")] GameObject _start;
    Animator _titleAnim;
    Animator _startAnim;
    [SerializeField, Header("�X�g�[���[��Text")] GameObject _storyText;
    [SerializeField, Header("�t�F�[�h������̊Ԋu")] float _fade;
    [SerializeField, Tooltip("�����ꂽ��")] bool _isPushed;

    private void Start()
    {
        _titleAnim = _title.GetComponent<Animator>();
        _startAnim = _start.GetComponent<Animator>();
        _storyText.SetActive(false);
        _isPushed = false;
    }

    void Update()
    {
        //�Ȃɂ��������ꂽ��V�[���ڍs
        if(Input.anyKeyDown && !_isPushed)
        {
            StartCoroutine(Fade());
            _isPushed = !_isPushed;
        }

    }

    IEnumerator Fade()
    {
        _titleAnim.SetBool("isPressed", true);
        _startAnim.SetBool("isPressed", true);

        yield return new WaitForSeconds(_fade);

        _title.SetActive(false);
        _start.SetActive(false);
        _storyText.SetActive(true);
        GameManager.instance._gameMode = GameManager.GameMode.Opening;
    }
}
