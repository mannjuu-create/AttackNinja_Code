using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ゲーム開始時のカウントダウンを行うスクリプト.
/// </summary>

public class countDownScript : MonoBehaviour
{
    //カウントの時間.
    [SerializeField] private float countTime;
    //ゲームが始まったフラグ.
    public bool StartFrag = false;

    // テキスト.
    [SerializeField] private TextMeshProUGUI CountText;

    void Start()
    {
        //カウントが終わったら「Start」と表示するため、1秒プラスする.
        countTime += 1;
    }

    void Update()
    {
        if(StartFrag == false)
        {
            //カウントダウン.
            countTime -= Time.deltaTime;
            //テキスト変更.
            TextCountChange(countTime);

            //カウントダウンが終わったらゲームスタート.
            if (countTime <= 0)
            {
                StartFrag = true;
            }
        }
    }

    void TextCountChange(float countdown)
    {
        if(countdown > 3)
        {
            CountText.text = string.Format("3");
        }
        else if(countdown > 2)
        {
            CountText.text = string.Format("2");
        }
        else if(countdown > 1)
        {
            CountText.text = string.Format("1");
        }
        else if(countdown > 0)
        {
            CountText.text = string.Format("start!");
        }
        else
        {
            CountText.text = string.Format("");
        }
    }
}
