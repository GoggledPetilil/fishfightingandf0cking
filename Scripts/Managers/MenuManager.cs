using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager m_instance;

    [SerializeField] private GameObject m_UnitCommandMenu;
    [SerializeField] private GameObject m_MeleeButton;
    [SerializeField] private GameObject m_ShootButton;

    [SerializeField] private GameObject m_EndButton;

    void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        m_UnitCommandMenu.SetActive(false);
    }

    public void ToggleUnitCommandMenu(bool state)
    {
        m_UnitCommandMenu.SetActive(state);
    }

    public void ToggleMeleeButton(bool state)
    {
        m_MeleeButton.SetActive(state);
    }

    public void ToggleShootButton(bool state)
    {
        m_ShootButton.SetActive(state);
    }

    public void ToggleEndButton(bool state)
    {
        m_EndButton.SetActive(state);
    }
}
