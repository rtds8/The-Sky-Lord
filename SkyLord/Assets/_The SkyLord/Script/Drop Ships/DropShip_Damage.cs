using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropShip_Damage : MonoBehaviour
{
    [SerializeField] private Canvas m_healthBar;
    [SerializeField] private float m_health = 100f, m_healRate = 10f;
    private float m_currentHealth = 0f;
    private bool m_gotHit, m_vulnerable;
    private void OnEnable()
    {
        m_gotHit = false;
        m_vulnerable = true;
        m_currentHealth = m_health;
    }

    void Start()
    {
        m_healthBar.GetComponentInChildren<Health_Bar>().SetMaxHealth(m_health);
        StartCoroutine(RestoreHealth());
    }

    void Update()
    {
        if (m_currentHealth <= 0f)
            this.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Bullet") && m_vulnerable)
        {
            m_gotHit = true;
            m_vulnerable = false;
        }

        ReduceHealth();
    }

    void ReduceHealth()
    {
        var damageAmount = 0f;

        if (m_gotHit)
        {
            if (m_currentHealth > 50f)
                damageAmount = Mathf.Round(m_currentHealth * 0.05f);
            else
                damageAmount = 2.5f;

            m_healthBar.GetComponentInChildren<Health_Bar>().DecrementHealth(damageAmount);
        }

        m_currentHealth -= damageAmount;
        m_gotHit = false;
        m_vulnerable = true;
    }

    private IEnumerator RestoreHealth()
    {
        yield return new WaitForSeconds(m_healRate);

        m_vulnerable = false;

        var amount = 0f;

        if (m_currentHealth < 90f)
        {
            if (m_currentHealth > 50f)
                amount = Mathf.Round(m_currentHealth * 0.1f);

            else
                amount = 10f;
            m_healthBar.GetComponentInChildren<Health_Bar>().IncrementHealth(amount);
        }

        m_currentHealth += amount;
        m_vulnerable = true;

        StartCoroutine(RestoreHealth());
    }
}
