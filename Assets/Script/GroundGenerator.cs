using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    // 그라운드의 생성을 위한 변수
    public GameObject ground; // 생성될 그라운드
    public Transform spawn; // 그라운드가 생성될 위치
    public float distance; // 생성될 그라운드간의 거리
    public float distanceMax; // 그라운드간 거리의 최대값
    public float distanceMin; // 그라운드간 거리의 최소값
    private int groundSelect; // 생성될 그라운드의 번호 저장
    private float[] groundsW; // 그라운드의 사이즈
    public Transform spawnH; // 그라운드가 생성될 위치
    private float heightMax; // 그라운드의 최대 높이
    private float heightMin; // 그라운드의 최소 높이
    public float heightChangeMax; // 그라운드간 높이차이의 최대값
    private float heightChange; // 그라운드의 높이 차이

    public Pooling[] pools; // 그라운드 모음

    //제네레이터 관련 변수
    public CoinGenerator coinGenerator; // 코인 제네레이터
    public float randomCoin; // 코인 출현 빈도
    public float randomSpike; // 가시 출현 빈도
    public Pooling spikePool; // 가시 모음
    public BeeGenerator beeGenerator; // 벌 제네레이터
    public float randomBee; // 벌 출현 빈도
    public float itemH; // 아이템의 최대 높이
    public Pooling itemPool; // 아이템 높이
    public float randomItem; // 아이템 출현 빈도

    void Start()
    {
        groundsW = new float[pools.Length]; // 그라운드 모음 크기만큼 배열 사이즈 설정
        for (int i = 0; i < pools.Length; i++)
        {
            // 그라운드 사이즈 = 그라운드 모음 i번째 그라운드의 BoxCollider 사이즈
            groundsW[i] = pools[i].poolObject.GetComponent<BoxCollider2D>().size.x; 
        }

        heightMin = transform.position.y; // 그라운드 최소 높이를 그라운드의 Y값으로 설정
        heightMax = spawnH.position.y; // 그라운드 최대 높이를 spawnH의 Y값으로 설정
    }

    void Update()
    {
        if (transform.position.x < spawn.position.x) // 제네레이터의 x좌표가 스폰보다 뒤면
        {
            // 생성될 그라운드 사이의 거리를 정한다.
            distance = Random.Range(distanceMin, distanceMax);

            // 생성될 그라운드의 모양을 정한다. 1~4까지 랜덤
            groundSelect = Random.Range(0, pools.Length);

            // 그라운드가 생성될 높이를 정한다. 최대 y + heightChangeMax, 최소  y + -heightChangeMax
            heightChange = transform.position.y + Random.Range(heightChangeMax, -heightChangeMax);

            // heightChange의 값이 최대 최소 값을 넘겼을 때의 예외처리
            if (heightChange > heightMax)
            {
                heightChange = heightMax;
            }
            else if (heightChange < heightMin)
            {
                heightChange = heightMin;
            }

            if (Random.Range(0f, 100f) < randomItem) // 0에서 100까지 숫자중 randomItem 보다 작으면 아이템 생성
            {
                GameObject newItem = itemPool.getPoolObject();
                newItem.transform.position = transform.position + new Vector3(distance / 2f, Random.Range(itemH/2, itemH), 0f);
                newItem.SetActive(true);
            }


            // 제네레이터의 위치를 뒤로 옮겨준다.
            transform.position = new Vector3(transform.position.x + (groundsW[groundSelect] / 2) + distance, heightChange, transform.position.z);

            // 거리만큼 띄워서 그라운드를 생성한다.
            GameObject newGround = pools[groundSelect].getPoolObject(); // 함수를 통해 새로운 그라운드 생성
            newGround.transform.position = transform.position; // 생성된 그라운드의 position 설정
            newGround.transform.rotation = transform.rotation; // 생성된 그라운드의 rotation 설정
            newGround.SetActive(true); // 활성화 시킨다.

            if (Random.Range(0f, 100f) < randomCoin) // 0에서 100까지 숫자중 randomCoin 보다 작으면 코인 생성
            {
                coinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));
            }

            if (Random.Range(0f, 100f) < randomBee) // 0에서 100까지 숫자중 randomBee 보다 작으면 벌 생성
            {
                beeGenerator.SpawnBees(new Vector3(transform.position.x, Random.Range(transform.position.y + 2f, transform.position.y + 5f), transform.position.z));
            }

            if (Random.Range(0f, 100f) < randomSpike) // 0에서 100까지 숫자중 randomSpike 보다 작으면 가시 생성
            {
                // 가시 생성
                GameObject newSpike = spikePool.getPoolObject();
                float spikeX = Random.Range(-groundsW[groundSelect] / 2 + 1.5f, groundsW[groundSelect] / 2 - 1.5f);
                Vector3 spikePosition = new Vector3(spikeX, 1.2f, 0f);
                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            }

            // 제네레이터의 위치를 뒤로 옮겨준다.
            transform.position = new Vector3(transform.position.x + (groundsW[groundSelect] / 2), transform.position.y, transform.position.z);

        }
    }
}
