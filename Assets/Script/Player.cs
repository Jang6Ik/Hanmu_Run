using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ���� ����
    public float MoveSpeed; // �̵��ӵ�
    public float MoveSpeedStore; // �̵��ӵ� �ʱⰪ
    public float speedMultiplier; // �̵��ӵ� ����ġ
    public float speedCheck; // �̵��ӵ� Ȯ��
    private float speedCheckStore; // �̵��ӵ� Ȯ�� �ʱⰪ
    private float speedCheckCount;  
    private float speedCheckCountStore;
    public float JumpForce; // ������
    public float JumpTime; // ���� ���ӽð� �ʱⰪ
    public float JumpCounter; // ���� ���ӽð�
    private bool JumpStop; // ���� ������ �ƴ��� Ȯ��
    private bool JumpDouble; // �������� ���� ���� Ȯ��
    private Rigidbody2D MyRigidBody2D; // rigidbody�� �޾ƿ� ����
    public bool IsGround; // �÷��̾ ���� �ִ��� �ƴ����� Ȯ����
    public LayerMask Ground; // layermask�� ����� Ư�� layer�� ������Ʈ�� ã�´�.
    public Transform groundCheck; // �׶���üũ ������Ʈ�� ��ġ�� ������
    public float groundCheckRadius; // �׶���üũ ������Ʈ�� ũ��
    private Animator MyAnimator; // �ִϸ��̼�
    public GameDIrector director; // ���� ������Ʈ ã��
    public AudioSource jumpSound; // ���� ����
    public AudioSource deathSound; // ���� ����

    public GameObject Life0; // ���� 0
    public GameObject Life1; // ���� 1
    public GameObject Life2; // ���� 2

    void Start()
    {
        // ��ũ��Ʈ ���۰� �Բ� �������� �ʱⰪ ����
        MyRigidBody2D = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
        JumpCounter = JumpTime;
        JumpStop = true;
        speedCheckCount = speedCheck;
        MoveSpeedStore = MoveSpeed;
        speedCheckStore = speedCheck;
        speedCheckCountStore = speedCheckCount;
    }

    void Update()
    {
        // ������ ���� �÷��̾�� �ϳ� ���� �׶����� ���縦 Ȯ���Ͽ� �÷��̾ �׶��� ���� �ִ��� Ȯ���Ѵ�.
        IsGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, Ground);

        if (transform.position.x > speedCheckCount) // �ӵ������� ������ �д�.
        {
            speedCheckCount += speedCheck;
            speedCheck = speedCheck * speedMultiplier;
            MoveSpeed = MoveSpeed * speedMultiplier;
        }
        
        
        // ������ ���۵Ǹ� �̵��ӵ� ��ŭ X������, Y���� �÷��̾��� ���� ��ġ���� ���Ծ��� �����δ�.
        MyRigidBody2D.velocity = new Vector2(MoveSpeed,MyRigidBody2D.velocity.y);

        if (Input.GetMouseButtonDown(0)) // Ŭ���ϸ� ����
        {
            if (IsGround) // �׶��� ���� ����
            {
                MyRigidBody2D.velocity = new Vector2(MyRigidBody2D.velocity.x, JumpForce); // �����¸�ŭ �����Ѵ�.
                JumpStop = false; // ������
                jumpSound.Play(); // ȿ���� ����
            }

            if (!IsGround && JumpDouble) // �׶��� ���� �ƴϰ� ���������� �����ϸ� ����
            {
                MyRigidBody2D.velocity = new Vector2(MyRigidBody2D.velocity.x, JumpForce); // �����¸�ŭ �����Ѵ�.
                JumpCounter = JumpTime;
                JumpStop = false; // ������
                JumpDouble = false; // �������� �Ұ���
                jumpSound.Play(); // ȿ���� ����
            }
        }
        if (Input.GetMouseButton(0) && !JumpStop) // �� ������ ����
        {
            if (JumpCounter > 0)
            {
                MyRigidBody2D.velocity = new Vector2(MyRigidBody2D.velocity.x, JumpForce); // �����¸�ŭ �����Ѵ�.
                JumpCounter -= Time.deltaTime; // ���� �ð��� ���δ�.
            }
        }
        if (Input.GetMouseButtonUp(0)) // ������ �� �� ����
        {
            JumpCounter = 0;
            JumpStop = true; // �������� �ƴ�
        }
        if (IsGround)
        {
            JumpCounter = JumpTime;
            JumpDouble = true; // �������� ����
        }


        MyAnimator.SetFloat("Speed", MyRigidBody2D.velocity.x); // �ִϸ��̼� ����
        MyAnimator.SetBool("IsGround", IsGround); // �ִϸ��̼� ����
    }
    private void OnCollisionEnter2D(Collision2D col) // �浹 Ȯ��
    {
        if (col.gameObject.tag == "DeathZone") // �浹�� ������Ʈ�� �±װ� DeathZone�̸� ����
        {
            director.Restart(); // ������ Restart ����
            MoveSpeed = MoveSpeedStore; // �̵��ӵ� �ʱ�ȭ
            speedCheck = speedCheckStore;
            speedCheckCount = speedCheckCountStore;
            deathSound.Play();
        }

        if (col.gameObject.name.Contains("spikes")) // �浹�� ������Ʈ�� �̸��� spikes�� ���ԵǾ� ������ ����
        {
            if (Life2.activeSelf) // ����2�� Ȱ��ȭ �Ǿ� �ִٸ�
            {
                Life2.SetActive(false); // ����2�� ��Ȱ��ȭ �ϰ�
                deathSound.Play(); // ȿ���� ����
            }
            else if (Life1.activeSelf)
            {
                Life1.SetActive(false); // ����1�� ��Ȱ��ȭ
                deathSound.Play(); // ȿ���� ����
            }
            else
            {
                Life0.SetActive(false); // ����0�� ��Ȱ��ȭ
                deathSound.Play(); // ȿ���� ����
                director.Restart(); // ������ Restart ����
                MoveSpeed = MoveSpeedStore; // �̵��ӵ� �ʱ�ȭ
                speedCheck = speedCheckStore;
                speedCheckCount = speedCheckCountStore;
            }
        }

    }
}
