using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトルからゲームプレイ画面へ遷移する時に
/// ロード画面を表示するスクリプト.
/// </summary>

public class GoGame : MonoBehaviour
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
            SceneManager.LoadScene("GameScene");
        }
    }
}