using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oblivion_Movement_Controller : MonoBehaviour
{   
    [SerializeField] Oblivion_Main_Controller m_mainController;
    
    private float m_accelerationPerSec, m_decelerationPerSec, m_brakingPerSec, m_forwardVelocity;

    void Start()
    {
        m_accelerationPerSec = m_mainController.m_maxSpeed/ m_mainController.m_timeZeroToMax;
        m_decelerationPerSec = -m_mainController.m_maxSpeed / m_mainController.m_timeMaxToZero;
        m_brakingPerSec = -m_mainController.m_maxSpeed / m_mainController.m_timeBrakeToZero;
        m_forwardVelocity = 0f;
    }

    void FixedUpdate()
    {
        MakeRotation();
        MakeMovement();
    }


    void MakeRotation()
    {
        m_mainController.myTransform.Rotate(-m_mainController.m_inputController.m_pitchValue, 
                                             m_mainController.m_inputController.m_yawValue, 
                                            -m_mainController.m_inputController.m_rollValue);
    }

    void MakeMovement()
    {
        if(m_mainController.m_inputController.m_accelerate)
        {
            Acceleration(m_accelerationPerSec);
            ApplyAcceleration();
        }

        if(m_mainController.m_inputController.m_strafeValue != 0)
        {
            m_mainController.transform.Translate(transform.right * m_mainController.m_inputController.m_strafeValue * (m_mainController.m_maxSpeed/10) * Time.deltaTime);
        }

        if (m_mainController.m_inputController.m_brake)
        {
            Acceleration(m_brakingPerSec);
            ApplyAcceleration();
        }

        if(m_mainController.m_inputController.m_decelerate)
        {
            Acceleration(m_decelerationPerSec);
            ApplyAcceleration();
        }
    }

    void Acceleration(float accelerate)
    {
        m_forwardVelocity += accelerate * Time.deltaTime;
        m_forwardVelocity = Mathf.Clamp(m_forwardVelocity, 0, m_mainController.m_maxSpeed);
    }

    void ApplyAcceleration()
    {
        m_mainController.myRigidBody.velocity = transform.forward * m_forwardVelocity;
    }
}
