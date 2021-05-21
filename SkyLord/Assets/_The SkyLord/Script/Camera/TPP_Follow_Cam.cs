using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPP_Follow_Cam : MonoBehaviour
{
    [SerializeField] CameraManager m_cameraManager;
    [SerializeField] Oblivion_Main_Controller m_mainController;
    [SerializeField] private Transform m_playerShip;
    [SerializeField] private float m_distanceDamp;
    Vector3 m_defaultDistance, m_velocity = Vector3.one;
    Transform m_myTransform;

    void Start()
    {
        m_myTransform = transform;
        m_defaultDistance = m_myTransform.position;
    }

    void FixedUpdate()
    {
        Follow();

       /* if (!m_mainController.m_inputController.m_cameraSteer)
        {
            //m_cameraManager.CalculateRotation(m_cameraManager._TPP, 0, 0, 0, 0);
            m_myTransform.position = m_playerShip.position + (m_playerShip.rotation * m_defaultDistance);
            m_myTransform.LookAt(m_playerShip, m_playerShip.up);
        }*/
    }

    void Follow()
    {
        Vector3 targetPos = m_playerShip.position + (m_playerShip.rotation * m_defaultDistance);
        m_myTransform.position = Vector3.SmoothDamp(m_myTransform.position, targetPos, ref m_velocity, m_distanceDamp);
        m_myTransform.LookAt(m_playerShip, m_playerShip.up);

        /*if (m_mainController.m_inputController.m_cameraSteer)
        {
            m_myTransform.position = m_playerShip.position + (m_playerShip.rotation * m_defaultDistance);
            m_cameraManager.CalculateRotation(m_cameraManager._TPP, 15, 15, 15, 15);
        }   */
    }
}
