using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Transform m_Cursor;

    [Header("Tile Data")]
    [SerializeField] private bool m_Walkable;
    public UnitBase m_OccupiedUnit;
    public bool m_IsWalkable => m_Walkable && m_OccupiedUnit == null;
    public int m_TravelCost;

    [Header("Range Data")]
    public List<Tile> m_AdjacentList = new List<Tile>();
    public bool m_visited;
    public Tile m_LastTile;
    public int m_Distance = 0;

    [SerializeField] private SpriteRenderer m_sr;

    // Start is called before the first frame update
    void Start()
    {
        m_Cursor = GridManager.m_instance.m_Cursor.transform;
    }

    public void SetUnit(UnitBase unit)
    {
        if(unit.m_Occupying != null) unit.m_Occupying.m_OccupiedUnit = null; // Unit won't occupy their last tile anymore.
        unit.transform.position = transform.position;
        m_OccupiedUnit = unit;
        unit.m_Occupying = this;
    }

    void OnMouseEnter()
    {
        m_Cursor.position = new Vector3(transform.position.x, transform.position.y, m_Cursor.position.z);
        MenuManager.m_instance.ShowHighlightedUnit(m_OccupiedUnit);
    }

    void OnMouseExit()
    {
        m_Cursor.position = new Vector3(99, 99, m_Cursor.position.z);
        MenuManager.m_instance.ShowHighlightedUnit(null);
    }

    void OnMouseDown()
    {
        if(GameManager.m_instance.m_GameState != GameManager.GameState.PlayerTurn) return;

        // It's the Player's Turn
        // This tile is occupied. Interact with the occupent.
        if(m_OccupiedUnit != null)
        {
            // Occupying unit is a Hero, so select the Hero.
            if(m_OccupiedUnit.m_Faction == UnitBase.Faction.Hero)
            {
                UnitManager.m_instance.SetSelectedHero((Hero)m_OccupiedUnit);
            }
            else
            {
                // Occupying unit is an enemy.
                if(UnitManager.m_instance.m_SelectedHero != null)
                {
                    // The player has a Hero selected.
                    // Hero will now beat the fguck out of the enemy that's occupying this tile.
                    UnitManager.m_instance.SetSelectedHero(null);
                }
            }
        }
        else
        {
            // This tile is free.
            if(UnitManager.m_instance.m_SelectedHero != null)
            {
                // The selected Hero will now walk to this tile.
                // If it's within range.
                if(UnitManager.m_instance.m_SelectedHero.m_TileRange.Contains(this))
                {
                    UnitManager.m_instance.m_SelectedHero.MoveToTile(this);
                }
                else
                {
                    Debug.Log("Tile not in range.");
                }
            }
        }
    }

    public void ChangeColor(Color c)
    {
        m_sr.color = c;
    }

    public void FindNeighbours()
    {
        Reset();
        CheckTile(Vector2.up);
        CheckTile(Vector2.down);
        CheckTile(Vector2.right);
        CheckTile(-Vector2.right);
    }

    public void Reset()
    {
        m_visited = false;
        m_LastTile = null;
        m_Distance = 0;
        m_AdjacentList.Clear();
        ChangeColor(Color.white);
    }

    public void CheckTile(Vector2 direction)
    {
        Vector2 size = new Vector2(0.25f, 0.25f);
        Vector3 p = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(p, size, 0f);

        foreach(Collider2D item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if(tile != null && tile.m_IsWalkable)
            {
                m_AdjacentList.Add(tile);
            }
        }
    }
}
