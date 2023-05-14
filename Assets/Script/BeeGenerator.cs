using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeGenerator : MonoBehaviour
{
    public Pooling beePool; // 国 葛澜
    public void SpawnBees(Vector3 spawnPosition)
    {
        GameObject Bee = beePool.getPoolObject(); // 国 积己
        Bee.transform.position = spawnPosition; // 积己瞪 国狼 困摹
        Bee.SetActive(true);
    }

}
