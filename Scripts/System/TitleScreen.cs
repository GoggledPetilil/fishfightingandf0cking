using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject m_IntroFlag;
    [SerializeField] private AudioClip m_GameStart;
    [SerializeField] private AudioSource m_TitleMusic;

    // Update is called once per frame
    void Update()
    {
        if(m_IntroFlag.activeSelf == false && Input.GetMouseButtonDown(0))
        {
            SoundManager.m_instance.PlayAudio(m_GameStart);
            LevelManager.m_instance.LoadNewLevel("MainMenu");
        }

        if(m_IntroFlag.activeSelf == false && m_TitleMusic.isPlaying == false)
        {
            m_TitleMusic.Play();
        }
    }
}
