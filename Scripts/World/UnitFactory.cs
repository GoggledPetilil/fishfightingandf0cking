using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitFactory : Tile
{
    [Header("Unit Factory Traits")]
    [SerializeField] private bool m_isPlayerFactory;
    private bool m_IsBuying;

    [Header("Preview Info Box")]
    public TMP_Text m_PreviewName;
    public Image m_PreviewImage;
    public TMP_Text m_PreviewStat;


    // Start is called before the first frame update
    void Start()
    {
        Init();
        HidePreviewUnit();

        if(m_isPlayerFactory)
        {
            GridManager.m_instance.m_PlayerFactory = this;
        }
        else
        {
            GridManager.m_instance.m_EnemyFactory = this;
        }
    }

    void Update()
    {
        if(m_IsBuying && Input.GetMouseButtonDown(1))
        {
            CloseFactoryMenu();
        }
    }

    void OnMouseDown()
    {
        // You can't click on tiles if you don't have permission to lol
        if(GridManager.m_instance.m_CanClick == false) return;

        if(m_OccupiedUnit != null || !m_isPlayerFactory || UnitManager.m_instance.m_SelectedUnit != null)
        {
            base.OnMouseDown();
        }
        else
        {
            OpenFactoryMenu();
        }
    }

    void OpenFactoryMenu()
    {
        MenuManager.m_instance.ToggleUnitBuyMenu(true);
        m_IsBuying = true;

        CameraManager.m_instance.LockCamera(true);
        GridManager.m_instance.TileClickAllowed(false);
        GridManager.m_instance.ToggleCursor(false);
    }

    public void CloseFactoryMenu()
    {
        MenuManager.m_instance.ToggleUnitBuyMenu(false);
        m_IsBuying = false;

        CameraManager.m_instance.LockCamera(false);
        GridManager.m_instance.TileClickAllowed(true);
        GridManager.m_instance.ToggleCursor(true);
    }

    public void SpawnNewUnit(GameObject unitPrefab)
    {
        GameObject unit = Instantiate(unitPrefab, this.transform.position, Quaternion.identity) as GameObject;
        EffectsManager.m_instance.SpawnEgg(this.transform.position);
        if(m_isPlayerFactory)
        {
            Hero ub = unit.GetComponent<Hero>();
            TurnManager.m_instance.AddHeroUnit(ub);
        }
        else
        {
            Enemy ub = unit.GetComponent<Enemy>();
            TurnManager.m_instance.AddEnemyUnit(ub);
        }

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
}
