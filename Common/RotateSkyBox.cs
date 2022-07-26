using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{

    //�@��]�X�s�[�h
    [SerializeField]
    public float rotateSpeed = 0.5f;
    //�@�X�J�C�{�b�N�X�̃}�e���A��
    public Material skyboxMaterial;

    void Update()
    {
        //�@�X�J�C�{�b�N�X�}�e���A����Rotation�𑀍삵�Ċp�x��ω�������
        skyboxMaterial.SetFloat("_Rotation", Mathf.Repeat(skyboxMaterial.GetFloat("_Rotation") + rotateSpeed * Time.deltaTime, 360f));
    }
}