using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance;
    public int m_PlayerFunds;
    public int m_EnemyFunds;

    void Awake()
    {
        m_instance = this;
    }
}
