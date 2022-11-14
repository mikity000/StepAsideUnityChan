using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    void Start()
    {
        //‰ñ“]‚ğŠJn‚·‚éŠp“x‚ğİ’è
        transform.Rotate(0, Random.Range(0, 360), 0);
    }

    void Update()
    {
        //‰ñ“]
        transform.Rotate(0, 3, 0);
    }
}
