using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oblivion_Input_Controller : MonoBehaviour
{
    [SerializeField] Oblivion_Main_Controller m_mainController;
    [SerializeField] private GameObject m_radarPoint;
    internal bool m_accelerate = false, m_brake = false, m_decelerate = false, m_cameraSteer = false, m_switchCam = false, m_doFire = false;
    internal float m_strafeValue = 0f, m_yawValue = 0f, m_pitchValue = 0f, m_rollValue = 0f;

    private void Awake()
    {
        m_radarPoint.SetActive(true);
        LockCursor();
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;

        if (Input.GetAxis("Vertical") > 0)
            m_accelerate = true;

        else
            m_accelerate = false;

        if (Input.GetAxis("Vertical") < 0)
            m_brake = true;

        else
            m_brake = false;

        if (Input.GetAxis("Horizontal") != 0)
            m_strafeValue = Input.GetAxis("Horizontal");

        else
            m_strafeValue = 0f;

        if (Input.GetAxis("Mouse X") != 0)
            m_yawValue = Input.GetAxis("Mouse X");

        else
            m_yawValue = 0f;

        if (Input.GetAxis("Mouse Y") != 0)
            m_pitchValue = Input.GetAxis("Mouse Y");

        else
            m_pitchValue = 0f;

        if (Input.GetAxis("Roll") != 0)
            m_rollValue = Input.GetAxis("Roll");

        else
            m_rollValue = 0f;

        if (Input.GetMouseButtonDown(1))
        {
            LockCursor();
            m_cameraSteer = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            UnlockCursor();
            m_cameraSteer = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
            m_switchCam = true;

        else
            m_switchCam = false;

        if (Input.GetMouseButtonDown(0))
        {
            LockCursor();
            m_doFire = true;
        }

        else
            m_doFire = false;

        bool isMouseMove = Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;

        if (!Input.anyKey && !isMouseMove)
            m_decelerate = true;

        else
            m_decelerate = false;
    }

    void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
