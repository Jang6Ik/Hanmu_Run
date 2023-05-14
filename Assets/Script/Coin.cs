using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinScore;
    private GameDIrector director;

    void Start()
    {
        director = FindObjectOfType<GameDIrector>(); // 생성되면서 감독을 찾는다.
    }

    private void OnTriggerEnter2D(Collider2D col) // trigger 충돌 판정
    {
        if (col.gameObject.name == "Player") // 충돌한 오브젝트의 이름이 Player면 실행
        {
            director.coinSound.Play(); // 효과음 실행
            director.AddScore(coinScore); // 감독 스크립트의 AddScore를 실행
            gameObject.SetActive(false); // 비활성화
        }
    }
}
