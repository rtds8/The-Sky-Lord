using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    [SerializeField] private float m_speed = 100f;
    [SerializeField] private GameObject m_hitEffect;
    private Vector3 m_startPoint; 
    bool m_target;

    private void OnEnable()
    {
        m_startPoint = this.transform.position;
        gameObject.tag = "Enemy Bullet";
    }

    private void Start()
    {
        m_target = transform.GetComponentInParent<Enemy_Fire>().AimAtPlayer();
    }

    void FixedUpdate()
    {
        if (m_speed != 0 && m_target)
        {
            this.transform.position += this.transform.forward * (m_speed * Time.deltaTime);
        }

        if ((Vector3.Distance(m_startPoint, this.transform.position) >= 500f || !m_target) && this.gameObject.activeInHierarchy)
            DeactivateBullet();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Guard Ship"))
        {
            Instantiate(m_hitEffect, other.transform.position, other.transform.rotation);
            DeactivateBullet();
        }
    }

    private void DeactivateBullet()
    {
        this.gameObject.SetActive(false);
    }
}
