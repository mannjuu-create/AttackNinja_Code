using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Â��}�b�v���폜���ĐV�����}�b�v���쐬����X�N���v�g.
/// </summary>

public class Map : MonoBehaviour
{
    //int�^��ϐ�StageTipSize�Ő錾���܂��B
    const int StageTipSize = 60;
    //int�^��ϐ�currentTipIndex�Ő錾���܂��B
    [SerializeField] private int currentTipIndex;
    //�^�[�Q�b�g�L�����N�^�[�̎w�肪�o����l�ɂ����
    [SerializeField] private Transform character;
    //�X�e�[�W�`�b�v�̔z��
    [SerializeField] private GameObject[] stageTips;
    //�����������鎞�Ɏg���ϐ�startTipIndex
    [SerializeField] private int startTipIndex;
    //�X�e�[�W�����̐�ǂ݌�
    [SerializeField] private int preInstantiate;
    //������X�e�[�W�`�b�v�̕ێ����X�g
    [SerializeField] private List<GameObject> generatedStageList = new List<GameObject>();

    //���ڂ̃}�b�v�쐻�͌Œ�.
    private bool firstFrag = true;
    //�O�������X�e�[�W�̃i���o�[.
    private int PreStageNum;
    //��ɏオ���Ă����̃X�e�[�W�̔ԍ�.
    private const int UpStageNum = 5;
    //�~���X�e�[�W�̔ԍ�.
    private const int DownStageNum = 6;
    //��̃X�e�[�W�̔ԍ�.
    private const int OnStageNum = 7;

    void Start()
    {
        //����������
        currentTipIndex = startTipIndex - 1;
        UpdateStage(preInstantiate);
    }


    void Update()
    {
        //�L�����N�^�[�̈ʒu���猻�݂̃X�e�[�W�`�b�v�̃C���f�b�N�X���v�Z���܂�
        int charaPositionIndex = (int)(character.position.z / StageTipSize);
        //���̃X�e�[�W�`�b�v�ɓ�������X�e�[�W�̍X�V�������s���܂��B
        if (charaPositionIndex + preInstantiate > currentTipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }

    }
    //�w��̃C���f�b�N�X�܂ł̃X�e�[�W�`�b�v�𐶐����āA�Ǘ����ɂ���
    void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= currentTipIndex) return;
        //�w��̃X�e�[�W�`�b�v�܂Ő��������
        for (int i = currentTipIndex + 1; i <= toTipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);
            //���������X�e�[�W�`�b�v���Ǘ����X�g�ɒǉ����āA
            generatedStageList.Add(stageObject);
        }
        //�X�e�[�W�ێ�����ɂȂ�܂ŌÂ��X�e�[�W���폜���܂��B
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage();

        currentTipIndex = toTipIndex;
    }
    //�w��̃C���f�b�N�X�ʒu��stage�I�u�W�F�N�g�������_���ɐ���
    GameObject GenerateStage(int tipIndex)
    {
        int nextStageTip;
        //���ڂȂ�Œ�.
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
                //�����̃X�e�[�W�ƍ~���p�̃X�e�[�W�������_���Ɋ܂܂��Ȃ��悤��-2����
                nextStageTip = Random.Range(0, (stageTips.Length - 2));
            }
        }

        GameObject stageObject = (GameObject)Instantiate(
            stageTips[nextStageTip],
            new Vector3(0, 0, tipIndex * StageTipSize),
            Quaternion.identity);
        //���Ԃ̃X�e�[�W�����������ۑ�.
        PreStageNum = nextStageTip;

        return stageObject;
    }
    //��ԌÂ��X�e�[�W���폜���܂�
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }

}