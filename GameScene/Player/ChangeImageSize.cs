using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeImageSize : MonoBehaviour
{
    //Šg‘åk¬‚ÌØ‚è‘Ö‚¦‚ÌŠÔ.
    public float changeTime;
    //Šg‘åk¬‚ğs‚¤•Ï”.
    public float changeSpeed;

    //Œo‰ßŠÔ.
    private float time;
    //Šg‘åk¬‚ªØ‚è‘Ö‚í‚Á‚½ƒtƒ‰ƒO.
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