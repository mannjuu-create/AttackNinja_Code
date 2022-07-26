using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クナイと衝突したらクナイを削除して死亡アニメーションを
/// 再生するスクリプト.
/// </summary>

public class HitKunai : MonoBehaviour
{
    //Moveスクリプト.
    [SerializeField] private enemyMove move;

    //////////////////////////////
    ///サウンド.
    //////////////////////////////
    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        //オーディオのComponentを取得.
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "kunai" && move.deathFrag == false)
        {
            //当たったクナイ削除.
            Destroy(col.gameObject);

            //音を鳴らす
            audioSource.PlayOneShot(sound);

            //重力削除.
            move.rb.useGravity = false;
            //当たり判定削除.
            move.col.enabled = false;
            //死亡フラグを立てる.
            move.deathFrag = true;
            //アニメーションセット.
            move.animator.SetTrigger("death");
            //3秒後にゲームオーバー.
            Destroy(gameObject, 2.0f);
        }
    }
}
