using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager m_instance;

    void Awake()
    {
        m_instance = this;
    }

    public void LoadNewLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
