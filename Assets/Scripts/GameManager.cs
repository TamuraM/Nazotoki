using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�Q�[���}�l�[�W���[�I�I�I�I�I�I�I�I�I</summary>
public class GameManager : MonoBehaviour
{
    [SerializeField, Header("���o�["), Tooltip("���o�[�̃Q�[���I�u�W�F�N�g")] GameObject _lever;

    /// <summary>������̐i�s�x</summary>
    enum Clear
    {
        /// <summary>�ŏ��̓������</summary>
        FirstStageClear,
        /// <summary>��Ԗڂ̓������</summary>
        SecondStageClear,
        /// <summary>�O�Ԗڂ̓������</summary>
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
