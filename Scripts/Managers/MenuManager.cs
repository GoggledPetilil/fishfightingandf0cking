using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager m_instance;

    [Header("Unit Command Menu")]
    [SerializeField] private GameObject m_UnitCommandMenu;
    [SerializeField] private GameObject m_MeleeButton;
    [SerializeField] private GameObject m_ShootButton;

    [Header("Unit Buy Menu")]
    [SerializeField] private GameObject m_UnitBuyMenu;
    [SerializeField] private GameObject m_BuyPreview;

    [Header("General Menu")]
    [SerializeField] private GameObject m_EndButton;

    void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        m_UnitCommandMenu.SetActive(false);
        m_UnitBuyMenu.SetActive(false);
    }

    // Unit command menu
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

    // Unit Buy Menu
    public void ToggleUnitBuyMenu(bool state)
    {
        m_UnitBuyMenu.SetActive(state);
    }

    public void ToggleBuyPreview(bool state)
    {
        m_BuyPreview.SetActive(state);
    }

    // General stuff
    public void ToggleEndButton(bool state)
    {
        m_EndButton.SetActive(state);
    }
}
