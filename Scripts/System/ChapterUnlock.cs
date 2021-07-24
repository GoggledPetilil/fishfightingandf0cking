using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterUnlock : MonoBehaviour
{
    [SerializeField] private int m_Chapter;
    private Button m_Butt;

    void Awake()
    {
        m_Butt = this.gameObject.GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int stagesBeaten = PlayerPrefs.GetInt("StagesBeaten", 1);
        if(stagesBeaten < m_Chapter)
        {
            m_Butt.interactable = false;
        }
        else
        {
            m_Butt.interactable = true;
        }
    }
}
