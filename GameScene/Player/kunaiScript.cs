using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クナイを移動、時間経過or樹に衝突で消滅させるスクリプト.
/// </summary>

public class kunaiScript : MonoBehaviour
{
    //移動速度.
    [SerializeField] private float speed;
    //経過時間.
    private float progressCount = 0;
    //消えるまでの時間.
    [SerializeField] private float disappearTime;

    void Start()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();   // rigidbodyを取得
        Vector3 force = new Vector3(0.0f, 0.0f, speed);  // 力を設定
        rb.AddForce(force, ForceMode.Impulse);           // 力を加える
    }

    void Update()
    {
        progressCount += Time.deltaTime;

        //一定時間で消滅.
        if(progressCount >= disappearTime)
        {
            Destroy(this.gameObject);
        }
    }

    //樹にぶつかったら消滅.
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "tree")
        {
            Destroy(this.gameObject);
        }
    }
}
