using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField, Header("�^�C�g����Animator")] Animator _titleAnim;
    [SerializeField, Header("�X�^�[�g��Animator")] Animator _startAnim;
    [SerializeField, Header("�t�F�[�h����܂ł̎���")] float _fade;

    void Update()
    {

        if(Input.anyKeyDown)
        {
            StartCoroutine(GameStart());
        }

        IEnumerator GameStart()
        {
            _titleAnim.SetBool("isPressed", true);
            _startAnim.SetBool("isPressed", true);

            yield return new WaitForSeconds(_fade);

            SceneManager.LoadScene("Game");
        }
    }
}
