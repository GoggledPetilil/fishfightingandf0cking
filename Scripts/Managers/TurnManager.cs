using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum Phase
    {
        PlayerPhase,
        EnemyPhase
    }

    public static TurnManager m_instance;

    public Phase m_Phase;
    public List<UnitBase> m_PlayerUnits = new List<UnitBase>();
    public List<UnitBase> m_EnemyUnits = new List<UnitBase>();

    void Awake()
    {
        m_instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Phase = Phase.PlayerPhase;
    }

    public void AddUnit(UnitBase unit)
    {
        if(unit.m_Faction == UnitBase.Faction.Hero)
        {
            m_PlayerUnits.Add(unit);
        }
        else
        {
            m_EnemyUnits.Add(unit);
        }
    }

    public void DeleteUnit(UnitBase unit)
    {
        if(unit.m_Faction == UnitBase.Faction.Hero)
        {
            int i = m_PlayerUnits.IndexOf(unit);
            m_PlayerUnits.RemoveAt(i);
        }
        else
        {
          int i = m_EnemyUnits.IndexOf(unit);
          m_EnemyUnits.RemoveAt(i);
        }
    }

    public void CheckPlayerUnits()
    {
        int i = 0;
        foreach(UnitBase unit in m_PlayerUnits)
        {
            if(unit.m_Hasmoved)
            {
                i++;
            }
        }
        if(i >= m_PlayerUnits.Count)
        {
            SwitchPhase();
        }
    }

    public void SwitchPhase()
    {
        if(m_Phase == Phase.PlayerPhase)
        {
            m_Phase = Phase.EnemyPhase;
            MoveAllEnemies();
        }
        else
        {
            m_Phase = Phase.PlayerPhase;
        }
        Debug.Log(m_Phase);
    }

    public void MoveAllEnemies()
    {
        foreach(Enemy enemy in m_EnemyUnits)
        {
            enemy.RefreshTurn();

            enemy.FindNearestTarget();
            enemy.CalculatePath();
            enemy.FindSelectableTiles();
        }
        SwitchPhase();
    }
}
