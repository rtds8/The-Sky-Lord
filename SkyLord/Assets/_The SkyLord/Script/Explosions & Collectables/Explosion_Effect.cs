using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Effect : MonoBehaviour
{
    [SerializeField] private GameObject m_explosionPrefab;
    [SerializeField] private float m_impactRadius = 100f, m_impactForce = 300f;
    internal bool m_hasExploded;

    private void Start()
    {
        m_hasExploded = false;
    }

    public void Explode()
    {
        if (!m_hasExploded)
        {
            Instantiate(m_explosionPrefab, transform.position, transform.rotation);
            m_hasExploded = true;
        }
    }

    public void AddNearbyForce()
    {
        Collider[] nearbyColliers = Physics.OverlapSphere(transform.position, m_impactRadius);

        foreach(Collider nearbyObjects in nearbyColliers)
        {
            Rigidbody rb = nearbyObjects.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(m_impactForce, transform.position, m_impactRadius);
            }
        }
    }
}
