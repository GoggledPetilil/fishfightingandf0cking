using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Transform m_Cursor;

    // Start is called before the first frame update
    void Start()
    {
        m_Cursor = GridManager.m_instance.m_Cursor.transform;
    }

    void OnMouseEnter()
    {
        m_Cursor.position = new Vector3(transform.position.x, transform.position.y, m_Cursor.position.z);
    }
}
