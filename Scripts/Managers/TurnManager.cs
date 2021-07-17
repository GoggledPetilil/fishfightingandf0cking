using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager m_instance;

    public enum Phase
    {
        PlayerPhase,
        EnemyPhase
    }

    public Phase m_Phase;
    public List<Hero> m_PlayerUnits = new List<Hero>();
    public List<Enemy> m_EnemyUnits = new List<Enemy>();

    void Awake()
    {
        m_instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetAllUnits();
        SwitchPhase();
    }

    public void SwitchPhase()
    {
        /*if(m_Phase == TurnManager.PlayerPhase)
        {
            m_Phase = TurnManager.EnemyPhase;
        }
        else
        {
            m_Phase = TurnManager.PlayerPhase;
        }*/
    }

    public void GetAllUnits()
    {
        //m_PlayerUnits.AddRange(new List<Hero>(GameObject.FindGameObjectsWithTag("Player").GetComponent<Hero>()));
    }
}
