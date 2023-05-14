using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDestroy : MonoBehaviour
{
    public GameObject groundDestroyer;

    void Start()
    {
        // GroundDestroy ������Ʈ�� ã�´�.
        groundDestroyer = GameObject.Find("GroundDestroy");
    }

    void Update()
    {
        // �׶����� ��ġ�� �׶����ı� ������Ʈ���� �ڸ�
        if (transform.position.x < groundDestroyer.transform.position.x)
        {
            gameObject.SetActive(false); // �׶��带 ��Ȱ��ȭ �Ѵ�.
        }
    }
}
