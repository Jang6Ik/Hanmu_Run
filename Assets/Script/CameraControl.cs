using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Player player; 
    private Vector3 lastPosition;
    private float distance;

    void Start()
    {
        player = FindObjectOfType<Player>(); // �÷��̾ ã�´�.
        lastPosition = player.transform.position; // �÷��̾��� ������ ��ġ�� �����Ѵ�.
    }

    void Update()
    {
        // �÷��̾��� ���� ��ġ�� ������ ��ġ�� ���̸� ���Ѵ�.
        distance = player.transform.position.x - lastPosition.x;

        // ī�޶��� x��ǥ�� distance��ŭ �̵���Ų��.
        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        lastPosition = player.transform.position;
    }
}
