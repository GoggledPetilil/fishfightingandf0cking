using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    public enum Faction
    {
        Hero,
        Enemy
    }

    [Header("Unit Info")]
    public string m_UnitName;
    public Tile m_Occupying;
    public Faction m_Faction;

    [Header("Parameters")]
    public int m_MaxHP;
    public int m_HP;
    public int m_MeleeAtk;
    public int m_RangeAtk;
    public int m_Def;
    public int m_Mov;
    public int m_Range;

    [Header("Movement Data")]
    public bool m_Hasmoved = false; // When true, this unit's turn is over.
    public bool m_Moving = false;
    public float m_MoveSpeed = 5f; // How fast this unit moves through tiles.
    public Vector2 m_Velocity = new Vector2(); // How fast this unit is going.
    public Vector2 m_Destination = new Vector2(); // Where this unit will go towards.
    public List<Tile> m_TileRange = new List<Tile>();
    Stack<Tile> path = new Stack<Tile>();

    [Header("Visuals")]
    public Animator m_Anim;
    public SpriteRenderer m_sr;

    public void GetTileUnder()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        if(hit.collider != null && hit.collider.gameObject.CompareTag("Tile"))
        {
            Tile tile = hit.collider.gameObject.GetComponent<Tile>();
            tile.SetUnit(this);
        }
    }

    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1);
        Tile tile = null;

        if(hit != null)
        {
            tile = hit.collider.GetComponent<Tile>();
        }
        return tile;
    }

    public void ComputeAdjacencyList()
    {
        foreach(GameObject tile in GridManager.m_instance.m_Tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbours();
        }
    }

    public void FindSelectableTiles()
    {
        ComputeAdjacencyList();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(m_Occupying);
        m_Occupying.m_visited = true;

        while(process.Count > 0)
        {
            Tile t = process.Dequeue();

            m_TileRange.Add(t);
            t.ChangeColor(Color.blue);

            if(t.m_Distance < m_Mov)
            {
                foreach(Tile tile in t.m_AdjacentList)
                {
                    if(!tile.m_visited)
                    {
                        tile.m_LastTile = t;
                        tile.m_visited = true;
                        tile.m_Distance = 1 + t.m_Distance;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }

    public void MoveToTile(Tile tile)
    {
        path.Clear();
        m_Destination = tile.transform.position;
        m_Moving = true;

        Tile next = tile;
        while(next != null)
        {
            path.Push(next);
            next = next.m_LastTile;
        }
    }

    public void Move()
    {
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector2 target = t.transform.position;
            if(Vector2.Distance(transform.position, target) >= 0.05f)
            {
                CalculateHeading(target);
                SetVelocity();

                // Possible rotation here
                Vector3 v = new Vector3(m_Velocity.x, m_Velocity.y, 0f);
                transform.position += v * Time.deltaTime;
                MoveAnimation();
            }
            else
            {
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            ClearTileList();
            m_Moving = false;
            GetTileUnder();
            UnitManager.m_instance.SetSelectedHero(null);
            EndTurn();
        }
    }

    public void MoveAnimation()
    {
        if(m_Destination != Vector2.zero)
        {
            m_Anim.SetFloat("Horizontal", m_Destination.x);
            m_Anim.SetFloat("Vertical", m_Destination.y);
        }
    }

    public void ClearTileList()
    {
        foreach(Tile tile in m_TileRange)
        {
            tile.Reset();
        }
        m_TileRange.Clear();
    }

    void CalculateHeading(Vector2 target)
    {
        m_Destination = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        m_Destination.Normalize();
    }

    void SetVelocity()
    {
        m_Velocity = m_Destination * m_MoveSpeed;
    }

    void EndTurn()
    {
        m_Hasmoved = true;
        float c = 0.32f;
        m_sr.color = new Color(c, c, c, 1f);

        if(m_Faction == Faction.Hero)
        {
            TurnManager.m_instance.CheckPlayerUnits();
        }
        else
        {
            TurnManager.m_instance.CheckEnemyUnits();
        }
    }

    public void RefreshTurn()
    {
      m_Hasmoved = false;
      m_sr.color = new Color(1f, 1f, 1f, 1f);
    }
}
