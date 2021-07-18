using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager m_instance;

    [SerializeField] private GameObject m_UnitInfoBox;
    [SerializeField] private GameObject m_UnitCommandMenu;

    void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        m_UnitCommandMenu.SetActive(false);
    }

    public void ShowHighlightedUnit(UnitBase unit)
    {
        if(unit == null)
        {
            m_UnitInfoBox.SetActive(false);
            return;
        }
        m_UnitInfoBox.GetComponentInChildren<TMP_Text>().text = unit.m_UnitName;
        m_UnitInfoBox.SetActive(true);
    }

    public void ToggleUnitCommandMenu(bool state)
    {
        m_UnitCommandMenu.SetActive(state);
    }
}
