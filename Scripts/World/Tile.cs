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

    [Header("Enemy AI Data")]
    public float f = 0f;
    public float g = 0f;
    public float h = 0f;

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
        CameraManager.m_instance.SetCameraTarget(new Vector2(transform.position.x, transform.position.y));
        MenuManager.m_instance.ShowHighlightedUnit(m_OccupiedUnit);
    }

    void OnMouseExit()
    {
        m_Cursor.position = new Vector3(99, 99, m_Cursor.position.z);
        MenuManager.m_instance.ShowHighlightedUnit(null);
    }

    void OnMouseDown()
    {
        // You can't click on tiles if you don't have permission to lol
        if(GridManager.m_instance.m_CanClick == false) return;

        if(m_OccupiedUnit != null)
        {
            // This tile is occupied. Interact with the occupent:
            if(m_OccupiedUnit.m_Faction == UnitBase.Faction.Hero)
            {
                // Occupying unit is a Hero.
                if(m_OccupiedUnit != UnitManager.m_instance.m_SelectedUnit)
                {
                    // This is a different hero, so select this unit instead.
                    UnitManager.m_instance.SetSelectedHero((Hero)m_OccupiedUnit);
                }
                else
                {
                    // The occupying unit is the same as the unit already selected.
                    m_OccupiedUnit.FinishedMoving();
                    GridManager.m_instance.TileClickAllowed(false);
                    GridManager.m_instance.ToggleCursor(false);
                }
            }
            else
            {
                // Occupying unit is an enemy, so display enemy's move range.
                UnitManager.m_instance.SetSelectedHero((Enemy)m_OccupiedUnit);
            }
        }
        else
        {
            // This tile is free.
            if(UnitManager.m_instance.m_SelectedUnit != null)
            {
                // The Player has a unit selected
                UnitBase unit = UnitManager.m_instance.m_SelectedUnit;
                if(unit.m_TileRange.Contains(this) && !unit.m_Hasmoved && !unit.m_Moving && unit.m_Faction == UnitBase.Faction.Hero)
                {
                    // The selected Player unit will now move to this tile.
                    UnitManager.m_instance.m_SelectedUnit.MoveToTile(this);
                    GridManager.m_instance.TileClickAllowed(false);
                    GridManager.m_instance.ToggleCursor(false);
                }
                else
                {
                    // Player clicked a tile out of range or had an enemy selected, so deselect the unit.
                    UnitManager.m_instance.SetSelectedHero(null);
                }
            }
        }
    }

    public void ChangeColor(Color c)
    {
        m_sr.color = c;
    }

    public void FindNeighbours(Tile target)
    {
        Reset();
        CheckTile(Vector2.up, target);
        CheckTile(Vector2.down, target);
        CheckTile(Vector2.right, target);
        CheckTile(-Vector2.right, target);
    }

    public void Reset()
    {
        m_visited = false;
        m_LastTile = null;
        m_Distance = 0;
        m_AdjacentList.Clear();
        ChangeColor(Color.white);

        f = g = h = 0f;
    }

    public void CheckTile(Vector2 direction, Tile target)
    {
        Vector2 size = new Vector2(0.25f, 0.25f);
        Vector3 p = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(p, size, 0f);

        foreach(Collider2D item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if(tile != null && tile.m_IsWalkable || tile == target)
            {
                m_AdjacentList.Add(tile);
            }
        }
    }
}
