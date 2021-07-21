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

    void Update()
    {
        if(m_SelectedUnit != null && TurnManager.m_instance.m_Phase == TurnManager.Phase.PlayerPhase && !m_SelectedUnit.m_Moving
        && Input.GetMouseButtonDown(1))
        {
            m_SelectedUnit.m_OriginTile.SetUnit(m_SelectedUnit);
            m_SelectedUnit.m_IsAttacking = false;
            SetSelectedHero(null);

            MenuManager.m_instance.ToggleUnitCommandMenu(false);
            MenuManager.m_instance.ToggleEndButton(true);
            GridManager.m_instance.TileClickAllowed(true);
            GridManager.m_instance.ToggleCursor(true);
        }
    }

    public void SetSelectedHero(UnitBase unit)
    {
        ClearSelectedTileRange();
        if(unit == null)
        {
            m_SelectedUnit = unit;
            if(TurnManager.m_instance.m_Phase == TurnManager.Phase.PlayerPhase)
            {
                MenuManager.m_instance.ToggleEndButton(true);
            }
        }
        else
        {
            m_SelectedUnit = unit;
            unit.FindSelectableTiles(unit.m_Mov);
            unit.ShowSelectableTiles(GridManager.m_instance.m_MoveTileColor);
            MenuManager.m_instance.ToggleEndButton(false);
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
        m_SelectedUnit.ColorValidTargets();

        GridManager.m_instance.TileClickAllowed(true);
        GridManager.m_instance.ToggleCursor(true);
        MenuManager.m_instance.ToggleUnitCommandMenu(false);

        // The unit will now be allowed to click a valid tile to attack an enemy.
    }

    public void SelectedUnitShoots()
    {
        m_SelectedUnit.m_IsAttacking = true;
        m_SelectedUnit.GetAttackRange(m_SelectedUnit.m_ShootRange);
        UnitBase t = m_SelectedUnit.GetClosestTarget();
        m_SelectedUnit.m_SelectableEnemies.Clear();
        m_SelectedUnit.m_SelectableEnemies.Add(t);
        m_SelectedUnit.ColorValidTargets();

        GridManager.m_instance.TileClickAllowed(true);
        GridManager.m_instance.ToggleCursor(true);
        MenuManager.m_instance.ToggleUnitCommandMenu(false);


        // The unit will now be allowed to click a valid tile to attack an enemy.
    }

    public void SelectedUnitWaits()
    {
        MenuManager.m_instance.ToggleUnitCommandMenu(false);
        MenuManager.m_instance.ToggleEndButton(true);
        GridManager.m_instance.TileClickAllowed(true);
        GridManager.m_instance.ToggleCursor(true);
        CameraManager.m_instance.LockCamera(false);

        if(m_SelectedUnit != null)
        {
            m_SelectedUnit.EndTurn();
        }
        SetSelectedHero(null);
    }
}
