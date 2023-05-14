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
        player = FindObjectOfType<Player>(); // 플레이어를 찾는다.
        lastPosition = player.transform.position; // 플레이어의 마지막 위치를 저장한다.
    }

    void Update()
    {
        // 플레이어의 현재 위치와 마지막 위치의 차이를 구한다.
        distance = player.transform.position.x - lastPosition.x;

        // 카메라의 x좌표를 distance만큼 이동시킨다.
        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        lastPosition = player.transform.position;
    }
}
