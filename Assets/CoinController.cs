using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    void Start()
    {
        //回転を開始する角度を設定
        transform.Rotate(0, Random.Range(0, 360), 0);
    }

    void Update()
    {
        //回転
        transform.Rotate(0, 3, 0);
    }
}
