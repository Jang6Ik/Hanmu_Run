using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public Pooling coinPool; // ���� ����
    public float distance; // ������ ���ΰ��� �Ÿ�
    public void SpawnCoins(Vector3 spawnPosition)
    {
        GameObject coin1 = coinPool.getPoolObject(); // ���� ����
        coin1.transform.position = spawnPosition; // ������ ������ ��ġ
        coin1.SetActive(true);

        GameObject coin2 = coinPool.getPoolObject(); // ������ ������ ��ġ�� 1�� ���ο��� distance��ŭ �ڿ� �ִ�.
        coin2.transform.position = new Vector3(spawnPosition.x - distance, spawnPosition.y, spawnPosition.z);
        coin2.SetActive(true);

        GameObject coin3 = coinPool.getPoolObject(); // ������ ������ ��ġ�� 1�� ���ο��� distance��ŭ �տ� �ִ�.
        coin3.transform.position = new Vector3(spawnPosition.x + distance, spawnPosition.y, spawnPosition.z);
        coin3.SetActive(true);
    }

}
