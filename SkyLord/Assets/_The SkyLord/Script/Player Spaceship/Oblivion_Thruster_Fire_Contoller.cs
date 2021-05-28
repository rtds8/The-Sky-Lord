using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oblivion_Thruster_Fire_Contoller : MonoBehaviour
{
    [SerializeField] Oblivion_Main_Controller m_mainController;
    [SerializeField] private ParticleSystem m_thrusterFlameLeft, m_thrusterFlameRight;

    void Update()
    {
        if (m_mainController.m_inputController.m_accelerate)
        {
            m_thrusterFlameLeft.Play();
            m_thrusterFlameRight.Play();
        }

        else
            Invoke("DeactivateFlames", m_mainController.m_timeMaxToZero/2);

        if (m_mainController.m_inputController.m_brake)
            Invoke("DeactivateFlames", m_mainController.m_timeBrakeToZero/2);
    }

    void DeactivateFlames()
    {
        m_thrusterFlameLeft.Stop();
        m_thrusterFlameRight.Stop();
    }
}
