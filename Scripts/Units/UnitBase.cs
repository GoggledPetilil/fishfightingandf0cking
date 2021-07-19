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
    public HealthBar m_HealthBar;

    [Header("Parameters")]
    public int m_MaxHP;
    public int m_HP;
    public int m_MeleeAtk;
    public int m_RangeAtk;
    public int m_Def;
    public int m_Mov;
    public int m_ShootRange;

    [Header("Buffs")]
    public int m_MeleeBuff;
    public int m_RangeBuff;
    public int m_DefBuff;

    [Header("Movement Data")]
    public bool m_Hasmoved = false; // When true, this unit's turn is over.
    public bool m_Moving = false;
    public float m_MoveSpeed = 5f; // How fast this unit moves through tiles.
    public Vector2 m_Velocity = new Vector2(); // How fast this unit is going.
    public Vector2 m_Destination = new Vector2(); // Where this unit will go towards.
    public List<Tile> m_TileRange = new List<Tile>();
    // Exclusive to the AI:
    Stack<Tile> path = new Stack<Tile>();
    public Tile m_AITargetTile; // The target tile the AI will actually move to.


    [Header("Battle Data")]
    public bool m_IsAttacking; // This unit is currently trying to attack someone else.
    public GameObject m_Battler;
    public List<UnitBase> m_SelectableEnemies = new List<UnitBase>();


    [Header("Visuals")]
    public Animator m_Anim;
    public SpriteRenderer m_sr;

    public void DamageUnit(int damage)
    {
        Mathf.Clamp(damage, 0, Mathf.Infinity); // Prevent damage from being negative (thus healing this unit).
        m_HP = Mathf.Clamp(m_HP - damage, 0, m_MaxHP); // HP won't go above max or below 0.
        m_HealthBar.SetHealthBar(m_MaxHP, m_HP);

        EffectsManager.m_instance.SpawnPopUp(transform.position, damage.ToString());
    }

    public int GetMeleeAtk()
    {
        return m_MeleeAtk + m_MeleeBuff;
    }

    public int GetRangeAtk()
    {
        return m_RangeAtk + m_RangeBuff;
    }

    public int GetDefence()
    {
        return m_Def + m_DefBuff;
    }

    public void GetTileUnder()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        if(hit.collider != null && hit.collider.gameObject.CompareTag("Tile"))
        {
            Tile tile = hit.collider.gameObject.GetComponent<Tile>();
            tile.SetUnit(this);
        }
    }

    public void ComputeAdjacencyList(Tile target, bool ignoreOccupied)
    {
        foreach(GameObject tile in GridManager.m_instance.m_Tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbours(target, ignoreOccupied);
        }
    }

    public void FindSelectableTiles(int range)
    {
        ComputeAdjacencyList(null, true);

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(m_Occupying);
        m_Occupying.m_visited = true;

        while(process.Count > 0)
        {
            Tile t = process.Dequeue();

            m_TileRange.Add(t);

            if(t.m_Distance < range)
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

    public void GetAttackRange(int range)
    {
        ComputeAdjacencyList(null, false);
        m_TileRange.Clear();
        m_SelectableEnemies.Clear();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(m_Occupying);
        m_Occupying.m_visited = true;

        while(process.Count > 0)
        {
            Tile t = process.Dequeue();

            m_TileRange.Add(t);

            if(t.m_Distance < range)
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

        m_TileRange.Remove(m_Occupying);

        foreach(Tile tile in m_TileRange)
        {
            if(tile.m_OccupiedUnit != null && tile.m_OccupiedUnit.m_Faction != m_Faction && !m_SelectableEnemies.Contains(tile.m_OccupiedUnit))
            {
                m_SelectableEnemies.Add(tile.m_OccupiedUnit);
            }
        }
    }

    public void ShowSelectableTiles(Color c)
    {
        foreach(Tile t in m_TileRange)
        {
            t.ChangeColor(c);
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
            FinishedMoving();
        }
    }

    public void FinishedMoving()
    {
        m_Moving = false;
        GetTileUnder();

        if(m_Faction == Faction.Enemy)
        {
            // The enemy will Wait after their turn is over.
            UnitManager.m_instance.SetSelectedHero(null);
            EndTurn();
        }
        else
        {
            // This unit is a player, so give options.
            ClearTileList();

            // Check for melee enemies.
            GetAttackRange(1);
            if(m_SelectableEnemies.Count > 0)
            {
                // Found melee enemies, so allow melee combat.
                MenuManager.m_instance.ToggleMeleeButton(true);
            }
            else
            {
                // No melee enemies found.
                MenuManager.m_instance.ToggleMeleeButton(false);
            }

            // Check for ranged enemies.
            GetAttackRange(5);
            if(m_SelectableEnemies.Count > 0)
            {
                // Found ranged enemies, so allow shooting.
                MenuManager.m_instance.ToggleShootButton(true);
            }
            else
            {
                // No ranged enemies found. Disable Shoot.
                MenuManager.m_instance.ToggleShootButton(false);
            }

            MenuManager.m_instance.ToggleUnitCommandMenu(true);
        }
    }

    public void EndTurn()
    {
        m_Hasmoved = true;
        m_IsAttacking = false;
        ClearTileList();
        float c = 0.32f;
        m_sr.color = new Color(c, c, c, 1f);

        if(m_Faction == Faction.Hero)
        {
            TurnManager.m_instance.CheckPlayerUnits();
        }
        else
        {
            // This is an enemy unit.
            int i = TurnManager.m_instance.m_EnemyUnits.IndexOf((Enemy)this);
            TurnManager.m_instance.MoveNextEnemy(i + 1);
        }
    }

    public void RefreshTurn()
    {
        m_Hasmoved = false;
        m_IsAttacking = false;
        m_sr.color = new Color(1f, 1f, 1f, 1f);
    }

    public void Die()
    {
        ClearTileList();
        EffectsManager.m_instance.SpawnExplosion(this.transform.position);
        this.gameObject.transform.position = new Vector2(99, 99); // Get this dude off-screen.

        // Make the rest of the logic happen.
        if(m_Faction == Faction.Hero)
        {
            TurnManager.m_instance.DeleteHero((Hero)this);
            TurnManager.m_instance.CheckPlayerUnits();
        }
        else
        {
            TurnManager.m_instance.DeleteEnemy((Enemy)this);
        }
        Destroy(this.gameObject);
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

    // ########################################################################
    // AI SHIT HERE
    // YOU PROBABLY DON'T NEED TO TOUCH THESE
    // ########################################################################

    protected Tile FindLowestF(List<Tile> list)
    {
        Tile lowest = list[0];

        foreach(Tile t in list)
        {
            if(t.f < lowest.f)
            {
                lowest = t;
            }
        }

        list.Remove(lowest);

        return lowest;
    }

    protected Tile FindEndTile(Tile t)
    {
        Stack<Tile> tempPath = new Stack<Tile>();
        Tile next = t.m_LastTile;
        while(next != null)
        {
            tempPath.Push(next);
            next = next.m_LastTile;
        }

        if(tempPath.Count <= m_Mov)
        {
            return t.m_LastTile;
        }

        Tile endTile = null;
        for (int i = 0; i <= m_Mov; i++)
        {
            endTile = tempPath.Pop();
        }
        return endTile;
    }

    protected void FindPath(Tile target)
    {
        ComputeAdjacencyList(target, true);
        GetTileUnder();

        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(m_Occupying);
        m_Occupying.h = Vector2.Distance(m_Occupying.transform.position, target.transform.position);
        m_Occupying.f = m_Occupying.h;

        while(openList.Count > 0)
        {
            Tile t = FindLowestF(openList);

            closedList.Add(t);

            if(t == target)
            {
                m_AITargetTile = FindEndTile(t);
                MoveToTile(m_AITargetTile);
                return;
            }

            foreach(Tile tile in t.m_AdjacentList)
            {
                if(closedList.Contains(tile))
                {
                    // Do Nothing.
                }
                else if(openList.Contains(tile))
                {
                    float tempG = t.g + Vector2.Distance(tile.transform.position, t.transform.position);
                    if(tempG < tile.g)
                    {
                        tile.m_LastTile = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else
                {
                    tile.m_LastTile = t;
                    tile.g = t.g + Vector2.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector2.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);
                }
            }
        }
    }
}
