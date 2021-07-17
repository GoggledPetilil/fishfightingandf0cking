using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager m_instance;

    [SerializeField] private GameObject m_UnitInfoBox;

    void Awake()
    {
        m_instance = this;
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
}
