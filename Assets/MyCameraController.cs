using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{
    //Unityちゃんのオブジェクト
    private GameObject unitychan;
    //Unityちゃんとカメラの距離
    private float difference;

    void Start()
    {
        //Unityちゃんのオブジェクトを取得
        unitychan = GameObject.Find("unitychan");
        //Unityちゃんとカメラの位置（z座標）の差を求める
        difference = unitychan.transform.position.z - transform.position.z;
    }

    void Update()
    {
        //Unityちゃんの位置に合わせてカメラの位置を移動
        transform.position = new Vector3(0, transform.position.y, unitychan.transform.position.z - difference);
    }
}
