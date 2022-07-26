using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �N�i�C���ړ��A���Ԍo��or���ɏՓ˂ŏ��ł�����X�N���v�g.
/// </summary>

public class kunaiScript : MonoBehaviour
{
    //�ړ����x.
    [SerializeField] private float speed;
    //�o�ߎ���.
    private float progressCount = 0;
    //������܂ł̎���.
    [SerializeField] private float disappearTime;

    void Start()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();   // rigidbody���擾
        Vector3 force = new Vector3(0.0f, 0.0f, speed);  // �͂�ݒ�
        rb.AddForce(force, ForceMode.Impulse);           // �͂�������
    }

    void Update()
    {
        progressCount += Time.deltaTime;

        //��莞�Ԃŏ���.
        if(progressCount >= disappearTime)
        {
            Destroy(this.gameObject);
        }
    }

    //���ɂԂ����������.
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "tree")
        {
            Destroy(this.gameObject);
        }
    }
}
