using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : Tile
{
    [Header("Magazine Components")]
    [SerializeField] private SpriteRenderer m_MagazineGraphic;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void TileFunction()
    {
        UnitBase unit = m_OccupiedUnit;
        if(unit != null)
        {
            if(GameManager.m_instance.m_RedMagazines.Contains(this) || GameManager.m_instance.m_BlueMagazines.Contains(this)) return;

            if(unit.m_Faction == UnitBase.Faction.Hero)
            {
                m_MagazineGraphic.color = Color.red;
                GameManager.m_instance.RemoveMagazine(this);
                GameManager.m_instance.m_RedMagazines.Add(this);
            }
            else if(unit.m_Faction == UnitBase.Faction.Enemy)
            {
                m_MagazineGraphic.color = Color.blue;
                GameManager.m_instance.RemoveMagazine(this);
                GameManager.m_instance.m_BlueMagazines.Add(this);
            }
            EffectsManager.m_instance.SpawnPopUp(this.transform.position, "CAPTURED!");
            SoundManager.m_instance.PlayAudio(SoundManager.m_instance.m_CaptureSound);
        }
        else
        {
            m_MagazineGraphic.color = Color.white;
            GameManager.m_instance.RemoveMagazine(this);
        }
    }
}
