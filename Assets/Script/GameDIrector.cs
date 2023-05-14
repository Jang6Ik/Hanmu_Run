using UnityEngine;
using UnityEngine.UI;

public class GameDIrector : MonoBehaviour
{
    public Transform groundGenerator; // 그라운드 제네레이터의 위치
    public Player player; // 플레이어
    public Text scoreText; // 현재 점수를 표시할 UI
    public Text highscoreText; // 최고 점수를 표시할 UI
    public float Score; // 현재 점수
    public float highScore; // 최고 점수
    public float scoreSecond; // 1초당 증가할 점수
    public bool IsScore; // 점수가 증가할 수 있는 상태인지 확인
    public bool doubleScore; // 점수 두 배 확인
    public GameObject gameOver; // 게임 오버 메시지
    public bool IsPause; // 퍼즈 상태인가 아닌가 확인
    public AudioSource coinSound; // 효과음

    // 아이템 관련 변수
    private bool isDoubleScore; // 점수 두 배
    private bool safeMode; // 안전모드
    private bool itemActive; // 아이템 활성화 여부
    private float itemLengthCounter; // 아이템 지속시간
    private GroundGenerator theGroundGenerator; // 그라운드 제네레이터
    private float originalScoreSecond; // 점수 증가 초기값
    private float randomSpike; // 가시 빈도 설정
    private GroundDestroy[] spikeList; // 가시 모음

    // BGM 관련 변수
    public Sprite mute; // UI 스프라이트
    public Sprite play; // UI 스프라이트
    public GameObject BackgroundMusic; // BGM 오브젝트
    public AudioSource backmusic; // BGM 오디오 리소스
    public GameObject BGMbutton; // BGM 버튼

    private void Awake()
    {
        BackgroundMusic = GameObject.Find("S_bgm"); // S_bgm 이름의 오브젝트를 찾는다.
        BGMbutton = GameObject.Find("BgmButton"); // BgmButton 이름의 오브젝트를 찾는다.
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); // BackgroundMusic 오디오 리소스 저장
        if (backmusic.isPlaying) // BGM 재생중이면 실행 
        {
            BGMbutton.GetComponent<Image>().sprite = play; // 스프라이트를 play로 바꿈
        }
        else // 재생중이 아니면
        {
            BGMbutton.GetComponent<Image>().sprite = mute; // 스프라이트를 play로 바꿈
        }
    }

    void Start()
    {
        theGroundGenerator = FindObjectOfType<GroundGenerator>(); // 그라운드 제네레이터를 찾는다.

        if (PlayerPrefs.HasKey("HighScore")) // 최고 점수가 존재 한다면 실행
        {
            highScore = PlayerPrefs.GetFloat("HighScore"); // UI에 최고 점수 표시 
        }
    }

    void Update()
    {
        if (IsScore) // 점수가 증가할 수 있다면
        {
            Score += scoreSecond * Time.deltaTime; // 현재 점수에 1초마다 점수 추가
        }

        if (Score > highScore) // 현재점수가 최고 점수 보다 크다면
        {
            highScore = Score;  // 최고 점수는 현재점수가 된다.
            PlayerPrefs.SetFloat("HighScore", highScore); // 최고 점수를 갱신한다.
        }
        scoreText.text = "Score : " + (int)Score; // 현재 점수 표시
        highscoreText.text = "High Score : " + (int)highScore; // 최고 점수 표시

        if (itemActive) // 아이템이 활성화 되었다면 실행
        {
            itemLengthCounter -= Time.deltaTime; // 아이템 지속시간을 감소시킨다.

            if (isDoubleScore) // 점수 두 배 실행
            {
                scoreSecond = originalScoreSecond * 2.75f;
                doubleScore = true;
            }

            if (safeMode) // 안전 모드 실행
            {
                theGroundGenerator.randomSpike = 0f;
            }

            if (itemLengthCounter <= 0) // 지속시간이 끝나면 실행
            {
                scoreSecond = originalScoreSecond; // 점수 증가를 원래대로
                doubleScore = false;
                theGroundGenerator.randomSpike = randomSpike; // 가시 출현 빈도를 원래대로

                itemActive = false;
            }
        }

    }

    public void Restart() // 게임을 재시작한다.
    {
        IsScore = false; // 점수가 더이상 오르지 않게 한다.
        IsPause = true;
        player.gameObject.SetActive(false); // 플레이어를 비활성화한다.

        gameOver.SetActive(true); // 게임 오버 메시지를 활성화한다.
    }

    public void AddScore(int score)
    {
        if (doubleScore)
        {
            score = score * 2;
        }
        Score += score; // 현재점수에 Score만큼의 값을 더한다.
    }

    public void ActiveItem(bool score, bool safe, float time, float plus)
    {
        isDoubleScore = score; // 점수 두 배 활성화/비활성화 설정
        safeMode = safe; // 안전모드 활성화/비활성화 설정
        itemLengthCounter = time; // 아이템 지속시간 설정
        Score += plus; // 추가 점수가 있다면 점수에 더해준다.

        originalScoreSecond = scoreSecond; // 점수 증가 초기값 저장
        randomSpike = theGroundGenerator.randomSpike; // 가시 빈도 초기값 저장

        if (safeMode) // 안전 모드면 실행
        {
            spikeList = FindObjectsOfType<GroundDestroy>(); // GroundDestroy를 가진 오브젝트를 찾는다.
            for (int i = 0; i < spikeList.Length; i++) // 이름에 spikes를 포함한 오브젝트를 비활성화 한다.
            {
                if (spikeList[i].gameObject.name.Contains("spikes"))
                {
                    spikeList[i].gameObject.SetActive(false);
                }
            }
        }
        itemActive = true; // 아이템 활성화
    }

}
