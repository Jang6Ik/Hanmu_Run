using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public string sceneName;
    public GameObject pausePanel;
    public GameDIrector director;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName); // 게임 시작
        Time.timeScale = 1;
    }

    public void Lobby()
    {
        SceneManager.LoadScene("Lobby"); // 게임 시작
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit(); // 게임 종료
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        if (!director.IsPause)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        director.IsPause = true;
    }

    public void ResumeGame()
    {
        director.IsPause = false;
        pausePanel.SetActive(false); 
        Time.timeScale = 1;
    }

    public void BackGroundMusicOffButton() // 배경음악 키고 끄는 버튼
    {
        if (director.backmusic.isPlaying)
        {
            director.backmusic.Pause();
            director.BGMbutton.GetComponent<Image>().sprite = director.mute; // 스프라이트를 mute로 바꿈
        }
        else
        {
            director.backmusic.Play();
            director.BGMbutton.GetComponent<Image>().sprite = director.play; // 스프라이트를 play로 바꿈
        }
    }
}
