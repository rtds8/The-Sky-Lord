using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oblivion_Input_Controller : MonoBehaviour
{
    [SerializeField] Oblivion_Main_Controller m_mainController;
    internal bool m_accelerate = false, m_brake = false, m_decelerate = false, m_cameraSteer = false, m_switchCam = false;
    internal float m_yawValue = 0f, m_pitchValue = 0f, m_rollValue = 0f;

    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        if (Input.GetAxis("Vertical") > 0)
            m_accelerate = true;

        else
            m_accelerate = false;

        if (Input.GetAxis("Vertical") < 0)
            m_brake = true;

        else
            m_brake = false;

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

        bool isMouseMove = Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;

        if (!Input.anyKey && !isMouseMove)
            m_decelerate = true;

        else
            m_decelerate = false;

        if (Input.GetMouseButton(1))
            m_cameraSteer = true;

        if(Input.GetMouseButtonUp(1))
            m_cameraSteer = false;

        if (Input.GetKeyDown(KeyCode.C))
            m_switchCam = true;

        else
            m_switchCam = false;
    }
}
