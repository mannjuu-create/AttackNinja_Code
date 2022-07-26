using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeImageSize : MonoBehaviour
{
    //�g��k���̐؂�ւ��̎���.
    public float changeTime;
    //�g��k�����s���ϐ�.
    public float changeSpeed;

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