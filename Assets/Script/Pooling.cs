using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 프리팹을 사용할 때 메모리 관리를 위해 사용하는 오브젝트 풀링 방법을 사용한다.
public class Pooling : MonoBehaviour
{
    public GameObject poolObject;
    public int poolAmount;
    public List<GameObject> poolObjects;

    void Start()
    {
        poolObjects = new List<GameObject>();

        for (int i = 0; i < poolAmount; i++)
        {
            // 미리 만들어둘 오브젝트(그라운드)를 지정한다.
            GameObject gameObj = Instantiate(poolObject);
            gameObj.SetActive(false); // 그 오브젝트를 비활성화 한다.
            poolObjects.Add(gameObj); // 그 오브젝트를 리스트에 추가한다.
        }
    }

    public GameObject getPoolObject()
    {
        for (int i = 0; i < poolObjects.Count; i++) // i가 리스트 크기보다 작으면 반복
        {
            if (!poolObjects[i].activeInHierarchy) // 리스트의 오브젝트가 비활성화 되어있다면
            {
                return poolObjects[i]; // 리스트의 첫번째 오브젝트를 반환한다.
            }
        }
        GameObject gameObj = Instantiate(poolObject);
        gameObj.SetActive(false);
        poolObjects.Add(gameObj);
        return gameObj; // 오브젝트를 반환한다.
    }

}
