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
            unit.FindSelectableTiles();
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
        SelectedUnitWaits();
    }

    public void SelectedUnitShoots()
    {
        SelectedUnitWaits();
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
