using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �N�i�C�ƏՓ˂�����N�i�C���폜���Ď��S�A�j���[�V������
/// �Đ�����X�N���v�g.
/// </summary>

public class HitKunai : MonoBehaviour
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
        if (col.gameObject.tag == "kunai" && move.deathFrag == false)
        {
            //���������N�i�C�폜.
            Destroy(col.gameObject);

            //����炷
            audioSource.PlayOneShot(sound);

            //�d�͍폜.
            move.rb.useGravity = false;
            //�����蔻��폜.
            move.col.enabled = false;
            //���S�t���O�𗧂Ă�.
            move.deathFrag = true;
            //�A�j���[�V�����Z�b�g.
            move.animator.SetTrigger("death");
            //3�b��ɃQ�[���I�[�o�[.
            Destroy(gameObject, 2.0f);
        }
    }
}
