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
    public List<Enemy> m_EnemyUnits = new List<Enemy>();

    void Awake()
    {
        m_instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Phase = Phase.PlayerPhase;
    }

    public void AddHeroUnit(Hero unit)
    {
        m_PlayerUnits.Add(unit);
    }

    public void AddEnemyUnit(Enemy unit)
    {
        m_EnemyUnits.Add(unit);
    }

    public void DeleteHero(Hero unit)
    {
        int i = m_PlayerUnits.IndexOf(unit);
        m_PlayerUnits.RemoveAt(i);
    }

    public void DeleteEnemy(Enemy unit)
    {
        int i = m_EnemyUnits.IndexOf(unit);
        m_EnemyUnits.RemoveAt(i);
    }

    public void CheckPlayerUnits()
    {
        int i = 0;
        foreach(UnitBase unit in m_PlayerUnits)
        {
            if(unit.m_Hasmoved)
            {
                i++;
                if(i >= m_PlayerUnits.Count)
                {
                    SwitchPhase();
                }
            }
        }
    }

    public void SwitchPhase()
    {
        if(m_Phase == Phase.PlayerPhase)
        {
            // It is now Enemy Phase
            m_Phase = Phase.EnemyPhase;
            GridManager.m_instance.ToggleCursor(false);
            GridManager.m_instance.TileClickAllowed(false);
            MoveNextEnemy(0);

        }
        else
        {
            // It is now Player Phase
            m_Phase = Phase.PlayerPhase;
            GridManager.m_instance.ToggleCursor(true);
            GridManager.m_instance.TileClickAllowed(true);
        }

        foreach(Hero hero in m_PlayerUnits)
        {
            hero.RefreshTurn();
        }

        foreach(Enemy enemy in m_EnemyUnits)
        {
            enemy.RefreshTurn();
        }
    }

    public void MoveNextEnemy(int index)
    {
        if(index < m_EnemyUnits.Count)
        {
            Enemy enemy = m_EnemyUnits[index];

            enemy.RefreshTurn();

            enemy.FindNearestTarget();
            enemy.CalculatePath();
            enemy.FindSelectableTiles();
        }
        else
        {
            // The last enemy has already moved, so switch phase.
            SwitchPhase();
        }
    }
}
