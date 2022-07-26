using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeImageSize : MonoBehaviour
{
    //拡大縮小の切り替えの時間.
    public float changeTime;
    //拡大縮小を行う変数.
    public float changeSpeed;

    //経過時間.
    private float time;
    //拡大縮小が切り替わったフラグ.
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