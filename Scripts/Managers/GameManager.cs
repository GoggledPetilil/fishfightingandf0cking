using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance;

    void Awake()
    {
        m_instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
