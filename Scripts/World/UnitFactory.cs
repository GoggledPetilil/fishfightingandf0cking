using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : Tile
{
    [Header("Unit Factory Traits")]
    [SerializeField] private bool m_isPlayerFactory;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        MenuManager.m_instance.HidePreviewUnit();
    }

    void Update()
    {
        if(MenuManager.m_instance.m_IsBuying && Input.GetMouseButtonDown(1))
        {
            MenuManager.m_instance.CloseFactoryMenu();
        }
    }

    void OnMouseDown()
    {
        // You can't click on tiles if you don't have permission to lol
        if(GridManager.m_instance.m_CanClick == false) return;

        if(GridManager.m_instance.m_PlayerSpawnTile.m_OccupiedUnit != null || !m_isPlayerFactory || UnitManager.m_instance.m_SelectedUnit != null)
        {
            base.OnMouseDown();
        }
        else
        {
            MenuManager.m_instance.OpenFactoryMenu();
        }
    }
}
