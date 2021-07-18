using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager m_instance;
    public float m_MovSpeed;
    public float m_Threshold;
    public Vector2 m_Target; // The target the camera will be looking at.

    void Awake()
    {
        m_instance = this;
    }

    void FixedUpdate()
    {
        Move();
    }

    public void SetCameraTarget(Vector2 pos)
    {
        m_Target = new Vector2(pos.x - transform.position.x, pos.y - transform.position.y);
    }

    void Move()
    {
        if(Vector2.Distance(this.transform.position, m_Target) >= m_Threshold)
        {
            Vector3 target = new Vector3(m_Target.x, m_Target.y, this.transform.position.z);
            float speed = m_MovSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
        }

    }
}
