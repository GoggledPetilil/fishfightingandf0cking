using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private GameObject m_IntroFlag;

    // Update is called once per frame
    void Update()
    {
        if(m_IntroFlag.activeSelf == false && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
