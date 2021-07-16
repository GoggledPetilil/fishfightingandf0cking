using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    public enum Faction
    {
        Hero,
        Enemy
    }

    public string m_UnitName;
    public Tile m_Occupying;
    public Faction m_Faction;
}
