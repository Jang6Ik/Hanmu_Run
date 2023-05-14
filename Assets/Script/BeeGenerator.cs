using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeGenerator : MonoBehaviour
{
    public Pooling beePool; // �� ����
    public void SpawnBees(Vector3 spawnPosition)
    {
        GameObject Bee = beePool.getPoolObject(); // �� ����
        Bee.transform.position = spawnPosition; // ������ ���� ��ġ
        Bee.SetActive(true);
    }

}
