using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    void Start()
    {
        //��]���J�n����p�x��ݒ�
        transform.Rotate(0, Random.Range(0, 360), 0);
    }

    void Update()
    {
        //��]
        transform.Rotate(0, 3, 0);
    }
}
