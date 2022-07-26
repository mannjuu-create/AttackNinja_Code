using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーがエネミーに近づいたら攻撃アニメーションを
/// 再生するスクリプト.
/// </summary>

public class enemyAttack : MonoBehaviour
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
        if(move.deathFrag == false)
        {
            if (col.gameObject.tag == "Player")
            {
                move.animator.SetBool("attack", true);
                //音を鳴らす
                audioSource.PlayOneShot(sound);
            }
        }
    }
}