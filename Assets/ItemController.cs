using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private Transform mainCamera;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        //�I�u�W�F�N�g���J���������ɂȂ�����j������
        if (mainCamera.position.z - transform.position.z > 0)
            Destroy(gameObject);
    }
}
