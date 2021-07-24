using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridManager : MonoBehaviour
{
    public static GridManager m_instance;

    [Header("Tile Logic")]
    public GameObject m_Cursor;
    public bool m_CanClick;
    public Color m_MoveTileColor;
    public Color m_AttackTileColor;
    public Color m_TargetTileColor;

    [Header("Tiles")]
    public GameObject[] m_Tiles;
    [SerializeField] private Tile m_BasicPrefab;

    [Header("Faction Factories")]
    public Tile m_PlayerSpawnTile;
    public Tile m_EnemySpawnTile;

    [Header("Map Info")]
    [SerializeField] private GameObject m_UnitInfoBox;
    [SerializeField] private GameObject m_TileInfoBox;

    void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        m_Tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    public void SetCursorPosition(Vector2 pos)
    {
        m_Cursor.transform.position = pos;
    }

    public void ToggleCursor(bool state)
    {
        m_Cursor.SetActive(state);
    }

    public void TileClickAllowed(bool state)
    {
        m_CanClick = state;
    }

    public void ShowHighlightedUnit(UnitBase unit)
    {
        if(unit == null)
        {
            m_UnitInfoBox.SetActive(false);
            return;
        }
        string top = unit.m_UnitName + " " + unit.m_HP.ToString() + "/" + unit.m_MaxHP.ToString();
        string bottom = "A " + unit.m_MeleeAtk + " / " + "P " + unit.m_RangeAtk + " / " + "D " + unit.m_Def.ToString();

        m_UnitInfoBox.GetComponentInChildren<TMP_Text>().text = top + "\n" + bottom;
        m_UnitInfoBox.SetActive(true);
    }

    public void ShowHighlightedTile(Tile tile)
    {
        if(tile == null)
        {
            m_TileInfoBox.SetActive(false);
            return;
        }
        string trait = "";
        if(!tile.m_Walkable)
        {
            trait = "Impassable";
        }
        else
        {
            trait = "Def +" + tile.m_DefBoost.ToString();
        }
        m_TileInfoBox.transform.GetChild(0).GetComponent<TMP_Text>().text = tile.m_TileName + "\n" + trait;
        m_TileInfoBox.SetActive(true);
    }

    public void SpawnNewUnit(GameObject unitPrefab)
    {
        Vector3 pos;
        if(TurnManager.m_instance.m_Phase == TurnManager.Phase.PlayerPhase)
        {
            pos = m_PlayerSpawnTile.transform.position;
        }
        else
        {
            pos = m_EnemySpawnTile.transform.position;
        }

        GameObject unit = Instantiate(unitPrefab, pos, Quaternion.identity) as GameObject;
        EffectsManager.m_instance.SpawnEgg(pos);
    }
}
