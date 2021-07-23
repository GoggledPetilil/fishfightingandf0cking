using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager m_instance;

    [Header("Unit Command Menu")]
    [SerializeField] private GameObject m_UnitCommandMenu;
    [SerializeField] private GameObject m_MeleeButton;
    [SerializeField] private GameObject m_ShootButton;

    [Header("Unit Buy Menu")]
    public bool m_IsBuying;
    [SerializeField] private GameObject m_UnitBuyMenu;
    [SerializeField] private GameObject m_BuyPreview;
    [SerializeField] private TMP_Text m_PreviewName;
    [SerializeField] private Image m_PreviewImage;
    [SerializeField] private TMP_Text m_PreviewStat;

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

    public void OpenFactoryMenu()
    {
        MenuManager.m_instance.ToggleUnitBuyMenu(true);

        CameraManager.m_instance.LockCamera(true);
        GridManager.m_instance.TileClickAllowed(false);
        GridManager.m_instance.ToggleCursor(false);

        m_IsBuying = true;
    }

    public void CloseFactoryMenu()
    {
        MenuManager.m_instance.ToggleUnitBuyMenu(false);

        CameraManager.m_instance.LockCamera(false);
        GridManager.m_instance.TileClickAllowed(true);
        GridManager.m_instance.ToggleCursor(true);

        m_IsBuying = false;
    }

    public void ShowPreviewUnit(UnitBase u)
    {
        m_PreviewName.text = u.m_UnitName;
        m_PreviewImage.sprite = u.m_BattleSprite;

        string top = "ATK " + u.m_MeleeAtk.ToString() + " POW " + u.m_RangeAtk.ToString() + " DEF " + u.m_Def.ToString();
        string bottom = "MOV " + u.m_Mov.ToString() + "VIS " + u.m_ShootRange.ToString();

        m_PreviewStat.text = top + "\n" + bottom;

        m_PreviewName.gameObject.SetActive(true);
        m_PreviewImage.gameObject.SetActive(true);
        m_PreviewStat.gameObject.SetActive(true);
    }

    public void HidePreviewUnit()
    {
        m_PreviewName.gameObject.SetActive(false);
        m_PreviewImage.gameObject.SetActive(false);
        m_PreviewStat.gameObject.SetActive(false);
    }

    // General stuff
    public void ToggleEndButton(bool state)
    {
        m_EndButton.SetActive(state);
    }
}
