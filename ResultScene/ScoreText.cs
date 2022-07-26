using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// リザルト画面表示時に最終スコアをスコアテキストに
/// 取得させるスクリプト.
/// </summary>

public class ScoreText : MonoBehaviour
{
    public int finalScore;              //スコア.

    // 表示.
    [SerializeField] private Text scoreText;

    void Start()
    {
        scoreText.text = string.Format("FinalScore:{0}00＄", finalScore);
    }
}
