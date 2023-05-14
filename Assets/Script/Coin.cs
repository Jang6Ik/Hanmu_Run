using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinScore;
    private GameDIrector director;

    void Start()
    {
        director = FindObjectOfType<GameDIrector>(); // �����Ǹ鼭 ������ ã�´�.
    }

    private void OnTriggerEnter2D(Collider2D col) // trigger �浹 ����
    {
        if (col.gameObject.name == "Player") // �浹�� ������Ʈ�� �̸��� Player�� ����
        {
            director.coinSound.Play(); // ȿ���� ����
            director.AddScore(coinScore); // ���� ��ũ��Ʈ�� AddScore�� ����
            gameObject.SetActive(false); // ��Ȱ��ȭ
        }
    }
}
