using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{

    //　回転スピード
    [SerializeField]
    public float rotateSpeed = 0.5f;
    //　スカイボックスのマテリアル
    public Material skyboxMaterial;

    void Update()
    {
        //　スカイボックスマテリアルのRotationを操作して角度を変化させる
        skyboxMaterial.SetFloat("_Rotation", Mathf.Repeat(skyboxMaterial.GetFloat("_Rotation") + rotateSpeed * Time.deltaTime, 360f));
    }
}