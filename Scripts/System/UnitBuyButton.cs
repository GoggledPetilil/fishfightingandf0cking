using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitBuyButton : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private GameObject m_UnitPrefab;
    [SerializeField] private int m_Price;

    [Header("Components")]
    [SerializeField] private TMP_Text m_UnitNameField;
    [SerializeField] private TMP_Text m_PriceField;
    private Button m_ThisButton;

    private bool m_firstEnable;

    void Awake()
    {
        m_ThisButton = this.gameObject.GetComponent<Button>();
    }

    void OnEnable()
    {
        if(m_firstEnable == false)
        {
            m_firstEnable = true; // Literally here just so Unity doesn't complain about the GameManager not existing yet.
        }
        else
        {
            if(GameManager.m_instance.m_PlayerFunds < m_Price)
            {
                m_ThisButton.interactable = false;
            }
            else
            {
                m_ThisButton.interactable = true;
            }
        }
    }

    public void BuyThisUnit()
    {
        // This method is called when the button is pressed. So assume funds are sorted.
        GameManager.m_instance.m_PlayerFunds -= m_Price;
        GridManager.m_instance.m_PlayerFactory.SpawnNewUnit(m_UnitPrefab);

        GridManager.m_instance.m_PlayerFactory.CloseFactoryMenu();
    }

    public void OnMouseEnter()
    {
        GridManager.m_instance.m_PlayerFactory.ShowPreviewUnit(m_UnitPrefab.GetComponent<UnitBase>());
    }

    public void OnMouseExit()
    {
        GridManager.m_instance.m_PlayerFactory.HidePreviewUnit();
    }
}
