using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    private Animator animator;
    //Unity�������ړ�������R���|�[�l���g������
    private Rigidbody rb;
    //�O�����̑��x
    private float velocityZ = 16f;
    //�������̑��x
    private float velocityX = 10f;
    //������̑��x
    private float velocityY = 10f;
    //���E�̈ړ��ł���͈�
    private float movableRange = 3.4f;
    //����������������W��
    private float coefficient = 0.99f;
    //�Q�[���I���̔���
    private bool isEnd = false;
    //�Q�[���I�����ɕ\������e�L�X�g
    private GameObject stateText;
    //�X�R�A��\������e�L�X�g
    private GameObject scoreText;
    //���_
    private int score = 0;
    //���{�^�������̔���
    private bool isLButtonDown = false;
    //�E�{�^�������̔���
    private bool isRButtonDown = false;
    //�W�����v�{�^�������̔���
    private bool isJButtonDown = false;

    void Start()
    {
        //�A�j���[�^�R���|�[�l���g���擾
        animator = GetComponent<Animator>();

        //����A�j���[�V�������J�n
        animator.SetFloat("Speed", 1);

        //Rigidbody�R���|�[�l���g���擾
        rb = GetComponent<Rigidbody>();

        //�V�[������stateText�I�u�W�F�N�g���擾
        stateText = GameObject.Find("GameResultText");

        //�V�[������scoreText�I�u�W�F�N�g���擾
        scoreText = GameObject.Find("ScoreText");
    }

    void Update()
    {
        //�Q�[���I���Ȃ�Unity�����̓�������������
        if (isEnd)
        {
            velocityZ *= coefficient;
            velocityX *= coefficient;
            velocityY *= coefficient;
            animator.speed *= coefficient;
        }

        //�������̓��͂ɂ�鑬�x
        float inputVelocityX = 0;
        //������̓��͂ɂ�鑬�x
        float inputVelocityY = 0;

        //Unity��������L�[�܂��̓{�^���ɉ����č��E�Ɉړ�������
        if ((Input.GetKey(KeyCode.LeftArrow) || isLButtonDown) && -movableRange < transform.position.x)
        {
            //�������ւ̑��x����
            inputVelocityX = -velocityX;
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || isRButtonDown) && transform.position.x < movableRange)
        {
            //�E�����ւ̑��x����
            inputVelocityX = velocityX;
        }

        //�W�����v���Ă��Ȃ����ɃX�y�[�X�܂��̓{�^���������ꂽ��W�����v����
        if ((Input.GetKeyDown(KeyCode.Space) || isJButtonDown) && transform.position.y < 0.5f)
        {
            //�W�����v�A�j�����Đ�
            animator.SetBool("Jump", true);
            //������ւ̑��x����
            inputVelocityY = velocityY;
        }
        else
        {
            //���݂�Y���̑��x����
            inputVelocityY = rb.velocity.y;
        }

        //Jump�X�e�[�g�̏ꍇ��Jump��false���Z�b�g����
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            animator.SetBool("Jump", false);
        }

        //Unity�����ɑ��x��^����
        rb.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);
    }

    //�g���K�[���[�h�ő��̃I�u�W�F�N�g�ƐڐG�����ꍇ�̏���
    void OnTriggerEnter(Collider other)
    {

        //��Q���ɏՓ˂����ꍇ
        if (other.gameObject.CompareTag("Car") || other.gameObject.CompareTag("TrafficCone"))
        {
            isEnd = true;

            //stateText��GAME OVER��\��
            stateText.GetComponent<Text>().text = "GAME OVER";
        }

        //�S�[���n�_�ɓ��B�����ꍇ
        if (other.gameObject.CompareTag("Goal"))
        {
            isEnd = true;

            //stateText��GAME CLEAR��\��
            stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        //�R�C���ɏՓ˂����ꍇ
        if (other.gameObject.CompareTag("Coin"))
        {
            // �X�R�A�����Z(�ǉ�)
            score += 10;

            //ScoreText�Ɋl�������_����\��(�ǉ�)
            scoreText.GetComponent<Text>().text = "Score " + score + "pt";

            //�p�[�e�B�N�����Đ�
            GetComponent<ParticleSystem>().Play();

            //�ڐG�����R�C���̃I�u�W�F�N�g��j��
            Destroy(other.gameObject);
        }
    }

    //�W�����v�{�^�����������ꍇ�̏���
    public void GetMyJumpButtonDown()
    {
        isJButtonDown = true;
    }

    //�W�����v�{�^���𗣂����ꍇ�̏���
    public void GetMyJumpButtonUp()
    {
        isJButtonDown = false;
    }

    //���{�^���������������ꍇ�̏���
    public void GetMyLeftButtonDown()
    {
        isLButtonDown = true;
    }
    //���{�^���𗣂����ꍇ�̏���
    public void GetMyLeftButtonUp()
    {
        isLButtonDown = false;
    }

    //�E�{�^���������������ꍇ�̏���
    public void GetMyRightButtonDown()
    {
        isRButtonDown = true;
    }
    //�E�{�^���𗣂����ꍇ�̏���
    public void GetMyRightButtonUp()
    {
        isRButtonDown = false;
    }
}
