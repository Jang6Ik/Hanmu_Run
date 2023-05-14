using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� ����� �� �޸� ������ ���� ����ϴ� ������Ʈ Ǯ�� ����� ����Ѵ�.
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
            // �̸� ������ ������Ʈ(�׶���)�� �����Ѵ�.
            GameObject gameObj = Instantiate(poolObject);
            gameObj.SetActive(false); // �� ������Ʈ�� ��Ȱ��ȭ �Ѵ�.
            poolObjects.Add(gameObj); // �� ������Ʈ�� ����Ʈ�� �߰��Ѵ�.
        }
    }

    public GameObject getPoolObject()
    {
        for (int i = 0; i < poolObjects.Count; i++) // i�� ����Ʈ ũ�⺸�� ������ �ݺ�
        {
            if (!poolObjects[i].activeInHierarchy) // ����Ʈ�� ������Ʈ�� ��Ȱ��ȭ �Ǿ��ִٸ�
            {
                return poolObjects[i]; // ����Ʈ�� ù��° ������Ʈ�� ��ȯ�Ѵ�.
            }
        }
        GameObject gameObj = Instantiate(poolObject);
        gameObj.SetActive(false);
        poolObjects.Add(gameObj);
        return gameObj; // ������Ʈ�� ��ȯ�Ѵ�.
    }

}
