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
        if(unit == null)
        {
            m_SelectedUnit.ClearTileList();
            m_SelectedUnit = unit;
        }
        else
        {
            m_SelectedUnit = unit;
            unit.FindSelectableTiles();
        }
    }
}
