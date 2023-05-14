using UnityEngine;
using UnityEngine.UI;

public class GameDIrector : MonoBehaviour
{
    public Transform groundGenerator; // �׶��� ���׷������� ��ġ
    public Player player; // �÷��̾�
    public Text scoreText; // ���� ������ ǥ���� UI
    public Text highscoreText; // �ְ� ������ ǥ���� UI
    public float Score; // ���� ����
    public float highScore; // �ְ� ����
    public float scoreSecond; // 1�ʴ� ������ ����
    public bool IsScore; // ������ ������ �� �ִ� �������� Ȯ��
    public bool doubleScore; // ���� �� �� Ȯ��
    public GameObject gameOver; // ���� ���� �޽���
    public bool IsPause; // ���� �����ΰ� �ƴѰ� Ȯ��
    public AudioSource coinSound; // ȿ����

    // ������ ���� ����
    private bool isDoubleScore; // ���� �� ��
    private bool safeMode; // �������
    private bool itemActive; // ������ Ȱ��ȭ ����
    private float itemLengthCounter; // ������ ���ӽð�
    private GroundGenerator theGroundGenerator; // �׶��� ���׷�����
    private float originalScoreSecond; // ���� ���� �ʱⰪ
    private float randomSpike; // ���� �� ����
    private GroundDestroy[] spikeList; // ���� ����

    // BGM ���� ����
    public Sprite mute; // UI ��������Ʈ
    public Sprite play; // UI ��������Ʈ
    public GameObject BackgroundMusic; // BGM ������Ʈ
    public AudioSource backmusic; // BGM ����� ���ҽ�
    public GameObject BGMbutton; // BGM ��ư

    private void Awake()
    {
        BackgroundMusic = GameObject.Find("S_bgm"); // S_bgm �̸��� ������Ʈ�� ã�´�.
        BGMbutton = GameObject.Find("BgmButton"); // BgmButton �̸��� ������Ʈ�� ã�´�.
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); // BackgroundMusic ����� ���ҽ� ����
        if (backmusic.isPlaying) // BGM ������̸� ���� 
        {
            BGMbutton.GetComponent<Image>().sprite = play; // ��������Ʈ�� play�� �ٲ�
        }
        else // ������� �ƴϸ�
        {
            BGMbutton.GetComponent<Image>().sprite = mute; // ��������Ʈ�� play�� �ٲ�
        }
    }

    void Start()
    {
        theGroundGenerator = FindObjectOfType<GroundGenerator>(); // �׶��� ���׷����͸� ã�´�.

        if (PlayerPrefs.HasKey("HighScore")) // �ְ� ������ ���� �Ѵٸ� ����
        {
            highScore = PlayerPrefs.GetFloat("HighScore"); // UI�� �ְ� ���� ǥ�� 
        }
    }

    void Update()
    {
        if (IsScore) // ������ ������ �� �ִٸ�
        {
            Score += scoreSecond * Time.deltaTime; // ���� ������ 1�ʸ��� ���� �߰�
        }

        if (Score > highScore) // ���������� �ְ� ���� ���� ũ�ٸ�
        {
            highScore = Score;  // �ְ� ������ ���������� �ȴ�.
            PlayerPrefs.SetFloat("HighScore", highScore); // �ְ� ������ �����Ѵ�.
        }
        scoreText.text = "Score : " + (int)Score; // ���� ���� ǥ��
        highscoreText.text = "High Score : " + (int)highScore; // �ְ� ���� ǥ��

        if (itemActive) // �������� Ȱ��ȭ �Ǿ��ٸ� ����
        {
            itemLengthCounter -= Time.deltaTime; // ������ ���ӽð��� ���ҽ�Ų��.

            if (isDoubleScore) // ���� �� �� ����
            {
                scoreSecond = originalScoreSecond * 2.75f;
                doubleScore = true;
            }

            if (safeMode) // ���� ��� ����
            {
                theGroundGenerator.randomSpike = 0f;
            }

            if (itemLengthCounter <= 0) // ���ӽð��� ������ ����
            {
                scoreSecond = originalScoreSecond; // ���� ������ �������
                doubleScore = false;
                theGroundGenerator.randomSpike = randomSpike; // ���� ���� �󵵸� �������

                itemActive = false;
            }
        }

    }

    public void Restart() // ������ ������Ѵ�.
    {
        IsScore = false; // ������ ���̻� ������ �ʰ� �Ѵ�.
        IsPause = true;
        player.gameObject.SetActive(false); // �÷��̾ ��Ȱ��ȭ�Ѵ�.

        gameOver.SetActive(true); // ���� ���� �޽����� Ȱ��ȭ�Ѵ�.
    }

    public void AddScore(int score)
    {
        if (doubleScore)
        {
            score = score * 2;
        }
        Score += score; // ���������� Score��ŭ�� ���� ���Ѵ�.
    }

    public void ActiveItem(bool score, bool safe, float time, float plus)
    {
        isDoubleScore = score; // ���� �� �� Ȱ��ȭ/��Ȱ��ȭ ����
        safeMode = safe; // ������� Ȱ��ȭ/��Ȱ��ȭ ����
        itemLengthCounter = time; // ������ ���ӽð� ����
        Score += plus; // �߰� ������ �ִٸ� ������ �����ش�.

        originalScoreSecond = scoreSecond; // ���� ���� �ʱⰪ ����
        randomSpike = theGroundGenerator.randomSpike; // ���� �� �ʱⰪ ����

        if (safeMode) // ���� ���� ����
        {
            spikeList = FindObjectsOfType<GroundDestroy>(); // GroundDestroy�� ���� ������Ʈ�� ã�´�.
            for (int i = 0; i < spikeList.Length; i++) // �̸��� spikes�� ������ ������Ʈ�� ��Ȱ��ȭ �Ѵ�.
            {
                if (spikeList[i].gameObject.name.Contains("spikes"))
                {
                    spikeList[i].gameObject.SetActive(false);
                }
            }
        }
        itemActive = true; // ������ Ȱ��ȭ
    }

}
