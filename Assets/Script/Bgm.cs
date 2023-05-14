using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bgm : MonoBehaviour
{

    public Sprite mute;
    public Sprite play;
    GameObject BGMbutton;
    GameObject BackgroundMusic;
    AudioSource backmusic;

    void Awake()
    {
        BackgroundMusic = GameObject.Find("S_bgm");
        BGMbutton = GameObject.Find("BgmButton");
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); // 배경음악 저장
        if (backmusic.isPlaying) return;
        else
        {
            backmusic.Play();
            DontDestroyOnLoad(BackgroundMusic); // 배경음악 계속 재생(이후 버튼 조작)
        }

    }

    public void BackGroundMusicOffButton() // 배경음악 키고 끄는 버튼
    {
        BackgroundMusic = GameObject.Find("S_bgm");
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); // 배경음악 저장
        if (backmusic.isPlaying)
        {
            backmusic.Pause();
            BGMbutton.GetComponent<Image>().sprite = mute; // 스프라이트를 mute로 바꿈
        }
        else
        {
            backmusic.Play();
            BGMbutton.GetComponent<Image>().sprite = play; // 스프라이트를 play로 바꿈
        }
    }


}
