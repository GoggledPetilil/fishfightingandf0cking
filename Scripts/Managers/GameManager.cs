using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance;

    [Header("Game Info")]
    [SerializeField] private int m_Chapter;
    public bool m_IsMultiplayer;

    [Header("General Funds")]
    public int m_FundsThreshold; // If fund are bigger than this, bad stuff happens.
    public int m_MoneyPerTurn;

    [Header("Red Funds")]
    public int m_PlayerFunds;
    public Image m_RedFundsSlider;
    public Animator m_RedAnimator;
    public TMP_Text m_RedFundsDisplay;
    public List<Magazine> m_RedMagazines = new List<Magazine>();

    [Header("Blue Funds")]
    public int m_EnemyFunds;
    public Image m_BlueFundsSlider;
    public Animator m_BlueAnimator;
    public TMP_Text m_BlueFundsDisplay;
    public List<Magazine> m_BlueMagazines = new List<Magazine>();


    void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        ChangePlayerFunds(m_MoneyPerTurn);
        ChangeEnemyFunds(0);
        if(TitleMusic.m_instance != null)
        {
            Destroy(TitleMusic.m_instance.gameObject);
        }
    }

    public void ChangePlayerFunds(int funds)
    {
        m_PlayerFunds += funds;
        float fill = (float)m_PlayerFunds / (float)m_FundsThreshold;
        m_RedFundsSlider.fillAmount = fill;
        m_RedFundsDisplay.text = m_PlayerFunds.ToString();

        if(m_PlayerFunds >= m_FundsThreshold)
        {
            m_RedAnimator.SetBool("TooMuch", true);
        }
        else
        {
            m_RedAnimator.SetBool("TooMuch", false);
        }
    }

    public void ChangeEnemyFunds(int funds)
    {
        m_EnemyFunds += funds;
        float fill = (float)m_EnemyFunds / (float)m_FundsThreshold;
        m_BlueFundsSlider.fillAmount = fill;
        m_BlueFundsDisplay.text = m_EnemyFunds.ToString();

        if(m_EnemyFunds >= m_FundsThreshold)
        {
            m_BlueAnimator.SetBool("TooMuch", true);
        }
        else
        {
            m_BlueAnimator.SetBool("TooMuch", false);
        }
    }

    public void RemoveMagazine(Magazine mag)
    {
        if(m_RedMagazines.Contains(mag))
        {
            m_RedMagazines.Remove(mag);
        }
        else if(m_BlueMagazines.Contains(mag))
        {
            m_BlueMagazines.Remove(mag);
        }
    }

    public void WinGame()
    {
        GridManager.m_instance.ToggleCursor(false);
        GridManager.m_instance.TileClickAllowed(false);
        MenuManager.m_instance.ToggleEndButton(false);
        Invoke("WinTheGame", 2f);
    }

    void WinTheGame()
    {
        int stagesBeaten = PlayerPrefs.GetInt("StagesBeaten", 1);
        if(stagesBeaten <= m_Chapter)
        {
            PlayerPrefs.SetInt("StagesBeaten", m_Chapter + 1);
        }
        LevelManager.m_instance.LoadNewLevel("MainMenu");
    }

    public void LoseGame()
    {
        GridManager.m_instance.ToggleCursor(false);
        GridManager.m_instance.TileClickAllowed(false);
        MenuManager.m_instance.ToggleEndButton(false);
        Invoke("LoseTheGame", 2f);
    }

    void LoseTheGame()
    {
        LevelManager.m_instance.LoadNewLevel("MainMenu");
    }
}
