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
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); // ������� ����
        if (backmusic.isPlaying) return;
        else
        {
            backmusic.Play();
            DontDestroyOnLoad(BackgroundMusic); // ������� ��� ���(���� ��ư ����)
        }

    }

    public void BackGroundMusicOffButton() // ������� Ű�� ���� ��ư
    {
        BackgroundMusic = GameObject.Find("S_bgm");
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); // ������� ����
        if (backmusic.isPlaying)
        {
            backmusic.Pause();
            BGMbutton.GetComponent<Image>().sprite = mute; // ��������Ʈ�� mute�� �ٲ�
        }
        else
        {
            backmusic.Play();
            BGMbutton.GetComponent<Image>().sprite = play; // ��������Ʈ�� play�� �ٲ�
        }
    }


}
