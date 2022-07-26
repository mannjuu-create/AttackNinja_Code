using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���G�l�~�[�ɋ߂Â�����U���A�j���[�V������
/// �Đ�����X�N���v�g.
/// </summary>

public class enemyAttack : MonoBehaviour
{
    //Move�X�N���v�g.
    [SerializeField] private enemyMove move;

    //////////////////////////////
    ///�T�E���h.
    //////////////////////////////
    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        //�I�[�f�B�I��Component���擾.
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(move.deathFrag == false)
        {
            if (col.gameObject.tag == "Player")
            {
                move.animator.SetBool("attack", true);
                //����炷
                audioSource.PlayOneShot(sound);
            }
        }
    }
}