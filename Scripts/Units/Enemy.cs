using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        GetTileUnder();
        TurnManager.m_instance.AddUnit((Enemy)this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(m_Moving)
        {
            Move();
        }
    }

    public void CalculatePath()
    {
        //Tile targetTile = GetTargetTile(target);
        Tile targetTile = target.GetComponent<UnitBase>().m_Occupying;
        FindPath(targetTile);
    }

    public void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach(GameObject obj in targets)
        {
            float d = Vector2.Distance(transform.position, obj.transform.position);
            if(d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }
        target = nearest;
    }
}
