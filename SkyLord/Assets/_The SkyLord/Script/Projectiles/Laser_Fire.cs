using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Fire : MonoBehaviour
{
    [SerializeField] private Transform m_playerShip;
    [SerializeField] internal LineRenderer m_laserBeam;
    [SerializeField] private float m_stopFireTime = 1f, m_fireDelay = 2f, m_maxDistance = 300f, m_viewAngleLowerBound = 157f, m_viewAngleMax = 202f;
    internal bool m_canFire;
    Vector3 m_targetPos;

    public void FireLaser()
    {
        if(m_canFire)
        {
            m_laserBeam.SetPosition(0, m_laserBeam.transform.position);
            m_laserBeam.SetPosition(1, m_targetPos);
            m_laserBeam.enabled = true;
            m_canFire = false;
            Invoke("StopFire", m_stopFireTime);
            Invoke("CanFire", m_fireDelay);
        }
    }

    internal bool InFront()
    {
        Vector3 direction = m_laserBeam.transform.position - m_playerShip.position;
        float angle = Vector3.Angle(transform.forward, direction);

        if (Mathf.Abs(angle) > m_viewAngleLowerBound && Mathf.Abs(angle) < m_viewAngleMax)
            return true;

        return false;
    }

    internal bool InLineOfSight()
    {
        RaycastHit hit;
        Vector3 direction = m_playerShip.position - m_laserBeam.transform.position;

        if(Physics.Raycast(m_laserBeam.transform.position, direction, out hit, m_maxDistance))
        {
            if (hit.transform.CompareTag("The Oblivion"))
            {
                m_targetPos = hit.transform.position;
                DamagePlayer();
                return true;
            }
        }

        m_targetPos = m_laserBeam.transform.position; 
        return false;
    }

    void DamagePlayer()
    {
        m_playerShip.GetComponent<Oblivion_Damage_And_Health>().m_damageAmount = 0.5f;
        m_playerShip.GetComponent<Oblivion_Damage_And_Health>().m_takenDamage = true;
        m_playerShip.GetComponent<Oblivion_Damage_And_Health>().m_isVulnerable = true;
        m_playerShip.GetComponent<Oblivion_Damage_And_Health>().DoDamage();
    }

    void StopFire()
    {
        m_laserBeam.enabled = false;
    }

    void CanFire()
    {
        m_canFire = true;
    }
}
