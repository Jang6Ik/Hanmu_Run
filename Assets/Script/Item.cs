using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool doubleScore; // 점수를 두 배로
    public bool safeMode; // 안전 모드
    public float itemLength; // 아이템 지속시간
    public float plusScore = 0; // 추가 점수
    private GameDIrector dIrector;
    public Sprite[] itemSprites;

    void Start()
    {
        dIrector = FindObjectOfType<GameDIrector>(); // 감독 찾기
    }
    void Awake()
    {
        int itemSelect = Random.Range(0, 5); // 랜덤하게 아이템이 선택됨

        switch (itemSelect) // 선택된 아이템의 경우에 따라 다른 값을 가짐
        {
            case 0: 
                doubleScore = true; // 점수 두배 활성화
                break;
            case 1:
                safeMode = true; // 안전 모드 활성화
                break;
            case 2:
                plusScore = 250f; // 추가 점수 
                break;
            case 3:
                plusScore = 500f; // 추가 점수
                break;
            case 4:
                doubleScore = true; // 점수 두배 활성화
                safeMode = true; // 안전 모드 활성화
                plusScore = 1000f; // 추가 점수
                break;
        }
        // 선택된 아이템으로 스프라이트가 변함
        GetComponent<SpriteRenderer>().sprite = itemSprites[itemSelect];
    }

    private void OnTriggerEnter2D(Collider2D col) // 트리거 충돌 확인
    {
        if (col.name == "Player") // 충돌한 오브젝트의 이름이 Player이면 실행
        {
            dIrector.coinSound.Play(); // 효과음 실행
            dIrector.ActiveItem(doubleScore, safeMode, itemLength, plusScore); // 감독의 함수 실행
        }
        gameObject.SetActive(false); // 자신을 비활성화
    }
}
