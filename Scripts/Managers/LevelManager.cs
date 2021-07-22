using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager m_instance;
    [SerializeField] private Animator m_SimpleFade;

    private string m_SceneName;

    void Awake()
    {
        m_instance = this;
        m_SimpleFade.gameObject.SetActive(true);
    }

    public void LoadNewLevel(string sceneName)
    {
        m_SceneName = sceneName;
        StartCoroutine("LevelTransition");
    }

    IEnumerator LevelTransition()
    {
        m_SimpleFade.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(m_SceneName);
    }
}
