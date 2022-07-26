using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �󔠂ƏՓˎ��ɕ󔠂������ăX�R�A�𑝂₷�X�N���v�g.
/// </summary>

public class GetTreasure : MonoBehaviour
{
    //////////////////////////////
    ///�X�R�A.
    //////////////////////////////
    [SerializeField] private Text ScoreText;
    public int score = 0;

    //////////////////////////////
    ///�T�E���h.
    //////////////////////////////
    [SerializeField] private AudioClip sound;
    AudioSource audioSource;

    void Start()
    {
        //�I�[�f�B�I��Component���擾.
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "treasure")
        {
            score++;
            ScoreText.text = string.Format("GetTreasure:{0}", score);

            //����炷
            audioSource.PlayOneShot(sound);

            Destroy(col.gameObject);
        }
    }
}
