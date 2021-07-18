using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager m_instance;

    [Header("Participant Info")]
    public UnitBase m_AttackingUnit;
    public UnitBase m_DefendingUnit;
    public bool m_DefenderCounters; // If true, the Defending unit will counter attack.

    [Header("Battle Screen")]
    public GameObject m_RedBattler;
    public GameObject m_BlueBattler;

    void Awake()
    {
        m_instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
