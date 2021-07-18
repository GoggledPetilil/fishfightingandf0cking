using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager m_instance;

    [Header("Tile Logic")]
    public GameObject m_Cursor;
    public bool m_CanClick;

    [Header("Tiles")]
    public GameObject[] m_Tiles;
    [SerializeField] private Tile m_BasicPrefab;

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
}
