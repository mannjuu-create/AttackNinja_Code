using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの移動を制御するスクリプト.
/// </summary>

public class PlayerMove : MonoBehaviour
{
    //ゲーム開始時のカウントダウン.
    [SerializeField] private countDownScript countDown;

    //レーンの移動の数値をそれぞれの変数で宣言します。
    const int   MinLane = -1;
    const int   MaxLane = 1;
    const float LaneWidth = 5.0f;

    //CharacterController型を変数controllerで宣言します。
    public CharacterController controller;
    //Animator型を変数animatorで宣言します。
    public Animator animator;

    //それぞれの座標を０で宣言します。
    public Vector3 moveDirection = Vector3.zero;        //ローカル空間.
    [SerializeField] private Vector3 globalDirection = Vector3.zero;      //ワールド空間.
    //int型を変数targetLaneで宣言します。
    [SerializeField] private int targetLane = 0;
    //横移動のための変数.
    [SerializeField] private float ratioX = 0;
    [SerializeField] private float moveCount = 0;
    [SerializeField] private bool  moveFrag = false;
    const float moveMax = 0.5f;

    //それぞれのパラメーターの設定をInspectorで変える様にします。
    [SerializeField] private float gravity;
    [SerializeField] private float speedZ;
    [SerializeField] private float speedX;
    [SerializeField] private float accelerationZ;
    [SerializeField] private float speedKnockback;
    [SerializeField] private float knockbackCount;
    [SerializeField] private float knockbackMax;

    //ノックバック用フラグ.
    public bool knockbackFrag = false;
    //ゲームオーバーフラグ.
    public bool gameOverFrag = false;

    void Start()
    {
        //GetComponentでCharacterControllerを取得して変数controllseで参照します。
        controller = GetComponent<CharacterController>();
        //GetComponentでAnimatorを取得して変数animatorで参照します。
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if(countDown.StartFrag == true && gameOverFrag == false)
        {
            //それぞれの矢印が押されたらそれぞれの関数を実行します。
            if (Input.GetKeyDown(KeyCode.A) &&
                !animator.GetCurrentAnimatorStateInfo(0).IsName("jump") &&
                knockbackFrag == false) MoveToLeft();

            if (Input.GetKeyDown(KeyCode.D) &&
                !animator.GetCurrentAnimatorStateInfo(0).IsName("jump") &&
                knockbackFrag == false) MoveToRight();

            //前進.
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            //横移動.
            if (moveFrag == true)
            {
                moveCount += Time.deltaTime;
                //一定の距離になったら止める.
                if (moveCount >= moveMax)
                {
                    moveDirection.x = 0;
                    moveFrag = false;
                }
            }

            //重力.
            moveDirection.y -= gravity * Time.deltaTime;

            //ノックバック.
            if (knockbackFrag == true)
            {
                Knockback();
            }

            globalDirection = transform.TransformDirection(moveDirection);
            controller.Move(globalDirection * Time.deltaTime);

            //接地判定.
            if (controller.isGrounded) moveDirection.y = 0;

            //走るアニメーションセット.
            animator.SetBool("run", moveDirection.z > 0.0f);
        }
    }

    //新しく作った関数のそれぞれの処理。
    public void SideMove()
    {
        ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
        moveDirection.x = ratioX * speedX;
    }

    public void MoveToLeft()
    {
        if (controller.isGrounded && targetLane > MinLane) targetLane--;
        SideMove();

        moveFrag = true;
        moveCount = 0;
    }

    public void MoveToRight()
    {
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
        SideMove();
        
        moveFrag = true;
        moveCount = 0;
    }

    public void Knockback()
    {
        //スピード変更.
        moveDirection.z = speedKnockback;
        //一定時間経過したらノックバックをやめる.
        if (knockbackCount >= knockbackMax)
        {
            knockbackFrag = false;
            knockbackCount = 0;
        }
        //経過時間をプラス.
        knockbackCount += Time.deltaTime;
    }
}