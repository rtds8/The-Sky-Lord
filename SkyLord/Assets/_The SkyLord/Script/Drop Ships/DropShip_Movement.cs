using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropShip_Movement : MonoBehaviour
{
    [SerializeField] internal Transform m_playeShipTransform;
    [SerializeField] private float m_movementSpeed = 10f, m_rotationDamp = 0.5f;
    internal float m_distanceToTarget;

    void Update()
    {
        m_distanceToTarget = Vector3.Distance(transform.position, m_playeShipTransform.position);
    }

    internal void TurnTowardsPlayer()
    {
        Vector3 position = m_playeShipTransform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(position, m_playeShipTransform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * m_rotationDamp);
    }

    internal void MoveTowardsPlayer()
    {
        transform.position += (transform.forward * Time.fixedDeltaTime * m_movementSpeed);
    }

    internal void MoveAwayFromPlayer()
    {
        transform.position -= (transform.forward * Time.fixedDeltaTime * (2f * m_movementSpeed));
    }
}
