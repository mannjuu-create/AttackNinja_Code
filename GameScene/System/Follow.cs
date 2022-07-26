using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�����̈ړ�������X�N���v�g.
/// </summary>

public class Follow : MonoBehaviour
{
    //�v���C���[�̈ړ��X�N���v�g.
    [SerializeField] private PlayerMove move;

    //Vector3�^��ϐ�vector�Ő錾���܂��B
    Vector3 vector;

    //�v���C���[���S���ɏ���������.
    [SerializeField] private Vector3 deathPos;

    //GameObject�^��ϐ�Target�Ő錾���܂��B
    [SerializeField] private GameObject target;
    //float�^��ϐ�followSpeed�Ő錾���܂��B
    [SerializeField] private float followSpeed;

    void Start()
    {
        //�ʒu��Target�̈ʒu�����ɐݒ肷���B
        vector = target.transform.position - transform.position;
    }


    void Update()
    {
        //�ʒu���擾���ăX�s�[�h�����킹�Ă�����B
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