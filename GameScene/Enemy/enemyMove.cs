using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが一定の距離まで近づいたら
/// 屋根の上から飛び降りるスクリプト.
/// </summary>

public class enemyMove : MonoBehaviour
{
    //プレイヤーオブジェクト.
    [SerializeField] private GameObject playerObj;

    //////////////////////////////
    ///システム.
    /////////////////////////////////
    //rigidbodyを宣言.
    public Rigidbody rb;
    //BoxColliderを宣言.
    public BoxCollider col;
    //Animator型を変数animatorで宣言します。
    public Animator animator;

    //////////////////////////////
    ///ジャンプ関係.
    //////////////////////////////
    [SerializeField] private Vector3 moveDirection = Vector3.zero;    //それぞれの座標を０で宣言します。
    [SerializeField] private float distance;                          //飛び降りる距離.
    [SerializeField] private float nowDistance = 0;                   //プレイヤーとの現在の距離.
    [SerializeField] private Vector3 force;                           //最終的なジャンプ力.
    [SerializeField] private float powerX;                            //ジャンプ力(横).
    [SerializeField] private float sidePos;                           //横移動で目指す位置.
    [SerializeField] private float powerY;                            //ジャンプ力(高さ).
    [SerializeField] private float powerZ;                            //ジャンプ力(奥行き).
    [SerializeField] private bool jumpFrag = false;                   //ジャンプしたかどうか.
    [SerializeField] private bool leftFrag;                           //プレイヤーから見て左側か.
    [SerializeField] private bool centerFrag;                         //プレイヤーから見て真ん中か.
    [SerializeField] private bool rightFrag;                          //プレイヤーから見て右側か.
    [SerializeField] private float sideMax;                           //横移動の限界値.

    //死亡フラグ.
    public bool deathFrag = false;

    void Start()
    {
        //rigidbodyを取得.
        rb = this.GetComponent<Rigidbody>();
        //BoxColliderを取得.
        col = this.GetComponent<BoxCollider>();
        //GetComponentでAnimatorを取得して変数animatorで参照します。
        animator = GetComponent<Animator>();

        //プレイヤーを取得.
        playerObj = GameObject.Find("player");

        //横移動の距離を計算.
        powerX = (sidePos - this.transform.position.x) / 2;
        force = new Vector3(powerX, powerY, powerZ);
    }
        

    void Update()
    {
        //プレイヤーとの距離を計算.
        nowDistance = this.transform.position.z - playerObj.transform.position.z;
        //プレイヤーとの距離が一定距離に近づいたら飛び降りる.
        if (nowDistance <= distance && jumpFrag == false)
        {
            rb.AddForce(force, ForceMode.Impulse);  // 力を加える
            animator.SetTrigger("jump");            // ジャンプアニメーション再生.
            jumpFrag = true;                        // ジャンプ済みフラグをtrueにする.
        }

        Jump();
    }

    //ジャンプ処理.
    void Jump()
    {
        //プレイヤーから見て左側なら.
        if (leftFrag == true)
        {
            //真ん中に着地するなら.
            if (centerFrag == true)
            {
                if (this.transform.position.x >= sideMax)
                {
                    var tmp = rb.velocity; // 現在の速度を取る
                    tmp.x = 0;             // X軸速度だけ打ち消す
                    rb.velocity = tmp;     // 再代入
                }
            }
            else
            {
                if (this.transform.position.x >= sideMax)
                {
                    var tmp = rb.velocity; // 現在の速度を取る
                    tmp.x = 0;             // X軸速度だけ打ち消す
                    rb.velocity = tmp;     // 再代入
                }
            }
        }
        else if (rightFrag == true)
        {
            //真ん中に着地するなら.
            if (centerFrag == true)
            {
                if (this.transform.position.x <= sideMax)
                {
                    var tmp = rb.velocity; // 現在の速度を取る
                    tmp.x = 0;             // X軸速度だけ打ち消す
                    rb.velocity = tmp;     // 再代入
                }
            }
            else
            {
                if (this.transform.position.x <= sideMax)
                {
                    var tmp = rb.velocity; // 現在の速度を取る
                    tmp.x = 0;             // X軸速度だけ打ち消す
                    rb.velocity = tmp;     // 再代入
                }
            }
        }
    }
}
