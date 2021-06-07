using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oblivion_Damage_And_Health : MonoBehaviour
{
    [SerializeField] Oblivion_Main_Controller m_mainController;
    [SerializeField] private Canvas m_healthBar;
    private bool m_isVulnerable, m_takenDamage;
    private float m_currentHealth;

    private void OnEnable()
    {
        m_currentHealth = m_mainController.m_health;
        m_takenDamage = false;
        m_isVulnerable = true;
    }

    void Start()
    {
        m_healthBar.GetComponentInChildren<Health_Bar>().SetMaxHealth(m_mainController.m_health);
        StartCoroutine(Rejuvinate());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy Bullet" && m_isVulnerable && this.gameObject.tag != "Player Bullet")
        {
            m_takenDamage = true;
            m_isVulnerable = false;
        }

        DoDamage();
    }

    private void DoDamage()
    {
        var damageAmount = 10f;

        if (m_takenDamage)
        {/*
            if (m_currentHealth > 50f)
                damageAmount = Mathf.Round(m_currentHealth * 0.2f);
            else
                damageAmount = 2.5f;
*/
            m_healthBar.GetComponentInChildren<Health_Bar>().DecrementHealth(damageAmount);
        }

        m_currentHealth -= damageAmount;
        m_takenDamage = false;
        m_isVulnerable = true;
    }

    private IEnumerator Rejuvinate()
    {
        yield return new WaitForSeconds(m_mainController.m_healTime);

        m_isVulnerable = false;

        var amount = 50f;

        if (m_currentHealth < 450f)
        {/*
            if (m_currentHealth > 50f)
                amount = Mathf.Round(m_currentHealth * 0.2f);

            else
                amount = 10f;*/
            m_healthBar.GetComponentInChildren<Health_Bar>().IncrementHealth(amount);
        }

        m_currentHealth += amount;
        m_isVulnerable = true;

        StartCoroutine(Rejuvinate());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
