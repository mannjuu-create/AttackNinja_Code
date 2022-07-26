using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 宝箱と衝突時に宝箱を消してスコアを増やすスクリプト.
/// </summary>

public class GetTreasure : MonoBehaviour
{
    //////////////////////////////
    ///スコア.
    //////////////////////////////
    [SerializeField] private Text ScoreText;
    public int score = 0;

    //////////////////////////////
    ///サウンド.
    //////////////////////////////
    [SerializeField] private AudioClip sound;
    AudioSource audioSource;

    void Start()
    {
        //オーディオのComponentを取得.
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "treasure")
        {
            score++;
            ScoreText.text = string.Format("GetTreasure:{0}", score);

            //音を鳴らす
            audioSource.PlayOneShot(sound);

            Destroy(col.gameObject);
        }
    }
}
