using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// �v���C���[���G�l�~�[�����ɂԂ�������_���[�W���󂯁A
/// HP��0�ɂȂ����烊�U���g�ɑJ�ڂ���X�N���v�g.
/// </summary>

public class Damage : MonoBehaviour
{
    //////////////////////////////
    ///�R���|�[�l���g.
    //////////////////////////////
    [SerializeField] private int bloodImageNum;              //���̃G�t�F�N�g�̉摜����.
    [SerializeField] private Image[] bloodImage;             //���̃G�t�F�N�g�̉摜.
    [SerializeField] private Image redLightImage;            //�Ԃ����C�g�̃G�t�F�N�g�̉摜.

    //////////////////////////////
    ///�ϐ�.
    //////////////////////////////
    [SerializeField] private int hp;                         //�v���C���[��HP.
    [SerializeField] private int damageNum;                  //�_���[�W���󂯂���.
    [SerializeField] private float bloodSpeed;               //���̃G�t�F�N�g��������X�s�[�h.
    [SerializeField] private float LightSpeed;               //�Ԃ����C�g�̃G�t�F�N�g��������X�s�[�h.

    //////////////////////////////
    ///�X�N���v�g.
    //////////////////////////////
    [SerializeField] private PlayerMove move;                //�ړ��X�N���v�g.
    [SerializeField] private GetTreasure treasure;           //�󔠊l���X�N���v�g.

    //////////////////////////////
    ///�\��.
    //////////////////////////////
    [SerializeField] private Text HPText;                    //�v���C���[��HP.

    //////////////////////////////
    ///�T�E���h.
    //////////////////////////////
    [SerializeField] private AudioClip sound;
    AudioSource audioSource;

    void Start()
    {
        //�G�t�F�N�g�͍ŏ��Ɍ����Ȃ�����.
        for (int i = 0; i < bloodImageNum; i++)
        {
            bloodImage[i].color = Color.clear;

        }
        redLightImage.color = Color.clear;

        //�I�[�f�B�I��Component���擾.
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ���Ԃ��o�߂���ɂ�ď��X�ɓ����ɂ��܂��B
        for (int i = 0; i < bloodImageNum; i++)
        {
            bloodImage[i].color = Color.Lerp(bloodImage[i].color, Color.clear, bloodSpeed * Time.deltaTime);
        }
        redLightImage.color = Color.Lerp(redLightImage.color, Color.clear, LightSpeed * Time.deltaTime);
    }

    //�v���C���[���G�����ɏՓ˂�����.
    void OnControllerColliderHit(ControllerColliderHit col)
    {
        if ((col.gameObject.tag == "enemy" ||
             col.gameObject.tag == "tree") &&
             move.knockbackFrag == false)
        {
            damage();
        }
    }

    //�_���[�W����.
    void damage()
    {
        hp--;
        damageNum++;
        HPText.text = string.Format("HP:{0}", hp);

        //����炷
        audioSource.PlayOneShot(sound);

        //��ʂ�ԓh��ɂ���
        for (int i = 0; i < damageNum; i++)
        {
            bloodImage[i].color = Color.magenta;
        }
        redLightImage.color = new Color(1, 0, 1, 0.8f);


        if (hp <= 0)
        {
            //�Q�[���I�[�o�[�t���O��true�ɂ���.
            move.gameOverFrag = true;
            //�A�j���[�V�����Z�b�g.
            move.animator.SetTrigger("dead");
            //3�b��ɃQ�[���I�[�o�[.
            Invoke("LoadResultScene", 3);
        }
        else
        {
            //�A�j���[�V�����Z�b�g.
            move.animator.SetTrigger("damage");
        }

        //�m�b�N�o�b�N�t���O��true�ɂ���.
        move.knockbackFrag = true;
    }

    //���U���g�V�[���J�ڎ�.
    void LoadResultScene()
    {
        //�C�x���g�ɓo�^.
        SceneManager.sceneLoaded += ResultSceneLoaded;

        //�V�[���؂�ւ�.
        SceneManager.LoadScene("ResultScene");
    }

    private void ResultSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // �V�[���؂�ւ���̃X�N���v�g���擾
        var Manager = GameObject.FindWithTag("TextManager").GetComponent<ScoreText>();

        // �f�[�^��n������
        Manager.finalScore = treasure.score;

        // �C�x���g����폜
        SceneManager.sceneLoaded -= ResultSceneLoaded;
    }
}
