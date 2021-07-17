using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        PlayerTurn,
        EnemyTurn,
    }

    public static GameManager m_instance;
    public GameState m_GameState;

    void Awake()
    {
        m_instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeState(GameState newState)
    {
        m_GameState = newState;
        switch(newState)
        {
            case GameState.PlayerTurn:
              break;
            case GameState.EnemyTurn:
              break;
        }
    }
}
