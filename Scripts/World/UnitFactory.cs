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
            SoundManager.m_instance.PlayAudio(SoundManager.m_instance.m_Cancel);
            MenuManager.m_instance.ToggleEndButton(true);
        }
    }

    public override void OnMouseDown()
    {
        // You can't click on tiles if you don't have permission to lol
        if(GridManager.m_instance.m_CanClick == false) return;

        // Checking if the player can even check this tile this phase.
        Tile spawnTile = null;
        bool samePhase = false;
        if(TurnManager.m_instance.m_Phase == TurnManager.Phase.PlayerPhase)
        {
            // It's now the player phase.
            spawnTile = GridManager.m_instance.m_PlayerSpawnTile;
            if(m_isPlayerFactory)
            {
                samePhase = true;
            }
        }
        else if(TurnManager.m_instance.m_Phase == TurnManager.Phase.EnemyPhase)
        {
            // It's now the enemy phase.
            spawnTile = GridManager.m_instance.m_EnemySpawnTile;
            if(!m_isPlayerFactory)
            {
                samePhase = true;
            }
        }

        // Do the actual tile checking lol
        if(spawnTile.m_OccupiedUnit != null || !samePhase || UnitManager.m_instance.m_SelectedUnit != null)
        {
            base.OnMouseDown();
        }
        else
        {
            MenuManager.m_instance.OpenFactoryMenu();
            MenuManager.m_instance.ToggleEndButton(false);
            SoundManager.m_instance.PlayAudio(SoundManager.m_instance.m_Confirm);
        }
    }
}
