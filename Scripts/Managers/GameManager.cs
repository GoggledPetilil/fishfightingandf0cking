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
    [SerializeField] private int m_FundsThreshold; // If fund are bigger than this, bad stuff happens.
    public int m_MoneyPerTurn;

    [Header("Red Funds")]
    public int m_PlayerFunds;
    public Image m_RedFundsSlider;
    public TMP_Text m_RedFundsDisplay;
    public List<Magazine> m_RedMagazines = new List<Magazine>();

    [Header("Blue Funds")]
    public int m_EnemyFunds;
    public Image m_BlueFundsSlider;
    public TMP_Text m_BlueFundsDisplay;
    public List<Magazine> m_BlueMagazines = new List<Magazine>();


    void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        ChangePlayerFunds(0);
        ChangeEnemyFunds(0);
    }

    public void ChangePlayerFunds(int funds)
    {
        m_PlayerFunds += funds * (1 + m_RedMagazines.Count);
        float fill = (float)m_PlayerFunds / (float)m_FundsThreshold;
        m_RedFundsSlider.fillAmount = fill;
        m_RedFundsDisplay.text = m_PlayerFunds.ToString();
    }

    public void ChangeEnemyFunds(int funds)
    {
        m_EnemyFunds += funds * (1 + m_BlueMagazines.Count);
        float fill = (float)m_EnemyFunds / (float)m_FundsThreshold;
        m_BlueFundsSlider.fillAmount = fill;
        m_BlueFundsDisplay.text = m_EnemyFunds.ToString();
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
        int stagesBeaten = PlayerPrefs.GetInt("StagesBeaten", 1);
        if(stagesBeaten <= m_Chapter)
        {
            PlayerPrefs.SetInt("StagesBeaten", m_Chapter + 1);
        }
        LevelManager.m_instance.LoadNewLevel("MainMenu");
    }

    public void LoseGame()
    {
        LevelManager.m_instance.LoadNewLevel("MainMenu");
    }
}
