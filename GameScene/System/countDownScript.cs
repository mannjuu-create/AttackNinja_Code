using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �Q�[���J�n���̃J�E���g�_�E�����s���X�N���v�g.
/// </summary>

public class countDownScript : MonoBehaviour
{
    //�J�E���g�̎���.
    [SerializeField] private float countTime;
    //�Q�[�����n�܂����t���O.
    public bool StartFrag = false;

    // �e�L�X�g.
    [SerializeField] private TextMeshProUGUI CountText;

    void Start()
    {
        //�J�E���g���I�������uStart�v�ƕ\�����邽�߁A1�b�v���X����.
        countTime += 1;
    }

    void Update()
    {
        if(StartFrag == false)
        {
            //�J�E���g�_�E��.
            countTime -= Time.deltaTime;
            //�e�L�X�g�ύX.
            TextCountChange(countTime);

            //�J�E���g�_�E�����I�������Q�[���X�^�[�g.
            if (countTime <= 0)
            {
                StartFrag = true;
            }
        }
    }

    void TextCountChange(float countdown)
    {
        if(countdown > 3)
        {
            CountText.text = string.Format("3");
        }
        else if(countdown > 2)
        {
            CountText.text = string.Format("2");
        }
        else if(countdown > 1)
        {
            CountText.text = string.Format("1");
        }
        else if(countdown > 0)
        {
            CountText.text = string.Format("start!");
        }
        else
        {
            CountText.text = string.Format("");
        }
    }
}
