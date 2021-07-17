using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{
    // Start is called before the first frame update
    void Start()
    {
        GetTileUnder();
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
}
