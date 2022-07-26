using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���͂ɂ���ăv���C���[�̃A�N�V�������@�N�i�C�����A�Γق̏p�A�W�����v
/// �ɐ؂�ւ���X�N���v�g.
/// </summary>

public class Action : MonoBehaviour
{
    //////////////////////////////
    ///����.
    //////////////////////////////
    //Move�X�N���v�g.
    [SerializeField] private PlayerMove move;
    //���ꂼ��̃p�����[�^�[�̐ݒ��Inspector�ŕς���l�ɂ��܂��B
    [SerializeField] private float speedJump;
    //�ǂ̃A�N�V�����ɂȂ��Ă��邩���Ǘ����ϐ�.
    [SerializeField] private int actionNum = 1;
    //�ΓقƃN�i�C�����̔��ˈʒu.
    [SerializeField] private Transform actionPos;
    //�A�N�V�����������t���O.
    [SerializeField] private bool actionPlayFrag = false;

    //////////////////////////////
    ///�Γق̏p�֌W.
    //////////////////////////////
    [SerializeField] private GameObject fireEffectObj;        //�G�t�F�N�g�I�u�W�F�N�g.
    [SerializeField] private bool occurrenceFrag = false;     //�G�t�F�N�g�����t���O.

    //////////////////////////////
    ///�N�i�C�֌W.
    //////////////////////////////
    [SerializeField] private GameObject kunai;            //�I�u�W�F�N�g.
    [SerializeField] private float coolTime;              //�N�[���^�C��.
    private bool throwFrag = false;     //�����Ă��邩�̃t���O.
    private float progressCount = 0;    //�o�ߎ���.

    //////////////////////////////
    ///�A�N�V�����A�C�R��.
    //////////////////////////////
    //�傫�����Ə��������̃T�C�Y.
    [SerializeField] private float expansionSize;          // �傫����
    [SerializeField] private float reductionSize;          // ��������
    //�A�N�V�����`�b�v�̔z��
    [SerializeField] private GameObject[] actionList;
    //actionList��RectTransform
    RectTransform rectTransform0;
    RectTransform rectTransform1;
    RectTransform rectTransform2;

    //////////////////////////////
    ///�T�E���h.
    //////////////////////////////
    [SerializeField] private AudioClip[] sound;
    AudioSource audioSource;

    void Start()
    {
        rectTransform0 = actionList[0].GetComponent<RectTransform>();
        rectTransform1 = actionList[1].GetComponent<RectTransform>();
        rectTransform2 = actionList[2].GetComponent<RectTransform>();

        //�G�t�F�N�g���A�N�e�B�u�ɂ���.
        fireEffectObj.SetActive(false);

        //�I�[�f�B�I��Component���擾.
        audioSource = GetComponent<AudioSource>();

        ChangeSize(1);
    }

    void Update()
    {
        changeAction();

        //�A�N�V�������s
        if (Input.GetKeyDown("space") &&
            !move.animator.GetCurrentAnimatorStateInfo(0).IsName("jump") &&
            move.knockbackFrag == false)
        {
            switch(actionNum)
            {
                case 0:
                    if(occurrenceFrag == false)
                    {
                        FireEffect();
                        actionPlayFrag = true;
                    }
                    break;
                case 1:
                    if (move.controller.isGrounded)
                    {
                        Jump();
                        actionPlayFrag = true;
                    }
                    break;
                case 2:
                    if(throwFrag == false)
                    {
                        throwKunai();
                        actionPlayFrag = true;
                    }
                    break;
            }
            //�A�N�V�����������Ȃ�
            if (actionPlayFrag == true)
            {
                //����炷
                audioSource.PlayOneShot(sound[actionNum]);
                //�t���O�����낷
                actionPlayFrag = false;
            }
            
        }

        //�Γق̏p�������Ƀ{�^���𗣂��������.
        if (occurrenceFrag == true && Input.GetKeyUp("space"))
        {
            //�G�t�F�N�g���A�N�e�B�u�ɂ���.
            fireEffectObj.SetActive(false);
            //�G�t�F�N�g�����t���O��false�ɂ���.
            occurrenceFrag = false;
            //�����~�߂�.
            audioSource.Stop();
        }

        //�N�i�C�𓊂�����̌o�ߎ���.
        if (throwFrag == true)
        {
            //�o�ߎ��Ԃ��v���X.
            progressCount += Time.deltaTime;
            //��莞�Ԍo�߂����瓊���Ă���t���O�����낷.
            if(progressCount >= coolTime)
            {
                throwFrag = false;
                progressCount = 0;
            }
        }
    }

    //�A�N�V�����؂�ւ�
    void changeAction()
    {
        if (Input.GetKeyDown("left"))
        {
            if (actionNum == 0)
            {
                actionNum = 2;
            }
            else
            {
                actionNum--;
            }
        }

        if (Input.GetKeyDown("right"))
        {
            if (actionNum == 2)
            {
                actionNum = 0;
            }
            else
            {
                actionNum++;
            }
        }
        ChangeSize(actionNum);
    }

    //�Γق̏p.
    void FireEffect()
    {
        //�G�t�F�N�g���A�N�e�B�u�ɂ���.
        fireEffectObj.SetActive(true);
        //�G�t�F�N�g�����t���O��true�ɂ���.
        occurrenceFrag = true;
    }

    //�W�����v.
    public void Jump()
    {
        move.moveDirection.y = speedJump;

        move.animator.SetTrigger("jump");
    }

    //�N�i�C����.
    public void throwKunai()
    {
        //�e�ۂ̕���
        GameObject bullets = Instantiate(kunai) as GameObject;

        // �e�ۂ̈ʒu�𒲐�
        bullets.transform.position = actionPos.position;

        //�������t���O�𗧂Ă�.
        throwFrag = true;
    }

    //�A�N�V�����A�C�R���̑傫���ύX.
    void ChangeSize(int x)
    {
        switch (x)
        {
            case 0:
                rectTransform0.sizeDelta = new Vector2(expansionSize, expansionSize);
                rectTransform1.sizeDelta = new Vector2(reductionSize, reductionSize);
                rectTransform2.sizeDelta = new Vector2(reductionSize, reductionSize);
                break;
            case 1:
                rectTransform0.sizeDelta = new Vector2(reductionSize, reductionSize);
                rectTransform1.sizeDelta = new Vector2(expansionSize, expansionSize);
                rectTransform2.sizeDelta = new Vector2(reductionSize, reductionSize);
                break;
            case 2:
                rectTransform0.sizeDelta = new Vector2(reductionSize, reductionSize);
                rectTransform1.sizeDelta = new Vector2(reductionSize, reductionSize);
                rectTransform2.sizeDelta = new Vector2(expansionSize, expansionSize);
                break;
        }
    }
}
