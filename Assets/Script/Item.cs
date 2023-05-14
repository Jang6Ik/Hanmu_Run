using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool doubleScore; // ������ �� ���
    public bool safeMode; // ���� ���
    public float itemLength; // ������ ���ӽð�
    public float plusScore = 0; // �߰� ����
    private GameDIrector dIrector;
    public Sprite[] itemSprites;

    void Start()
    {
        dIrector = FindObjectOfType<GameDIrector>(); // ���� ã��
    }
    void Awake()
    {
        int itemSelect = Random.Range(0, 5); // �����ϰ� �������� ���õ�

        switch (itemSelect) // ���õ� �������� ��쿡 ���� �ٸ� ���� ����
        {
            case 0: 
                doubleScore = true; // ���� �ι� Ȱ��ȭ
                break;
            case 1:
                safeMode = true; // ���� ��� Ȱ��ȭ
                break;
            case 2:
                plusScore = 250f; // �߰� ���� 
                break;
            case 3:
                plusScore = 500f; // �߰� ����
                break;
            case 4:
                doubleScore = true; // ���� �ι� Ȱ��ȭ
                safeMode = true; // ���� ��� Ȱ��ȭ
                plusScore = 1000f; // �߰� ����
                break;
        }
        // ���õ� ���������� ��������Ʈ�� ����
        GetComponent<SpriteRenderer>().sprite = itemSprites[itemSelect];
    }

    private void OnTriggerEnter2D(Collider2D col) // Ʈ���� �浹 Ȯ��
    {
        if (col.name == "Player") // �浹�� ������Ʈ�� �̸��� Player�̸� ����
        {
            dIrector.coinSound.Play(); // ȿ���� ����
            dIrector.ActiveItem(doubleScore, safeMode, itemLength, plusScore); // ������ �Լ� ����
        }
        gameObject.SetActive(false); // �ڽ��� ��Ȱ��ȭ
    }
}
