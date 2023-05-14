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
        SceneManager.LoadScene(sceneName); // ���� ����
        Time.timeScale = 1;
    }

    public void Lobby()
    {
        SceneManager.LoadScene("Lobby"); // ���� ����
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit(); // ���� ����
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

    public void BackGroundMusicOffButton() // ������� Ű�� ���� ��ư
    {
        if (director.backmusic.isPlaying)
        {
            director.backmusic.Pause();
            director.BGMbutton.GetComponent<Image>().sprite = director.mute; // ��������Ʈ�� mute�� �ٲ�
        }
        else
        {
            director.backmusic.Play();
            director.BGMbutton.GetComponent<Image>().sprite = director.play; // ��������Ʈ�� play�� �ٲ�
        }
    }
}
