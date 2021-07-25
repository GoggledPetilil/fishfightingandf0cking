using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Text m_MenuLabel;
    [SerializeField] private GameObject m_MainMenu;
    [SerializeField] private GameObject m_SingleMenu;
    [SerializeField] private GameObject m_MultiMenu;
    [SerializeField] private GameObject m_CreditsMenu;

    void Start()
    {
        ShowMainMenu();
    }

    void HideAll()
    {
        m_MainMenu.SetActive(false);
        m_SingleMenu.SetActive(false);
        m_MultiMenu.SetActive(false);
        m_CreditsMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        HideAll();
        m_MainMenu.SetActive(true);
        m_MenuLabel.text = "MAIN MENU";
        SoundManager.m_instance.PlayAudio(SoundManager.m_instance.m_Confirm);
    }

    public void ShowSingleMenu()
    {
        HideAll();
        m_SingleMenu.SetActive(true);
        m_MenuLabel.text = "SINGLEPLAYER";
        SoundManager.m_instance.PlayAudio(SoundManager.m_instance.m_Confirm);
    }

    public void ShowMultiMenu()
    {
        HideAll();
        m_MultiMenu.SetActive(true);
        m_MenuLabel.text = "MULTIPLAYER";
        SoundManager.m_instance.PlayAudio(SoundManager.m_instance.m_Confirm);
    }

    public void ShowCreditsMenu()
    {
        HideAll();
        m_CreditsMenu.SetActive(true);
        m_MenuLabel.text = "CREDITS";
        SoundManager.m_instance.PlayAudio(SoundManager.m_instance.m_Confirm);
    }
}
