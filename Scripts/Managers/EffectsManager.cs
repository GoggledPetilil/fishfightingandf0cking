using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager m_instance;

    [Header("Effects")]
    [SerializeField] private GameObject m_TextPopUp;
    [SerializeField] private GameObject m_Explosion;
    [SerializeField] private GameObject m_EggHatch;

    void Awake()
    {
        m_instance = this;
    }

    public void SpawnPopUp(Vector2 pos, string text)
    {
        GameObject g = Instantiate(m_TextPopUp, pos, Quaternion.identity) as GameObject;
        g.GetComponent<TextPopUp>().ChangeText(text);
    }

    public void SpawnExplosion(Vector2 pos)
    {
        Instantiate(m_Explosion, pos, Quaternion.identity);
    }

    public void SpawnEgg(Vector2 pos)
    {
        Instantiate(m_EggHatch, pos, Quaternion.identity);
    }
}
