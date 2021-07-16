using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Parameters")]
    public int m_MaxHP;     // Max amount of damage guy can take.
    public int m_HP;        // Current health this guy has. If 0, dies.
    public int m_MeleeAtk;  // How much damage this dude does up-close.
    public int m_RangeAtk;  // How much damage this dude does from far.
    public int m_Def;       // How much damage is subtracted.
    public int m_Mov;       // How far this guy can move.
    public int m_Range;     // How far this guy can attack from far.

    [Header("Boosts")]
    public int m_HealthMod;
    public int m_MeleeMod;
    public int m_RangeMod;
    public int m_DefMod;
}
