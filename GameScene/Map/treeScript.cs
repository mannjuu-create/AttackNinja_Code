using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ƃv���C���[�̉��G�t�F�N�g���Փ˂����ۂ�
/// �������ł�����X�N���v�g.
/// </summary>

public class treeScript : MonoBehaviour
{
    MeshRenderer mesh;
    Color alpha = new Color(0, 0, 0, 0.05f);

    private bool hitFrag = false;                //�R�₳�ꂽ�t���O.

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if(hitFrag == true)
        {
            mesh.material.color -= alpha;
            if (mesh.material.color.a <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "fire")
        {
            hitFrag = true;
        }
    }
}
