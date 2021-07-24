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
            if(GameManager.m_instance.m_PlayerFunds < m_Price && TurnManager.m_instance.m_Phase == TurnManager.Phase.PlayerPhase ||
            GameManager.m_instance.m_EnemyFunds < m_Price && TurnManager.m_instance.m_Phase == TurnManager.Phase.EnemyPhase)
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
        SoundManager.m_instance.PlayAudio(SoundManager.m_instance.m_Confirm);
        GridManager.m_instance.SpawnNewUnit(m_UnitPrefab);
        if(TurnManager.m_instance.m_Phase == TurnManager.Phase.PlayerPhase)
        {
            GameManager.m_instance.ChangePlayerFunds(-m_Price);
        }
        else
        {
            GameManager.m_instance.ChangeEnemyFunds(-m_Price);
        }

        MenuManager.m_instance.CloseFactoryMenu();
        MenuManager.m_instance.ToggleEndButton(true);
    }

    public void OnMouseEnter()
    {
        MenuManager.m_instance.ShowPreviewUnit(m_UnitPrefab.GetComponent<UnitBase>());
    }

    public void OnMouseExit()
    {
        MenuManager.m_instance.HidePreviewUnit();
    }
}
