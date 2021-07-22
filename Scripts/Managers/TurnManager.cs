using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public enum Phase
    {
        PlayerPhase,
        EnemyPhase
    }

    public static TurnManager m_instance;

    public Phase m_Phase;
    public List<UnitBase> m_PlayerUnits = new List<UnitBase>();
    public List<Enemy> m_EnemyUnits = new List<Enemy>();

    public Enemy m_CurrentlyMoving;

    [Header("Phase Change Assets")]
    [SerializeField] private GameObject m_PhaseHolder;
    [SerializeField] private TMP_Text m_PhaseText;
    [SerializeField] private Image m_TopBorder;
    [SerializeField] private Image m_BottomBorder;

    void Awake()
    {
        m_instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Phase = Phase.EnemyPhase;
        SwitchPhase();
    }

    public void AddHeroUnit(Hero unit)
    {
        m_PlayerUnits.Add(unit);
    }

    public void AddEnemyUnit(Enemy unit)
    {
        m_EnemyUnits.Add(unit);
    }

    public void DeleteHero(Hero unit)
    {
        int i = m_PlayerUnits.IndexOf(unit);
        m_PlayerUnits.RemoveAt(i);
    }

    public void DeleteEnemy(Enemy unit)
    {
        int i = m_EnemyUnits.IndexOf(unit);
        m_EnemyUnits.RemoveAt(i);
    }

    public void CheckPlayerUnits()
    {
        int i = 0;
        foreach(UnitBase unit in m_PlayerUnits)
        {
            if(unit.m_Hasmoved)
            {
                i++;
                if(i >= m_PlayerUnits.Count)
                {
                    SwitchPhase();
                }
            }
        }
    }

    public void SwitchPhase()
    {
        if(m_PlayerUnits.Count > 0 && m_EnemyUnits.Count > 0)
        {
            GridManager.m_instance.ToggleCursor(false);
            GridManager.m_instance.TileClickAllowed(false);
            GridManager.m_instance.ShowHighlightedTile(null);
            MenuManager.m_instance.ToggleEndButton(false);

            if(m_Phase == Phase.PlayerPhase)
            {
                // It is now Enemy Phase
                m_Phase = Phase.EnemyPhase;
                PhaseAnnouncement();

            }
            else
            {
                // It is now Player Phase
                m_Phase = Phase.PlayerPhase;
                PhaseAnnouncement();
            }
        }

    }

    void PhaseAnnouncement()
    {
        Color c;
        if(m_Phase == Phase.PlayerPhase)
        {
            c = Color.red;
            m_PhaseText.text = "PLAYER PHASE";
            SoundManager.m_instance.PlayAudio(SoundManager.m_instance.m_PlayerPhase);
        }
        else
        {
            c = Color.blue;
            m_PhaseText.text = "ENEMY PHASE";
            SoundManager.m_instance.PlayAudio(SoundManager.m_instance.m_EnemyPhase);
        }
        m_TopBorder.color = c;
        m_BottomBorder.color = c;
        m_PhaseText.outlineColor = c;

        Animator anim = m_PhaseHolder.GetComponent<Animator>();
        m_PhaseHolder.SetActive(true);
        anim.Play("PhaseChange");

        StartCoroutine("PhaseAnimation");
    }

    void SwitchPhaseLogic()
    {
        if(m_Phase == Phase.EnemyPhase)
        {
            // It is now Enemy Phase
            MoveNextEnemy(0);
            Debug.Log(m_EnemyUnits[0].gameObject.name);
        }
        else
        {
            // It is now Player Phase
            GridManager.m_instance.ToggleCursor(true);
            GridManager.m_instance.TileClickAllowed(true);
            MenuManager.m_instance.ToggleEndButton(true);
            GameManager.m_instance.ChangePlayerFunds(GameManager.m_instance.m_MoneyPerTurn);

            CameraManager.m_instance.SetCameraTarget(m_PlayerUnits[0].transform.position);
        }

        foreach(Hero hero in m_PlayerUnits)
        {
            hero.RefreshTurn();
        }

        foreach(Enemy enemy in m_EnemyUnits)
        {
            enemy.RefreshTurn();
        }
    }

    private IEnumerator PhaseAnimation()
    {
        yield return new WaitForSecondsRealtime(0.7f);
        m_PhaseHolder.SetActive(false);
        SwitchPhaseLogic();
    }

    public void MoveNextEnemy(int index)
    {
        if(index < m_EnemyUnits.Count)
        {
            Enemy enemy = m_EnemyUnits[index];

            enemy.RefreshTurn();

            enemy.FindNearestTarget();
            enemy.CalculatePath();
            enemy.FindSelectableTiles(enemy.m_Mov);

            //enemy.ShowSelectableTiles(GridManager.m_instance.m_MoveTileColor);

            m_CurrentlyMoving = enemy;
        }
        else
        {
            // The last enemy has already moved, so switch phase.
            m_CurrentlyMoving = null;
            SwitchPhase();
        }
    }
}
