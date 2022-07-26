using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 入力によってプレイヤーのアクションを　クナイ投げ、火遁の術、ジャンプ
/// に切り替えるスクリプト.
/// </summary>

public class Action : MonoBehaviour
{
    //////////////////////////////
    ///共通.
    //////////////////////////////
    //Moveスクリプト.
    [SerializeField] private PlayerMove move;
    //それぞれのパラメーターの設定をInspectorで変える様にします。
    [SerializeField] private float speedJump;
    //どのアクションになっているかを管理す変数.
    [SerializeField] private int actionNum = 1;
    //火遁とクナイ投げの発射位置.
    [SerializeField] private Transform actionPos;
    //アクションをしたフラグ.
    [SerializeField] private bool actionPlayFrag = false;

    //////////////////////////////
    ///火遁の術関係.
    //////////////////////////////
    [SerializeField] private GameObject fireEffectObj;        //エフェクトオブジェクト.
    [SerializeField] private bool occurrenceFrag = false;     //エフェクト発生フラグ.

    //////////////////////////////
    ///クナイ関係.
    //////////////////////////////
    [SerializeField] private GameObject kunai;            //オブジェクト.
    [SerializeField] private float coolTime;              //クールタイム.
    private bool throwFrag = false;     //投げているかのフラグ.
    private float progressCount = 0;    //経過時間.

    //////////////////////////////
    ///アクションアイコン.
    //////////////////////////////
    //大きい時と小さい時のサイズ.
    [SerializeField] private float expansionSize;          // 大きい時
    [SerializeField] private float reductionSize;          // 小さい時
    //アクションチップの配列
    [SerializeField] private GameObject[] actionList;
    //actionListのRectTransform
    RectTransform rectTransform0;
    RectTransform rectTransform1;
    RectTransform rectTransform2;

    //////////////////////////////
    ///サウンド.
    //////////////////////////////
    [SerializeField] private AudioClip[] sound;
    AudioSource audioSource;

    void Start()
    {
        rectTransform0 = actionList[0].GetComponent<RectTransform>();
        rectTransform1 = actionList[1].GetComponent<RectTransform>();
        rectTransform2 = actionList[2].GetComponent<RectTransform>();

        //エフェクトを非アクティブにする.
        fireEffectObj.SetActive(false);

        //オーディオのComponentを取得.
        audioSource = GetComponent<AudioSource>();

        ChangeSize(1);
    }

    void Update()
    {
        changeAction();

        //アクション実行
        if (Input.GetKeyDown("space") &&
            !move.animator.GetCurrentAnimatorStateInfo(0).IsName("jump") &&
            move.knockbackFrag == false)
        {
            switch(actionNum)
            {
                case 0:
                    if(occurrenceFrag == false)
                    {
                        FireEffect();
                        actionPlayFrag = true;
                    }
                    break;
                case 1:
                    if (move.controller.isGrounded)
                    {
                        Jump();
                        actionPlayFrag = true;
                    }
                    break;
                case 2:
                    if(throwFrag == false)
                    {
                        throwKunai();
                        actionPlayFrag = true;
                    }
                    break;
            }
            //アクションをしたなら
            if (actionPlayFrag == true)
            {
                //音を鳴らす
                audioSource.PlayOneShot(sound[actionNum]);
                //フラグを下ろす
                actionPlayFrag = false;
            }
            
        }

        //火遁の術発動中にボタンを離したら消す.
        if (occurrenceFrag == true && Input.GetKeyUp("space"))
        {
            //エフェクトを非アクティブにする.
            fireEffectObj.SetActive(false);
            //エフェクト発生フラグをfalseにする.
            occurrenceFrag = false;
            //音を止める.
            audioSource.Stop();
        }

        //クナイを投げた後の経過時間.
        if (throwFrag == true)
        {
            //経過時間をプラス.
            progressCount += Time.deltaTime;
            //一定時間経過したら投げているフラグをおろす.
            if(progressCount >= coolTime)
            {
                throwFrag = false;
                progressCount = 0;
            }
        }
    }

    //アクション切り替え
    void changeAction()
    {
        if (Input.GetKeyDown("left"))
        {
            if (actionNum == 0)
            {
                actionNum = 2;
            }
            else
            {
                actionNum--;
            }
        }

        if (Input.GetKeyDown("right"))
        {
            if (actionNum == 2)
            {
                actionNum = 0;
            }
            else
            {
                actionNum++;
            }
        }
        ChangeSize(actionNum);
    }

    //火遁の術.
    void FireEffect()
    {
        //エフェクトをアクティブにする.
        fireEffectObj.SetActive(true);
        //エフェクト発生フラグをtrueにする.
        occurrenceFrag = true;
    }

    //ジャンプ.
    public void Jump()
    {
        move.moveDirection.y = speedJump;

        move.animator.SetTrigger("jump");
    }

    //クナイ投げ.
    public void throwKunai()
    {
        //弾丸の複製
        GameObject bullets = Instantiate(kunai) as GameObject;

        // 弾丸の位置を調整
        bullets.transform.position = actionPos.position;

        //投げたフラグを立てる.
        throwFrag = true;
    }

    //アクションアイコンの大きさ変更.
    void ChangeSize(int x)
    {
        switch (x)
        {
            case 0:
                rectTransform0.sizeDelta = new Vector2(expansionSize, expansionSize);
                rectTransform1.sizeDelta = new Vector2(reductionSize, reductionSize);
                rectTransform2.sizeDelta = new Vector2(reductionSize, reductionSize);
                break;
            case 1:
                rectTransform0.sizeDelta = new Vector2(reductionSize, reductionSize);
                rectTransform1.sizeDelta = new Vector2(expansionSize, expansionSize);
                rectTransform2.sizeDelta = new Vector2(reductionSize, reductionSize);
                break;
            case 2:
                rectTransform0.sizeDelta = new Vector2(reductionSize, reductionSize);
                rectTransform1.sizeDelta = new Vector2(reductionSize, reductionSize);
                rectTransform2.sizeDelta = new Vector2(expansionSize, expansionSize);
                break;
        }
    }
}
