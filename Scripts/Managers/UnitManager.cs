using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager m_instance;

    public UnitBase m_SelectedUnit;

    void Awake()
    {
        m_instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetSelectedHero(UnitBase unit)
    {
        ClearSelectedTileRange();
        if(unit == null)
        {
            m_SelectedUnit = unit;
        }
        else
        {
            m_SelectedUnit = unit;
            unit.FindSelectableTiles(unit.m_Mov);
            unit.ShowSelectableTiles(GridManager.m_instance.m_MoveTileColor);
        }
    }

    public void ClearSelectedTileRange()
    {
        if(m_SelectedUnit != null && m_SelectedUnit.m_TileRange.Count > 0)
        {
            m_SelectedUnit.ClearTileList();
        }
    }

    public void SelectedUnitAttacks()
    {
        m_SelectedUnit.m_IsAttacking = true;
        m_SelectedUnit.GetAttackRange(1);
        m_SelectedUnit.ShowSelectableTiles(GridManager.m_instance.m_AttackTileColor);

        GridManager.m_instance.TileClickAllowed(true);
        GridManager.m_instance.ToggleCursor(true);
        MenuManager.m_instance.ToggleUnitCommandMenu(false);

        // The unit will now be allowed to click a valid tile to attack an enemy.
    }

    public void SelectedUnitShoots()
    {
        m_SelectedUnit.m_IsAttacking = true;
        m_SelectedUnit.GetAttackRange(m_SelectedUnit.m_ShootRange);
        m_SelectedUnit.ShowSelectableTiles(GridManager.m_instance.m_AttackTileColor);

        GridManager.m_instance.TileClickAllowed(true);
        GridManager.m_instance.ToggleCursor(true);
        MenuManager.m_instance.ToggleUnitCommandMenu(false);

        // The unit will now be allowed to click a valid tile to attack an enemy.
    }

    public void SelectedUnitWaits()
    {
        MenuManager.m_instance.ToggleUnitCommandMenu(false);
        GridManager.m_instance.TileClickAllowed(true);
        GridManager.m_instance.ToggleCursor(true);

        if(m_SelectedUnit != null)
        {
            m_SelectedUnit.EndTurn();
        }
        SetSelectedHero(null);
    }
}
