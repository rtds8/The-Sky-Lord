using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oblivion_Thruster_Fire_Contoller : MonoBehaviour
{
    [SerializeField] Oblivion_Main_Controller m_mainController;
    [SerializeField] private ParticleSystem m_thrusterFlameLeft, m_thrusterFlameRight, m_warpEffect, m_warpBig;

    void Update()
    {
        if (m_mainController.m_inputController.m_accelerate)
        {
            m_thrusterFlameLeft.Play();
            m_thrusterFlameRight.Play();
            Invoke("WarpSmall", m_mainController.m_timeZeroToMax/2);
            Invoke("WarpBig", 0.5f);
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
        m_warpEffect.Stop();
        m_warpBig.Stop();
    }

    void WarpBig()
    {
        m_warpBig.Play();
    }

    void WarpSmall()
    {
        m_warpEffect.Play();
    }
}
