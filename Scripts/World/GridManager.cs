using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager m_instance;

    [Header("Map Info")]
    [SerializeField] private int m_Width;
    [SerializeField] private int m_Height;
    [SerializeField] private Transform m_Cam;

    [Header("Tiles")]
    public GameObject m_Cursor;
    public GameObject[] m_Tiles;
    [SerializeField] private Tile m_BasicPrefab;

    void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        // GenerateGrid();
        m_Tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    void GenerateGrid()
    {
        for (int x = 0; x < m_Width; x++)
        {
            for (int y = 0; y < m_Height; y++)
            {
                var spawnedTile = Instantiate(m_BasicPrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
            }
        }
        float offset = 0.5f;
        m_Cam.position = new Vector3((float)m_Width / 2 - offset, (float)m_Height / 2 - offset, -10f);
    }
}
