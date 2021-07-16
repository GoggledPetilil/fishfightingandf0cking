using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Transform m_Cursor;

    [Header("Tile Data")]
    [SerializeField] private bool m_Walkable;
    public UnitBase m_OccupiedUnit;
    public bool m_IsWalkable => m_Walkable && m_OccupiedUnit == null;

    // Start is called before the first frame update
    void Start()
    {
        m_Cursor = GridManager.m_instance.m_Cursor.transform;
    }

    public void SetUnit(UnitBase unit)
    {
        if(unit.m_Occupying != null) unit.m_Occupying.m_OccupiedUnit = null; // Unit won't occupy their last tile anymore.
        unit.transform.position = transform.position;
        m_OccupiedUnit = unit;
        unit.m_Occupying = this;
    }

    void OnMouseEnter()
    {
        m_Cursor.position = new Vector3(transform.position.x, transform.position.y, m_Cursor.position.z);
        MenuManager.m_instance.ShowHighlightedUnit(m_OccupiedUnit);
    }

    void OnMouseExit()
    {
        m_Cursor.position = new Vector3(99, 99, m_Cursor.position.z);
        MenuManager.m_instance.ShowHighlightedUnit(null);
    }

    void OnMouseDown()
    {
        if(GameManager.m_instance.m_GameState != GameManager.GameState.PlayerTurn) return;

        if(m_OccupiedUnit != null)
        {
            if(m_OccupiedUnit.m_Faction == UnitBase.Faction.Hero) UnitManager.m_instance.SetSelectedHero((Hero)m_OccupiedUnit);
            else
            {
                if(UnitManager.m_instance.m_SelectedHero != null)
                {
                    // Hero will now beat the fguck out of the enemy that's occupying this tile.
                    UnitManager.m_instance.SetSelectedHero(null);
                }
            }
        }
        else
        {
            if(UnitManager.m_instance.m_SelectedHero != null)
            {
                // The selected unit will now walk to this tile.
                SetUnit(UnitManager.m_instance.m_SelectedHero);
                UnitManager.m_instance.SetSelectedHero(null);
            }
        }
    }
}
