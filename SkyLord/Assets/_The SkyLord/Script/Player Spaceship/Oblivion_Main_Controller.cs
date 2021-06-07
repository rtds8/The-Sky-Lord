using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oblivion_Main_Controller : MonoBehaviour
{
    [SerializeField] internal float m_maxSpeed = 100f, m_timeZeroToMax = 4f, m_timeMaxToZero = 2f, m_timeBrakeToZero = 1f, m_rotationSpeed = 15f, m_health = 500f, m_healTime = 20f;
    [SerializeField] internal Oblivion_Input_Controller m_inputController;
    [SerializeField] internal Oblivion_Movement_Controller m_movementController;
    [SerializeField] internal Oblivion_Thruster_Fire_Contoller m_thrusterController;
    [SerializeField] internal Oblivion_Damage_And_Health m_damageAndHealth;
 
    internal Rigidbody myRigidBody;
    internal Transform myTransform;
    
    void Awake()
    {
        myRigidBody = this.GetComponent<Rigidbody>();
        myRigidBody.centerOfMass = new Vector3(0, 0, 0);
        myTransform = this.transform;
    }
}
