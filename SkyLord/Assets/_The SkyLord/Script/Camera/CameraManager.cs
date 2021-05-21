using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Oblivion_Main_Controller m_mainController;
    public Camera _FPP;
    public Camera _TPP;
    [SerializeField] private float m_sensitivity;
    private Vector3 m_currentRotation;

    void Start()
    {
        _TPP.enabled = true;
        _FPP.enabled = false;
    }

    void FixedUpdate()
    {
        if (_TPP.enabled && m_mainController.m_inputController.m_cameraSteer)
            TPPCamera();

        if(m_mainController.m_inputController.m_switchCam)
        {
            _FPP.enabled = !_FPP.enabled;
            _TPP.enabled = !_TPP.enabled;
        }

        if (_FPP.enabled && m_mainController.m_inputController.m_cameraSteer)
            FPPCamera();
    }

    void LateUpdate()
    {
        if (!m_mainController.m_inputController.m_cameraSteer)
        {
            CalculateRotation(_TPP, 0, 0, 0, 0);
            CalculateRotation(_FPP, 0, 0, 0, 0);
        }
    }
    public void TPPCamera()
    {
        CalculateRotation(_TPP, 15, 15, 15, 15);
    }

    public void FPPCamera()
    {
        CalculateRotation(_FPP, 45, 45, 10, 20);
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
