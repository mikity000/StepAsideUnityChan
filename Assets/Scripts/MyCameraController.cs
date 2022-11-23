using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{
    //Unity�����̃I�u�W�F�N�g
    private GameObject unitychan;
    //Unity�����ƃJ�����̋���
    private float difference;

    void Start()
    {
        //Unity�����̃I�u�W�F�N�g���擾
        unitychan = GameObject.Find("unitychan");
        //Unity�����ƃJ�����̈ʒu�iz���W�j�̍������߂�
        difference = unitychan.transform.position.z - transform.position.z;
    }

    void Update()
    {
        //Unity�����̈ʒu�ɍ��킹�ăJ�����̈ʒu���ړ�
        transform.position = new Vector3(0, transform.position.y, unitychan.transform.position.z - difference);
    }
}
