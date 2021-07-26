using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager m_instance;
    public GameObject m_Source;

    [Header("System Sounds")]
    public AudioClip m_Cursor;
    public AudioClip m_Confirm;
    public AudioClip m_Cancel;

    [Header("Unit Sounds")]
    public AudioClip m_UnitShoot;
    public AudioClip m_UnitDamage;
    public AudioClip m_UnitDeath;
    public AudioClip m_CaptureSound;
    public AudioClip m_PowerUp;
    public AudioClip m_Steam;

    [Header("Phase Sounds")]
    public AudioClip m_PlayerPhase;
    public AudioClip m_EnemyPhase;

    void Awake()
    {
        m_instance = this;
    }

    public void PlayAudio(AudioClip audioClip)
    {
        if(m_Source != null)
        {
            GameObject go = Instantiate(m_Source, this.transform.position, Quaternion.identity) as GameObject;
            AudioSource aus = go.GetComponent<AudioSource>();
            aus.clip = audioClip;
            aus.Play();
            Destroy(go, aus.clip.length);
        }
    }
}
