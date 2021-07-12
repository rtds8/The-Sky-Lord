using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oblivion_Damage_And_Health : MonoBehaviour
{
    [SerializeField] Oblivion_Main_Controller m_mainController;
    [SerializeField] internal Canvas m_playerUI;
    internal bool m_isVulnerable, m_takenDamage;
    internal float m_currentHealth, m_damageAmount = 10f, m_energyReduceAmount = 10f;

    private void OnEnable()
    {
        m_currentHealth = m_mainController.m_health;
        m_takenDamage = false;
        m_isVulnerable = true;
    }

    void Start()
    {
        m_playerUI.GetComponentInChildren<Health_Bar>().SetMaxHealth(m_mainController.m_health);
        m_playerUI.GetComponentInChildren<Energy_Bar>().SetMaxEnergy(m_mainController.m_maxEnergy);
        StartCoroutine(Rejuvinate());
    }

    void Update()
    {
        if(m_mainController.m_inputController.m_accelerate)
            m_playerUI.GetComponentInChildren<Energy_Bar>().ReduceEnergy(m_energyReduceAmount * 15f * Time.deltaTime);

        if(m_mainController.m_inputController.m_strafeValue != 0)
            m_playerUI.GetComponentInChildren<Energy_Bar>().ReduceEnergy(m_energyReduceAmount * 10f * Time.deltaTime);

        if (m_mainController.m_inputController.m_doFire)
            m_playerUI.GetComponentInChildren<Energy_Bar>().ReduceEnergy(m_energyReduceAmount * 20f * Time.deltaTime);

        m_playerUI.GetComponentInChildren<Energy_Bar>().ReduceEnergy(m_energyReduceAmount * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy Bullet") && m_isVulnerable && !(this.gameObject.CompareTag("Player Bullet")))
        {
            m_takenDamage = true;
            m_isVulnerable = false;
        }

        DoDamage();

        if(other.gameObject.CompareTag("Collectable"))
            m_playerUI.GetComponentInChildren<Energy_Bar>().ReduceEnergy(-m_mainController.m_energyIncrement);
    }

    internal void DoDamage()
    {
        if (m_takenDamage)
        {/*
            if (m_currentHealth > 50f)
                damageAmount = Mathf.Round(m_currentHealth * 0.2f);
            else
                damageAmount = 2.5f;
*/
            m_playerUI.GetComponentInChildren<Health_Bar>().DecrementHealth(m_damageAmount);
        }

        m_currentHealth -= m_damageAmount;
        m_takenDamage = false;
        m_isVulnerable = true;
    }

    private IEnumerator Rejuvinate()
    {
        yield return new WaitForSeconds(m_mainController.m_healTime);

        m_isVulnerable = false;

        var amount = 50f;

        if (m_currentHealth < m_mainController.m_health - amount)
        {/*
            if (m_currentHealth > 50f)
                amount = Mathf.Round(m_currentHealth * 0.2f);

            else
                amount = 10f;*/
            m_playerUI.GetComponentInChildren<Health_Bar>().IncrementHealth(amount);
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
