using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArrowScreenIndicator : MonoBehaviour
{
    public Transform m_target;
    public Vector2 m_camBorderOffset;

    bool m_canUpdate = false;

    Vector3 m_camPos;
    Vector3 m_targetPos;
    Vector3 m_dir;
    float m_angle;

    Vector3 m_targetScreenSpacePos;
    bool m_isOffScreen;
    Vector3 m_cappedTargetScreenSpacePos;
    Vector3 m_targetWorldSpacePos;
    public void Start()
    {
        m_canUpdate = true;
        m_targetPos = m_target.position;
    }

    void Update()
    {
        if (m_canUpdate)
        {

            m_camPos = Camera.main.transform.position;
            m_dir = (m_targetPos - m_camPos).normalized;
            //m_angle = Angle(m_dir);
            //transform.rotation = Quaternion.AngleAxis(m_angle, Vector3.forward);

            PointTarget();

            m_targetScreenSpacePos = Camera.main.WorldToScreenPoint(m_targetPos);

            m_isOffScreen = (m_targetScreenSpacePos.x <= m_camBorderOffset.x || m_targetScreenSpacePos.x >= Screen.width - m_camBorderOffset.x ||
                m_targetScreenSpacePos.y <= m_camBorderOffset.y || m_targetScreenSpacePos.y >= Screen.height - m_camBorderOffset.y);

            m_cappedTargetScreenSpacePos = m_targetScreenSpacePos;
            if (m_isOffScreen)
            {
                if (m_cappedTargetScreenSpacePos.x <= m_camBorderOffset.x) m_cappedTargetScreenSpacePos.x = m_camBorderOffset.x;
                if (m_cappedTargetScreenSpacePos.x >= Screen.width - m_camBorderOffset.x) m_cappedTargetScreenSpacePos.x = Screen.width - m_camBorderOffset.x;
                if (m_cappedTargetScreenSpacePos.y <= m_camBorderOffset.y) m_cappedTargetScreenSpacePos.y = m_camBorderOffset.y;
                if (m_cappedTargetScreenSpacePos.y >= Screen.height - m_camBorderOffset.y) m_cappedTargetScreenSpacePos.y = Screen.height - m_camBorderOffset.y;
            }
            transform.position = m_cappedTargetScreenSpacePos;
        }
    }

    public float Angle(Vector2 v)
    {
        float angle = (float)Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360;
        }

        angle = (angle >= 90.0f) ? angle - 90.0f : 270.0f + angle;

        return angle;
    }

    void PointTarget()
    {
        //Rotate the sprite towards the target object
        var targetPosLocal = Camera.main.transform.InverseTransformPoint(m_targetPos);
        var targetAngle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg;
        //Apply rotation
        transform.eulerAngles = new Vector3(0, 0, targetAngle);
    }
}