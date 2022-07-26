using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 古いマップを削除して新しくマップを作成するスクリプト.
/// </summary>

public class Map : MonoBehaviour
{
    //int型を変数StageTipSizeで宣言します。
    const int StageTipSize = 60;
    //int型を変数currentTipIndexで宣言します。
    [SerializeField] private int currentTipIndex;
    //ターゲットキャラクターの指定が出来る様にするよ
    [SerializeField] private Transform character;
    //ステージチップの配列
    [SerializeField] private GameObject[] stageTips;
    //自動生成する時に使う変数startTipIndex
    [SerializeField] private int startTipIndex;
    //ステージ生成の先読み個数
    [SerializeField] private int preInstantiate;
    //作ったステージチップの保持リスト
    [SerializeField] private List<GameObject> generatedStageList = new List<GameObject>();

    //一回目のマップ作製は固定.
    private bool firstFrag = true;
    //前回作ったステージのナンバー.
    private int PreStageNum;
    //上に上がっていくのステージの番号.
    private const int UpStageNum = 5;
    //降りるステージの番号.
    private const int DownStageNum = 6;
    //上のステージの番号.
    private const int OnStageNum = 7;

    void Start()
    {
        //初期化処理
        currentTipIndex = startTipIndex - 1;
        UpdateStage(preInstantiate);
    }


    void Update()
    {
        //キャラクターの位置から現在のステージチップのインデックスを計算します
        int charaPositionIndex = (int)(character.position.z / StageTipSize);
        //次のステージチップに入ったらステージの更新処理を行います。
        if (charaPositionIndex + preInstantiate > currentTipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }

    }
    //指定のインデックスまでのステージチップを生成して、管理下におく
    void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= currentTipIndex) return;
        //指定のステージチップまで生成するよ
        for (int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);
            //生成したステージチップを管理リストに追加して、
            generatedStageList.Add(stageObject);
        }
        //ステージ保持上限になるまで古いステージを削除します。
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

        currentTipIndex = toTipIndex;
    }
    //指定のインデックス位置にstageオブジェクトをランダムに生成
    GameObject GenerateStage(int tipIndex)
    {
        int nextStageTip;
        //一回目なら固定.
        if (firstFrag == true)
        {
            nextStageTip = 0;
            firstFrag = false;
        }
        else
        {
            if(PreStageNum == UpStageNum)
            {
                nextStageTip = OnStageNum;
            }
            else if(PreStageNum == OnStageNum)
            {
                nextStageTip = DownStageNum;
            }
            else
            {
                //上限定のステージと降りる用のステージをランダムに含ませないように-2する
                nextStageTip = Random.Range(0, (stageTips.Length - 2));
            }
        }

        GameObject stageObject = (GameObject)Instantiate(
            stageTips[nextStageTip],
            new Vector3(0, 0, tipIndex * StageTipSize),
            Quaternion.identity);
        //何番のステージを作ったかを保存.
        PreStageNum = nextStageTip;

        return stageObject;
    }
    //一番古いステージを削除します
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }

}