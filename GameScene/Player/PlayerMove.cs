using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̈ړ��𐧌䂷��X�N���v�g.
/// </summary>

public class PlayerMove : MonoBehaviour
{
    //�Q�[���J�n���̃J�E���g�_�E��.
    [SerializeField] private countDownScript countDown;

    //���[���̈ړ��̐��l�����ꂼ��̕ϐ��Ő錾���܂��B
    const int   MinLane = -1;
    const int   MaxLane = 1;
    const float LaneWidth = 5.0f;

    //CharacterController�^��ϐ�controller�Ő錾���܂��B
    public CharacterController controller;
    //Animator�^��ϐ�animator�Ő錾���܂��B
    public Animator animator;

    //���ꂼ��̍��W���O�Ő錾���܂��B
    public Vector3 moveDirection = Vector3.zero;        //���[�J�����.
    [SerializeField] private Vector3 globalDirection = Vector3.zero;      //���[���h���.
    //int�^��ϐ�targetLane�Ő錾���܂��B
    [SerializeField] private int targetLane = 0;
    //���ړ��̂��߂̕ϐ�.
    [SerializeField] private float ratioX = 0;
    [SerializeField] private float moveCount = 0;
    [SerializeField] private bool  moveFrag = false;
    const float moveMax = 0.5f;

    //���ꂼ��̃p�����[�^�[�̐ݒ��Inspector�ŕς���l�ɂ��܂��B
    [SerializeField] private float gravity;
    [SerializeField] private float speedZ;
    [SerializeField] private float speedX;
    [SerializeField] private float accelerationZ;
    [SerializeField] private float speedKnockback;
    [SerializeField] private float knockbackCount;
    [SerializeField] private float knockbackMax;

    //�m�b�N�o�b�N�p�t���O.
    public bool knockbackFrag = false;
    //�Q�[���I�[�o�[�t���O.
    public bool gameOverFrag = false;

    void Start()
    {
        //GetComponent��CharacterController���擾���ĕϐ�controllse�ŎQ�Ƃ��܂��B
        controller = GetComponent<CharacterController>();
        //GetComponent��Animator���擾���ĕϐ�animator�ŎQ�Ƃ��܂��B
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if(countDown.StartFrag == true && gameOverFrag == false)
        {
            //���ꂼ��̖�󂪉����ꂽ�炻�ꂼ��̊֐������s���܂��B
            if (Input.GetKeyDown(KeyCode.A) &&
                !animator.GetCurrentAnimatorStateInfo(0).IsName("jump") &&
                knockbackFrag == false) MoveToLeft();

            if (Input.GetKeyDown(KeyCode.D) &&
                !animator.GetCurrentAnimatorStateInfo(0).IsName("jump") &&
                knockbackFrag == false) MoveToRight();

            //�O�i.
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            //���ړ�.
            if (moveFrag == true)
            {
                moveCount += Time.deltaTime;
                //���̋����ɂȂ�����~�߂�.
                if (moveCount >= moveMax)
                {
                    moveDirection.x = 0;
                    moveFrag = false;
                }
            }

            //�d��.
            moveDirection.y -= gravity * Time.deltaTime;

            //�m�b�N�o�b�N.
            if (knockbackFrag == true)
            {
                Knockback();
            }

            globalDirection = transform.TransformDirection(moveDirection);
            controller.Move(globalDirection * Time.deltaTime);

            //�ڒn����.
            if (controller.isGrounded) moveDirection.y = 0;

            //����A�j���[�V�����Z�b�g.
            animator.SetBool("run", moveDirection.z > 0.0f);
        }
    }

    //�V����������֐��̂��ꂼ��̏����B
    public void SideMove()
    {
        ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
        moveDirection.x = ratioX * speedX;
    }

    public void MoveToLeft()
    {
        if (controller.isGrounded && targetLane > MinLane) targetLane--;
        SideMove();

        moveFrag = true;
        moveCount = 0;
    }

    public void MoveToRight()
    {
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
        SideMove();
        
        moveFrag = true;
        moveCount = 0;
    }

    public void Knockback()
    {
        //�X�s�[�h�ύX.
        moveDirection.z = speedKnockback;
        //��莞�Ԍo�߂�����m�b�N�o�b�N����߂�.
        if (knockbackCount >= knockbackMax)
        {
            knockbackFrag = false;
            knockbackCount = 0;
        }
        //�o�ߎ��Ԃ��v���X.
        knockbackCount += Time.deltaTime;
    }
}