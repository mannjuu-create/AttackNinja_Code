using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �^�C�g������Q�[���v���C��ʂ֑J�ڂ��鎞��
/// ���[�h��ʂ�\������X�N���v�g.
/// </summary>

public class GoGame : MonoBehaviour
{
    //�V�[���J�ڎ��Ƀ��[�h��ʂ�\��.
    [SerializeField] private GameObject image;

    void Start()
    {
        image.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            image.SetActive(true);
            //�V�[���؂�ւ�.
            SceneManager.LoadScene("GameScene");
        }
    }
}