using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    [SerializeField] private float m_speed = 100f;
    private Vector3 m_startPoint;

    private void OnEnable()
    {
        m_startPoint = this.transform.position;
        gameObject.tag = "Enemy Bullet";
    }

    void Update()
    {
        if (m_speed != 0)
            this.transform.position += this.transform.forward * (m_speed * Time.deltaTime);

        if (Vector3.Distance(m_startPoint, this.transform.position) >= 500f && this.gameObject.activeInHierarchy)
            DeactivateBullet();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Guard Ship"))
        {
            DeactivateBullet();
        }
    }

    private void DeactivateBullet()
    {
        this.gameObject.SetActive(false);
    }
}
