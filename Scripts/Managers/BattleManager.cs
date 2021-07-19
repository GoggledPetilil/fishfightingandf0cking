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

    public void StartBattle(UnitBase attacker, UnitBase defender)
    {
        Debug.Log("Battle Started! " + attacker + " vs " + defender);
        float distance = Vector2.Distance(attacker.transform.position, defender.transform.position);
        if(distance > 1.2f)
        {
            // Defender is further than 1 tile away.
            m_DefenderCounters = false;
        }
        else
        {
            m_DefenderCounters = true;
        }
    }
}
