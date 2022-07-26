using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// リザルト画面からタイトルorゲームプレイ画面へ遷移するときに
/// ロード画面を表示するスクリプト.
/// </summary>

public class SceneChangeScript : MonoBehaviour
{
    //シーン遷移時にロード画面を表示.
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
            //シーン切り替え.
            SceneManager.LoadScene("TitleScene");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            image.SetActive(true);
            //シーン切り替え.
            SceneManager.LoadScene("GameScene");
        }
    }
}