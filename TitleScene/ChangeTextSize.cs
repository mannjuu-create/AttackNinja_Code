using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// テキスト＜Push To Space＞のサイズを時間経過で変更するスクリプト.
/// </summary>

public class ChangeTextSize : MonoBehaviour
{
    //拡大縮小の切り替えの時間.
    [SerializeField] private float changeTime;
    //拡大縮小を行う変数.
    [SerializeField] private float changeSpeed;

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