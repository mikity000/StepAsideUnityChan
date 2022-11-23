using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //conePrefabを入れる
    public GameObject conePrefab;
    //ユニティちゃん
    [SerializeField] private GameObject player;
    //前回のオブジェクト生成座標
    int preSpawnPos;
    //オブジェクト生成座標
    int spawnPos = 80;
    //ゴール地点
    private int goalPos = 360;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    private void Update()
    {
        //「前回の生成座標の方が遠い場合」または「生成座標がゴールを過ぎた場合」
        if (preSpawnPos > spawnPos || spawnPos >= goalPos)
            return;

        //プレイヤー座標と生成座標の距離が40を超えた場合
        if ((player.transform.position - new Vector3(0, 0, spawnPos)).sqrMagnitude > 1600)
            return;
        
        preSpawnPos = spawnPos;
        spawnPos += 15;

        //どのアイテムを出すのかをランダムに設定
        int num = Random.Range(1, 11);
        if (num <= 2)
        {
            //コーンをx軸方向に一直線に生成
            for (float x = -1; x <= 1; x += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab);
                cone.transform.position = new Vector3(4 * x, cone.transform.position.y, spawnPos);
            }
        }
        else
        {
            //レーンごとにアイテムを生成
            for (int x = -1; x <= 1; x++)
            {
                //アイテムの種類を決める
                int item = Random.Range(1, 11);
                //アイテムを置くZ座標のオフセットをランダムに設定
                int offsetZ = Random.Range(-5, 6);
                //60%コイン配置:30%車配置:10%何もなし
                if (1 <= item && item <= 6)
                {
                    //コインを生成
                    GameObject coin = Instantiate(coinPrefab);
                    coin.transform.position = new Vector3(posRange * x, coin.transform.position.y, spawnPos + offsetZ);
                }
                else if (7 <= item && item <= 9)
                {
                    //車を生成
                    GameObject car = Instantiate(carPrefab);
                    car.transform.position = new Vector3(posRange * x, car.transform.position.y, spawnPos + offsetZ);
                }
            }
        }
    }
}
