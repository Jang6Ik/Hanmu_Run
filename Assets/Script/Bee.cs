using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    private Rigidbody2D MyRigidBody2D; // rigidbody�� �޾ƿ� ����
    private Player player;

    void Start()
    {
        // �����Ǹ� ������ �����Ѵ�.
        MyRigidBody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>(); 
    }

    void Update()
    {
        // ��ֹ��� �����̰� �Ѵ�.
        MyRigidBody2D.velocity = new Vector2(-10, MyRigidBody2D.velocity.y); 
    }

    private void OnTriggerEnter2D(Collider2D col) // �浹 Ȯ��
    {
        if (col.gameObject.name == "Player") // �浹�� ������Ʈ�� �̸��� Player�� ����
        {
            player.deathSound.Play(); // ȿ���� ����
            player.MoveSpeed = player.MoveSpeedStore; // �÷��̾��� �̵��ӵ��� �ʱ�ȭ�Ѵ�.
            gameObject.SetActive(false); // ��Ȱ��ȭ
        }
    }
}
