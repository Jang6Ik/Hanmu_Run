using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public Pooling coinPool; // 코인 모음
    public float distance; // 생성된 코인간의 거리
    public void SpawnCoins(Vector3 spawnPosition)
    {
        GameObject coin1 = coinPool.getPoolObject(); // 코인 생성
        coin1.transform.position = spawnPosition; // 생성될 코인의 위치
        coin1.SetActive(true);

        GameObject coin2 = coinPool.getPoolObject(); // 생성될 코인의 위치는 1번 코인에서 distance만큼 뒤에 있다.
        coin2.transform.position = new Vector3(spawnPosition.x - distance, spawnPosition.y, spawnPosition.z);
        coin2.SetActive(true);

        GameObject coin3 = coinPool.getPoolObject(); // 생성될 코인의 위치는 1번 코인에서 distance만큼 앞에 있다.
        coin3.transform.position = new Vector3(spawnPosition.x + distance, spawnPosition.y, spawnPosition.z);
        coin3.SetActive(true);
    }

}
