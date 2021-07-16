using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager m_instance;

    public Hero m_SelectedHero;

    void Awake()
    {
        m_instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetSelectedHero(Hero hero)
    {
        m_SelectedHero = hero;
    }
}
