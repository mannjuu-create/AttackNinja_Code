using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// �e�L�X�g��Push To Space���̃T�C�Y�����Ԍo�߂ŕύX����X�N���v�g.
/// </summary>

public class ChangeTextSize : MonoBehaviour
{
    //�g��k���̐؂�ւ��̎���.
    [SerializeField] private float changeTime;
    //�g��k�����s���ϐ�.
    [SerializeField] private float changeSpeed;

    //�o�ߎ���.
    private float time;
    //�g��k�����؂�ւ�����t���O.
    private bool returnFrag;

    void Start()
    {
        returnFrag = true;
    }

    void Update()
    {
        changeSpeed = Time.deltaTime * 0.3f;

        if (time < 0)
        {
            returnFrag = true;
        }
        if (time > changeTime)
        {
            returnFrag = false;
        }

        if (returnFrag == true)
        {
            time += Time.deltaTime;
            transform.localScale += new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }
        else
        {
            time -= Time.deltaTime;
            transform.localScale -= new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }
    }
}