using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDestroy : MonoBehaviour
{
    public GameObject groundDestroyer;

    void Start()
    {
        // GroundDestroy 오브젝트를 찾는다.
        groundDestroyer = GameObject.Find("GroundDestroy");
    }

    void Update()
    {
        // 그라운드의 위치가 그라운드파괴 오브젝트보다 뒤면
        if (transform.position.x < groundDestroyer.transform.position.x)
        {
            gameObject.SetActive(false); // 그라운드를 비활성화 한다.
        }
    }
}
