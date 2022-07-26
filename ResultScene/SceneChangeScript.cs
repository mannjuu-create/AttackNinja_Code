using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���U���g��ʂ���^�C�g��or�Q�[���v���C��ʂ֑J�ڂ���Ƃ���
/// ���[�h��ʂ�\������X�N���v�g.
/// </summary>

public class SceneChangeScript : MonoBehaviour
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
            SceneManager.LoadScene("TitleScene");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            image.SetActive(true);
            //�V�[���؂�ւ�.
            SceneManager.LoadScene("GameScene");
        }
    }
}