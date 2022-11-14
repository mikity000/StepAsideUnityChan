using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    private Animator animator;
    //Unityちゃんを移動させるコンポーネントを入れる
    private Rigidbody rb;
    //前方向の速度
    private float velocityZ = 16f;
    //横方向の速度
    private float velocityX = 10f;
    //上方向の速度
    private float velocityY = 10f;
    //左右の移動できる範囲
    private float movableRange = 3.4f;
    //動きを減速させる係数
    private float coefficient = 0.99f;
    //ゲーム終了の判定
    private bool isEnd = false;
    //ゲーム終了時に表示するテキスト
    private GameObject stateText;
    //スコアを表示するテキスト
    private GameObject scoreText;
    //得点
    private int score = 0;
    //左ボタン押下の判定
    private bool isLButtonDown = false;
    //右ボタン押下の判定
    private bool isRButtonDown = false;
    //ジャンプボタン押下の判定
    private bool isJButtonDown = false;

    void Start()
    {
        //アニメータコンポーネントを取得
        animator = GetComponent<Animator>();

        //走るアニメーションを開始
        animator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();

        //シーン中のstateTextオブジェクトを取得
        stateText = GameObject.Find("GameResultText");

        //シーン中のscoreTextオブジェクトを取得
        scoreText = GameObject.Find("ScoreText");
    }

    void Update()
    {
        //ゲーム終了ならUnityちゃんの動きを減衰する
        if (isEnd)
        {
            velocityZ *= coefficient;
            velocityX *= coefficient;
            velocityY *= coefficient;
            animator.speed *= coefficient;
        }

        //横方向の入力による速度
        float inputVelocityX = 0;
        //上方向の入力による速度
        float inputVelocityY = 0;

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる
        if ((Input.GetKey(KeyCode.LeftArrow) || isLButtonDown) && -movableRange < transform.position.x)
        {
            //左方向への速度を代入
            inputVelocityX = -velocityX;
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || isRButtonDown) && transform.position.x < movableRange)
        {
            //右方向への速度を代入
            inputVelocityX = velocityX;
        }

        //ジャンプしていない時にスペースまたはボタンが押されたらジャンプする
        if ((Input.GetKeyDown(KeyCode.Space) || isJButtonDown) && transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生
            animator.SetBool("Jump", true);
            //上方向への速度を代入
            inputVelocityY = velocityY;
        }
        else
        {
            //現在のY軸の速度を代入
            inputVelocityY = rb.velocity.y;
        }

        //Jumpステートの場合はJumpにfalseをセットする
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            animator.SetBool("Jump", false);
        }

        //Unityちゃんに速度を与える
        rb.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);
    }

    //トリガーモードで他のオブジェクトと接触した場合の処理
    void OnTriggerEnter(Collider other)
    {

        //障害物に衝突した場合
        if (other.gameObject.CompareTag("Car") || other.gameObject.CompareTag("TrafficCone"))
        {
            isEnd = true;

            //stateTextにGAME OVERを表示
            stateText.GetComponent<Text>().text = "GAME OVER";
        }

        //ゴール地点に到達した場合
        if (other.gameObject.CompareTag("Goal"))
        {
            isEnd = true;

            //stateTextにGAME CLEARを表示
            stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        //コインに衝突した場合
        if (other.gameObject.CompareTag("Coin"))
        {
            // スコアを加算(追加)
            score += 10;

            //ScoreTextに獲得した点数を表示(追加)
            scoreText.GetComponent<Text>().text = "Score " + score + "pt";

            //パーティクルを再生
            GetComponent<ParticleSystem>().Play();

            //接触したコインのオブジェクトを破棄
            Destroy(other.gameObject);
        }
    }

    //ジャンプボタンを押した場合の処理
    public void GetMyJumpButtonDown()
    {
        isJButtonDown = true;
    }

    //ジャンプボタンを離した場合の処理
    public void GetMyJumpButtonUp()
    {
        isJButtonDown = false;
    }

    //左ボタンを押し続けた場合の処理
    public void GetMyLeftButtonDown()
    {
        isLButtonDown = true;
    }
    //左ボタンを離した場合の処理
    public void GetMyLeftButtonUp()
    {
        isLButtonDown = false;
    }

    //右ボタンを押し続けた場合の処理
    public void GetMyRightButtonDown()
    {
        isRButtonDown = true;
    }
    //右ボタンを離した場合の処理
    public void GetMyRightButtonUp()
    {
        isRButtonDown = false;
    }
}
