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
        m_AttackingUnit = attacker;
        m_DefendingUnit = defender;

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

        FishFight();
    }

    void FishFight()
    {
        int attackerAtk = 0; // The attack value used for the attacker.
        int defenderDef = 0; // The value used for the defender.
        if(m_DefenderCounters == false)
        {
            // This is a ranged battle.
            attackerAtk = m_AttackingUnit.GetRangeAtk();
        }
        else
        {
            // This is a melee battle.
            attackerAtk = m_AttackingUnit.GetMeleeAtk();
        }
        defenderDef = m_DefendingUnit.GetDefence();

        // Attacker will now attack the defender.
        m_DefendingUnit.DamageUnit(attackerAtk - defenderDef);


        // Checking if the Defender can fight back.
        if(m_DefendingUnit.m_HP < 1 || !m_DefenderCounters)
        {
            // Defender is fucking dead or can't counter.
        }
        else
        {
            // Defender can retaliate. So he does. Only during melee combat tho.
            int defenderAtk = m_DefendingUnit.GetMeleeAtk();
            int attackerDef = m_AttackingUnit.GetDefence();

            m_AttackingUnit.DamageUnit(defenderAtk - attackerDef);
        }

        if(m_DefendingUnit.m_HP < 1)
        {
            m_DefendingUnit.Die();
        }
        if(m_AttackingUnit.m_HP < 1)
        {
            m_AttackingUnit.Die();
        }
        else
        {
            m_AttackingUnit.EndTurn();
        }
    }
}
