using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���U���g��ʕ\�����ɍŏI�X�R�A���X�R�A�e�L�X�g��
/// �擾������X�N���v�g.
/// </summary>

public class ScoreText : MonoBehaviour
{
    public int finalScore;              //�X�R�A.

    // �\��.
    [SerializeField] private Text scoreText;

    void Start()
    {
        scoreText.text = string.Format("FinalScore:{0}00��", finalScore);
    }
}
