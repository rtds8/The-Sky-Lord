using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Behaviour : MonoBehaviour
{
    [SerializeField] private float m_speed = 100f;
    private Vector3 m_startPoint;
    private void OnEnable()
    {
        m_startPoint = this.transform.position;
    }

    void Update()
    {
        if (m_speed != 0)
            this.transform.position += this.transform.forward * (m_speed * Time.deltaTime);

        if (Vector3.Distance(m_startPoint, this.transform.position) >= Mathf.Abs(300f) && this.gameObject.activeInHierarchy)
            DeactivateBullet();

    }
    private void DeactivateBullet()
    {
        this.gameObject.SetActive(false);
    }
}
