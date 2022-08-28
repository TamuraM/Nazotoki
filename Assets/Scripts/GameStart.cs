using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField, Header("タイトルのAnimator")] Animator _titleAnim;
    [SerializeField, Header("スタートのAnimator")] Animator _startAnim;
    [SerializeField, Header("フェードするまでの時間")] float _fade;

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
