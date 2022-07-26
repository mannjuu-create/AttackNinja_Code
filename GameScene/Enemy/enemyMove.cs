using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�����̋����܂ŋ߂Â�����
/// �����̏ォ���э~���X�N���v�g.
/// </summary>

public class enemyMove : MonoBehaviour
{
    //�v���C���[�I�u�W�F�N�g.
    [SerializeField] private GameObject playerObj;

    //////////////////////////////
    ///�V�X�e��.
    /////////////////////////////////
    //rigidbody��錾.
    public Rigidbody rb;
    //BoxCollider��錾.
    public BoxCollider col;
    //Animator�^��ϐ�animator�Ő錾���܂��B
    public Animator animator;

    //////////////////////////////
    ///�W�����v�֌W.
    //////////////////////////////
    [SerializeField] private Vector3 moveDirection = Vector3.zero;    //���ꂼ��̍��W���O�Ő錾���܂��B
    [SerializeField] private float distance;                          //��э~��鋗��.
    [SerializeField] private float nowDistance = 0;                   //�v���C���[�Ƃ̌��݂̋���.
    [SerializeField] private Vector3 force;                           //�ŏI�I�ȃW�����v��.
    [SerializeField] private float powerX;                            //�W�����v��(��).
    [SerializeField] private float sidePos;                           //���ړ��Ŗڎw���ʒu.
    [SerializeField] private float powerY;                            //�W�����v��(����).
    [SerializeField] private float powerZ;                            //�W�����v��(���s��).
    [SerializeField] private bool jumpFrag = false;                   //�W�����v�������ǂ���.
    [SerializeField] private bool leftFrag;                           //�v���C���[���猩�č�����.
    [SerializeField] private bool centerFrag;                         //�v���C���[���猩�Đ^�񒆂�.
    [SerializeField] private bool rightFrag;                          //�v���C���[���猩�ĉE����.
    [SerializeField] private float sideMax;                           //���ړ��̌��E�l.

    //���S�t���O.
    public bool deathFrag = false;

    void Start()
    {
        //rigidbody���擾.
        rb = this.GetComponent<Rigidbody>();
        //BoxCollider���擾.
        col = this.GetComponent<BoxCollider>();
        //GetComponent��Animator���擾���ĕϐ�animator�ŎQ�Ƃ��܂��B
        animator = GetComponent<Animator>();

        //�v���C���[���擾.
        playerObj = GameObject.Find("player");

        //���ړ��̋������v�Z.
        powerX = (sidePos - this.transform.position.x) / 2;
        force = new Vector3(powerX, powerY, powerZ);
    }
        

    void Update()
    {
        //�v���C���[�Ƃ̋������v�Z.
        nowDistance = this.transform.position.z - playerObj.transform.position.z;
        //�v���C���[�Ƃ̋�������苗���ɋ߂Â������э~���.
        if (nowDistance <= distance && jumpFrag == false)
        {
            rb.AddForce(force, ForceMode.Impulse);  // �͂�������
            animator.SetTrigger("jump");            // �W�����v�A�j���[�V�����Đ�.
            jumpFrag = true;                        // �W�����v�ς݃t���O��true�ɂ���.
        }

        Jump();
    }

    //�W�����v����.
    void Jump()
    {
        //�v���C���[���猩�č����Ȃ�.
        if (leftFrag == true)
        {
            //�^�񒆂ɒ��n����Ȃ�.
            if (centerFrag == true)
            {
                if (this.transform.position.x >= sideMax)
                {
                    var tmp = rb.velocity; // ���݂̑��x�����
                    tmp.x = 0;             // X�����x�����ł�����
                    rb.velocity = tmp;     // �đ��
                }
            }
            else
            {
                if (this.transform.position.x >= sideMax)
                {
                    var tmp = rb.velocity; // ���݂̑��x�����
                    tmp.x = 0;             // X�����x�����ł�����
                    rb.velocity = tmp;     // �đ��
                }
            }
        }
        else if (rightFrag == true)
        {
            //�^�񒆂ɒ��n����Ȃ�.
            if (centerFrag == true)
            {
                if (this.transform.position.x <= sideMax)
                {
                    var tmp = rb.velocity; // ���݂̑��x�����
                    tmp.x = 0;             // X�����x�����ł�����
                    rb.velocity = tmp;     // �đ��
                }
            }
            else
            {
                if (this.transform.position.x <= sideMax)
                {
                    var tmp = rb.velocity; // ���݂̑��x�����
                    tmp.x = 0;             // X�����x�����ł�����
                    rb.velocity = tmp;     // �đ��
                }
            }
        }
    }
}
