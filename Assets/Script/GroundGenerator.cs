using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    // �׶����� ������ ���� ����
    public GameObject ground; // ������ �׶���
    public Transform spawn; // �׶��尡 ������ ��ġ
    public float distance; // ������ �׶��尣�� �Ÿ�
    public float distanceMax; // �׶��尣 �Ÿ��� �ִ밪
    public float distanceMin; // �׶��尣 �Ÿ��� �ּҰ�
    private int groundSelect; // ������ �׶����� ��ȣ ����
    private float[] groundsW; // �׶����� ������
    public Transform spawnH; // �׶��尡 ������ ��ġ
    private float heightMax; // �׶����� �ִ� ����
    private float heightMin; // �׶����� �ּ� ����
    public float heightChangeMax; // �׶��尣 ���������� �ִ밪
    private float heightChange; // �׶����� ���� ����

    public Pooling[] pools; // �׶��� ����

    //���׷����� ���� ����
    public CoinGenerator coinGenerator; // ���� ���׷�����
    public float randomCoin; // ���� ���� ��
    public float randomSpike; // ���� ���� ��
    public Pooling spikePool; // ���� ����
    public BeeGenerator beeGenerator; // �� ���׷�����
    public float randomBee; // �� ���� ��
    public float itemH; // �������� �ִ� ����
    public Pooling itemPool; // ������ ����
    public float randomItem; // ������ ���� ��

    void Start()
    {
        groundsW = new float[pools.Length]; // �׶��� ���� ũ�⸸ŭ �迭 ������ ����
        for (int i = 0; i < pools.Length; i++)
        {
            // �׶��� ������ = �׶��� ���� i��° �׶����� BoxCollider ������
            groundsW[i] = pools[i].poolObject.GetComponent<BoxCollider2D>().size.x; 
        }

        heightMin = transform.position.y; // �׶��� �ּ� ���̸� �׶����� Y������ ����
        heightMax = spawnH.position.y; // �׶��� �ִ� ���̸� spawnH�� Y������ ����
    }

    void Update()
    {
        if (transform.position.x < spawn.position.x) // ���׷������� x��ǥ�� �������� �ڸ�
        {
            // ������ �׶��� ������ �Ÿ��� ���Ѵ�.
            distance = Random.Range(distanceMin, distanceMax);

            // ������ �׶����� ����� ���Ѵ�. 1~4���� ����
            groundSelect = Random.Range(0, pools.Length);

            // �׶��尡 ������ ���̸� ���Ѵ�. �ִ� y + heightChangeMax, �ּ�  y + -heightChangeMax
            heightChange = transform.position.y + Random.Range(heightChangeMax, -heightChangeMax);

            // heightChange�� ���� �ִ� �ּ� ���� �Ѱ��� ���� ����ó��
            if (heightChange > heightMax)
            {
                heightChange = heightMax;
            }
            else if (heightChange < heightMin)
            {
                heightChange = heightMin;
            }

            if (Random.Range(0f, 100f) < randomItem) // 0���� 100���� ������ randomItem ���� ������ ������ ����
            {
                GameObject newItem = itemPool.getPoolObject();
                newItem.transform.position = transform.position + new Vector3(distance / 2f, Random.Range(itemH/2, itemH), 0f);
                newItem.SetActive(true);
            }


            // ���׷������� ��ġ�� �ڷ� �Ű��ش�.
            transform.position = new Vector3(transform.position.x + (groundsW[groundSelect] / 2) + distance, heightChange, transform.position.z);

            // �Ÿ���ŭ ����� �׶��带 �����Ѵ�.
            GameObject newGround = pools[groundSelect].getPoolObject(); // �Լ��� ���� ���ο� �׶��� ����
            newGround.transform.position = transform.position; // ������ �׶����� position ����
            newGround.transform.rotation = transform.rotation; // ������ �׶����� rotation ����
            newGround.SetActive(true); // Ȱ��ȭ ��Ų��.

            if (Random.Range(0f, 100f) < randomCoin) // 0���� 100���� ������ randomCoin ���� ������ ���� ����
            {
                coinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));
            }

            if (Random.Range(0f, 100f) < randomBee) // 0���� 100���� ������ randomBee ���� ������ �� ����
            {
                beeGenerator.SpawnBees(new Vector3(transform.position.x, Random.Range(transform.position.y + 2f, transform.position.y + 5f), transform.position.z));
            }

            if (Random.Range(0f, 100f) < randomSpike) // 0���� 100���� ������ randomSpike ���� ������ ���� ����
            {
                // ���� ����
                GameObject newSpike = spikePool.getPoolObject();
                float spikeX = Random.Range(-groundsW[groundSelect] / 2 + 1.5f, groundsW[groundSelect] / 2 - 1.5f);
                Vector3 spikePosition = new Vector3(spikeX, 1.2f, 0f);
                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            }

            // ���׷������� ��ġ�� �ڷ� �Ű��ش�.
            transform.position = new Vector3(transform.position.x + (groundsW[groundSelect] / 2), transform.position.y, transform.position.z);

        }
    }
}
