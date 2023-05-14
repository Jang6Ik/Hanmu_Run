using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 변수 선언
    public float MoveSpeed; // 이동속도
    public float MoveSpeedStore; // 이동속도 초기값
    public float speedMultiplier; // 이동속도 가중치
    public float speedCheck; // 이동속도 확인
    private float speedCheckStore; // 이동속도 확인 초기값
    private float speedCheckCount;  
    private float speedCheckCountStore;
    public float JumpForce; // 점프력
    public float JumpTime; // 점프 지속시간 초기값
    public float JumpCounter; // 점프 지속시간
    private bool JumpStop; // 점프 중인지 아닌지 확인
    private bool JumpDouble; // 더블점프 가능 여부 확인
    private Rigidbody2D MyRigidBody2D; // rigidbody를 받아올 변수
    public bool IsGround; // 플레이어가 땅에 있는지 아닌지를 확인함
    public LayerMask Ground; // layermask를 사용해 특정 layer의 오브젝트를 찾는다.
    public Transform groundCheck; // 그라운드체크 오브젝트의 위치를 가져옴
    public float groundCheckRadius; // 그라운드체크 오브젝트의 크기
    private Animator MyAnimator; // 애니메이션
    public GameDIrector director; // 감독 오브젝트 찾기
    public AudioSource jumpSound; // 점프 사운드
    public AudioSource deathSound; // 데스 사운드

    public GameObject Life0; // 생명 0
    public GameObject Life1; // 생명 1
    public GameObject Life2; // 생명 2

    void Start()
    {
        // 스크립트 시작과 함께 변수들의 초기값 설정
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
        // 가상의 원을 플레이어에게 하나 만들어서 그라운드의 존재를 확인하여 플레이어가 그라운드 위에 있는지 확인한다.
        IsGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, Ground);

        if (transform.position.x > speedCheckCount) // 속도증가에 제한을 둔다.
        {
            speedCheckCount += speedCheck;
            speedCheck = speedCheck * speedMultiplier;
            MoveSpeed = MoveSpeed * speedMultiplier;
        }
        
        
        // 게임이 시작되면 이동속도 만큼 X축으로, Y축은 플레이어의 현재 위치에서 변함없이 움직인다.
        MyRigidBody2D.velocity = new Vector2(MoveSpeed,MyRigidBody2D.velocity.y);

        if (Input.GetMouseButtonDown(0)) // 클릭하면 실행
        {
            if (IsGround) // 그라운드 위면 실행
            {
                MyRigidBody2D.velocity = new Vector2(MyRigidBody2D.velocity.x, JumpForce); // 점프력만큼 점프한다.
                JumpStop = false; // 점프중
                jumpSound.Play(); // 효과음 실행
            }

            if (!IsGround && JumpDouble) // 그라운드 위가 아니고 더블점프가 가능하면 실행
            {
                MyRigidBody2D.velocity = new Vector2(MyRigidBody2D.velocity.x, JumpForce); // 점프력만큼 점프한다.
                JumpCounter = JumpTime;
                JumpStop = false; // 점프중
                JumpDouble = false; // 더블점프 불가능
                jumpSound.Play(); // 효과음 실행
            }
        }
        if (Input.GetMouseButton(0) && !JumpStop) // 꾹 누르면 실행
        {
            if (JumpCounter > 0)
            {
                MyRigidBody2D.velocity = new Vector2(MyRigidBody2D.velocity.x, JumpForce); // 점프력만큼 점프한다.
                JumpCounter -= Time.deltaTime; // 점프 시간을 줄인다.
            }
        }
        if (Input.GetMouseButtonUp(0)) // 눌렀다 땔 때 실행
        {
            JumpCounter = 0;
            JumpStop = true; // 점프중이 아님
        }
        if (IsGround)
        {
            JumpCounter = JumpTime;
            JumpDouble = true; // 더블점프 가능
        }


        MyAnimator.SetFloat("Speed", MyRigidBody2D.velocity.x); // 애니메이션 실행
        MyAnimator.SetBool("IsGround", IsGround); // 애니메이션 실행
    }
    private void OnCollisionEnter2D(Collision2D col) // 충돌 확인
    {
        if (col.gameObject.tag == "DeathZone") // 충돌한 오브젝트의 태그가 DeathZone이면 실행
        {
            director.Restart(); // 감독의 Restart 실행
            MoveSpeed = MoveSpeedStore; // 이동속도 초기화
            speedCheck = speedCheckStore;
            speedCheckCount = speedCheckCountStore;
            deathSound.Play();
        }

        if (col.gameObject.name.Contains("spikes")) // 충돌한 오브젝트의 이름에 spikes가 포함되어 있으면 실행
        {
            if (Life2.activeSelf) // 생명2가 활성화 되어 있다면
            {
                Life2.SetActive(false); // 생명2를 비활성화 하고
                deathSound.Play(); // 효과음 실행
            }
            else if (Life1.activeSelf)
            {
                Life1.SetActive(false); // 생명1을 비활성화
                deathSound.Play(); // 효과음 실행
            }
            else
            {
                Life0.SetActive(false); // 생명0을 비활성화
                deathSound.Play(); // 효과음 실행
                director.Restart(); // 감독의 Restart 실행
                MoveSpeed = MoveSpeedStore; // 이동속도 초기화
                speedCheck = speedCheckStore;
                speedCheckCount = speedCheckCountStore;
            }
        }

    }
}
