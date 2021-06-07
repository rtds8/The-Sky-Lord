using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Oblivion_Main_Controller m_mainController;
    [SerializeField] private Camera m_FPP, m_TPP;
    public Camera _activeCam;
    [SerializeField] private float m_sensitivity;
    private Vector3 m_currentRotation;


    void Awake()
    {
        m_TPP.enabled = true;
        m_FPP.enabled = false;
        _activeCam = m_TPP;
    }

    void FixedUpdate()
    {
        if (m_TPP.enabled && m_mainController.m_inputController.m_cameraSteer)
            TPPCamera();

        if(m_mainController.m_inputController.m_switchCam)
        {
            m_FPP.enabled = !m_FPP.enabled;
            m_TPP.enabled = !m_TPP.enabled;
        }

        if (m_FPP.enabled && m_mainController.m_inputController.m_cameraSteer)
            FPPCamera();
    }

    void LateUpdate()
    {
        if (!m_mainController.m_inputController.m_cameraSteer)
        {
            CalculateRotation(m_TPP, 0, 0, 0, 0);
            CalculateRotation(m_FPP, 0, 0, 0, 0);
        }

        if (m_FPP.enabled)
            _activeCam = m_FPP;

        else
            _activeCam = m_TPP;
    }
    public void TPPCamera()
    {
        CalculateRotation(m_TPP, 15, 15, 15, 15);
    }

    public void FPPCamera()
    {
        CalculateRotation(m_FPP, 45, 45, 10, 20);
    }

    void CalculateRotation(Camera camera, float neg_x, float pos_x, float neg_y, float pos_y)
    {
        m_currentRotation.x += m_mainController.m_inputController.m_yawValue * m_sensitivity;
        m_currentRotation.y -= m_mainController.m_inputController.m_pitchValue * m_sensitivity;
        m_currentRotation.x = Mathf.Clamp(m_currentRotation.x, -pos_x, neg_x);
        m_currentRotation.y = Mathf.Clamp(m_currentRotation.y, -pos_y, neg_y);
        camera.transform.localRotation = Quaternion.Euler(m_currentRotation.y, m_currentRotation.x, 0);
    }
}
