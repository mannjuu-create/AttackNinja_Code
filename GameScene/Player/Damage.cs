using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// プレイヤーがエネミーか樹にぶつかったらダメージを受け、
/// HPが0になったらリザルトに遷移するスクリプト.
/// </summary>

public class Damage : MonoBehaviour
{
    //////////////////////////////
    ///コンポーネント.
    //////////////////////////////
    [SerializeField] private int bloodImageNum;              //血のエフェクトの画像枚数.
    [SerializeField] private Image[] bloodImage;             //血のエフェクトの画像.
    [SerializeField] private Image redLightImage;            //赤いライトのエフェクトの画像.

    //////////////////////////////
    ///変数.
    //////////////////////////////
    [SerializeField] private int hp;                         //プレイヤーのHP.
    [SerializeField] private int damageNum;                  //ダメージを受けた回数.
    [SerializeField] private float bloodSpeed;               //血のエフェクトが消えるスピード.
    [SerializeField] private float LightSpeed;               //赤いライトのエフェクトが消えるスピード.

    //////////////////////////////
    ///スクリプト.
    //////////////////////////////
    [SerializeField] private PlayerMove move;                //移動スクリプト.
    [SerializeField] private GetTreasure treasure;           //宝箱獲得スクリプト.

    //////////////////////////////
    ///表示.
    //////////////////////////////
    [SerializeField] private Text HPText;                    //プレイヤーのHP.

    //////////////////////////////
    ///サウンド.
    //////////////////////////////
    [SerializeField] private AudioClip sound;
    AudioSource audioSource;

    void Start()
    {
        //エフェクトは最初に見えなくする.
        for (int i = 0; i < bloodImageNum; i++)
        {
            bloodImage[i].color = Color.clear;

        }
        redLightImage.color = Color.clear;

        //オーディオのComponentを取得.
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 時間が経過するにつれて徐々に透明にします。
        for (int i = 0; i < bloodImageNum; i++)
        {
            bloodImage[i].color = Color.Lerp(bloodImage[i].color, Color.clear, bloodSpeed * Time.deltaTime);
        }
        redLightImage.color = Color.Lerp(redLightImage.color, Color.clear, LightSpeed * Time.deltaTime);
    }

    //プレイヤーが敵か樹に衝突したら.
    void OnControllerColliderHit(ControllerColliderHit col)
    {
        if ((col.gameObject.tag == "enemy" ||
             col.gameObject.tag == "tree") &&
             move.knockbackFrag == false)
        {
            damage();
        }
    }

    //ダメージ処理.
    void damage()
    {
        hp--;
        damageNum++;
        HPText.text = string.Format("HP:{0}", hp);

        //音を鳴らす
        audioSource.PlayOneShot(sound);

        //画面を赤塗りにする
        for (int i = 0; i < damageNum; i++)
        {
            bloodImage[i].color = Color.magenta;
        }
        redLightImage.color = new Color(1, 0, 1, 0.8f);


        if (hp <= 0)
        {
            //ゲームオーバーフラグをtrueにする.
            move.gameOverFrag = true;
            //アニメーションセット.
            move.animator.SetTrigger("dead");
            //3秒後にゲームオーバー.
            Invoke("LoadResultScene", 3);
        }
        else
        {
            //アニメーションセット.
            move.animator.SetTrigger("damage");
        }

        //ノックバックフラグをtrueにする.
        move.knockbackFrag = true;
    }

    //リザルトシーン遷移時.
    void LoadResultScene()
    {
        //イベントに登録.
        SceneManager.sceneLoaded += ResultSceneLoaded;

        //シーン切り替え.
        SceneManager.LoadScene("ResultScene");
    }

    private void ResultSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        var Manager = GameObject.FindWithTag("TextManager").GetComponent<ScoreText>();

        // データを渡す処理
        Manager.finalScore = treasure.score;

        // イベントから削除
        SceneManager.sceneLoaded -= ResultSceneLoaded;
    }
}
