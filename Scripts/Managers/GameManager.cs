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

    [Header("Funds")]
    public int m_PlayerFunds;
    public int m_EnemyFunds;
    [SerializeField] private int m_FundsThreshold; // If fund are bigger than this, bad stuff happens.
    public Image m_FundsSlider;
    public TMP_Text m_FundsDisplay;
    public int m_MoneyPerTurn;

    void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        ChangePlayerFunds(0);
    }

    public void ChangePlayerFunds(int funds)
    {
        m_PlayerFunds += funds;
        float fill = (float)m_PlayerFunds / (float)m_FundsThreshold;
        m_FundsSlider.fillAmount = fill;
        m_FundsDisplay.text = m_PlayerFunds.ToString();
    }

    public void WinGame()
    {
        // Will check if m_Chapter is higher than the player save's chapter num
        // If yes, set it to this. If not, just ignore.


    }

    public void LoseGame()
    {

    }
}
