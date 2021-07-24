using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager m_instance;
    public float m_TravelTime;
    public Vector2 followOffset;
    public Vector2 threshold;
    public Vector2 m_Target; // The target the camera will be looking at.
    private bool m_LockCam;

    void Awake()
    {
        m_instance = this;
        threshold = CalculateThreshold();
    }

    void FixedUpdate()
    {
        if(!m_LockCam)
        {
            Move();
        }
    }

    void Move()
    {
        int xDiff = Mathf.FloorToInt(Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * m_Target.x));
        int yDiff = Mathf.FloorToInt(Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * m_Target.y));

        Vector3 newPos = transform.position;
        if(Mathf.Abs(xDiff) >= threshold.x)
        {
            newPos.x = m_Target.x;
        }
        if(Mathf.Abs(yDiff) >= threshold.y)
        {
            newPos.y = m_Target.y;
        }

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime / m_TravelTime);

    }

    public void SetCameraTarget(Vector2 pos)
    {
        m_Target = pos;
    }

    private Vector3 CalculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Mathf.FloorToInt(Camera.main.orthographicSize * aspect.width / aspect.height), Mathf.FloorToInt(Camera.main.orthographicSize));
        t.x -= followOffset.x;
        t.y -= followOffset.y;

        return t;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }

    public void LockCamera(bool state)
    {
        m_LockCam = state;
    }
}
