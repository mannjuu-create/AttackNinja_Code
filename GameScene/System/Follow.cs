using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの移動をするスクリプト.
/// </summary>

public class Follow : MonoBehaviour
{
    //プレイヤーの移動スクリプト.
    [SerializeField] private PlayerMove move;

    //Vector3型を変数vectorで宣言します。
    Vector3 vector;

    //プレイヤー死亡時に少し下がる.
    [SerializeField] private Vector3 deathPos;

    //GameObject型を変数Targetで宣言します。
    [SerializeField] private GameObject target;
    //float型を変数followSpeedで宣言します。
    [SerializeField] private float followSpeed;

    void Start()
    {
        //位置をTargetの位置を元に設定するよ。
        vector = target.transform.position - transform.position;
    }


    void Update()
    {
        //位置を取得してスピードも合わせていくよ。
        if(move.gameOverFrag == false)
        {
            transform.position = Vector3.Lerp(
            transform.position,
            target.transform.position - vector,
            Time.deltaTime * followSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(
            transform.position,
            target.transform.position - (vector - deathPos),
            Time.deltaTime * followSpeed);
        }
    }
}