using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    private Rigidbody2D MyRigidBody2D; // rigidbody를 받아올 변수
    private Player player;

    void Start()
    {
        // 생성되며 변수를 설정한다.
        MyRigidBody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>(); 
    }

    void Update()
    {
        // 장애물을 움직이게 한다.
        MyRigidBody2D.velocity = new Vector2(-10, MyRigidBody2D.velocity.y); 
    }

    private void OnTriggerEnter2D(Collider2D col) // 충돌 확인
    {
        if (col.gameObject.name == "Player") // 충돌한 오브젝트의 이름이 Player면 실행
        {
            player.deathSound.Play(); // 효과음 실행
            player.MoveSpeed = player.MoveSpeedStore; // 플레이어의 이동속도를 초기화한다.
            gameObject.SetActive(false); // 비활성화
        }
    }
}
