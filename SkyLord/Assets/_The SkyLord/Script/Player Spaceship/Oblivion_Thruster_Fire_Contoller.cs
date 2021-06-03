using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oblivion_Thruster_Fire_Contoller : MonoBehaviour
{
    [SerializeField] Oblivion_Main_Controller m_mainController;
    [SerializeField] private ParticleSystem m_thrusterFlameLeft, m_thrusterFlameRight;
    [SerializeField] private GameObject m_engineSmokeLeft, m_engineSmokeRight;
    private float m_activateThrusters, m_accelTime = 0f;

    void Awake()
    {
        m_activateThrusters = 1.5f;
        m_engineSmokeLeft.SetActive(false);
        m_engineSmokeRight.SetActive(false);
    }

    void Update()
    {
        if (m_mainController.m_inputController.m_accelerate)
        {
            EngineGas();
            m_accelTime += Time.deltaTime;
            if(m_accelTime >= m_activateThrusters)
            {
                StartThrusters();
                StopGas();
            }   
        }

        else
        {
            m_accelTime = 0f;
            StopGas();
            Invoke("DeactivateFlames", 0.5f);
        }
            
        if (m_mainController.m_inputController.m_brake)
        {
            m_accelTime = 0f;
            StopGas();
            Invoke("DeactivateFlames", 0f);
        } 
    }

    void EngineGas()
    {
        m_engineSmokeLeft.SetActive(true);
        m_engineSmokeRight.SetActive(true);
    }

    void StopGas()
    {
        m_engineSmokeLeft.SetActive(false);
        m_engineSmokeRight.SetActive(false);
    }

    void StartThrusters()
    {
        m_thrusterFlameLeft.Play();
        m_thrusterFlameRight.Play();
    }

    void DeactivateFlames()
    {
        m_thrusterFlameLeft.Stop();
        m_thrusterFlameRight.Stop();
    }
}
