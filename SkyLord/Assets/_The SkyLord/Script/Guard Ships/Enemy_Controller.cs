using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    [SerializeField] Enemy_Manager m_enemyManager;
    [SerializeField] private float m_moveSpeed = 100f, m_dodgeDistance = 100f, m_health = 100f, m_healRate = 10f;
    [SerializeField] internal float m_minDistanceForFire, m_recoilTime = 2f;
    [SerializeField] private Vector3 m_offsetDistance;
    [SerializeField] private Canvas m_healthBar;
    [Range(0.1f,0.5f)]
    [SerializeField] private float m_dodgeStrength;
    internal bool m_doFire;
    private float m_currentHealth = 0f;
    private bool m_gotHit, m_vulnerable;

    private void OnEnable()
    {
        m_doFire = false;
        m_gotHit = false;
        m_vulnerable = true;
        m_currentHealth = m_health;
        StartCoroutine(MoveToPlayer());
        InvokeRepeating("Dodge", 5f, 0.2f);
    }

    void Start()
    {
        m_healthBar.GetComponentInChildren<Health_Bar>().SetMaxHealth(m_health);
        StartCoroutine(RestoreHealth());
    }

    void FixedUpdate()
    {
        this.transform.LookAt(m_enemyManager.m_playerTransform, m_enemyManager.m_playerTransform.up);
    }

    void Update()
    {
        if (m_currentHealth <= 0f)
        {
            gameObject.GetComponent<Explosion_Effect>().Explode();
            m_enemyManager.m_playerTransform.gameObject.GetComponent<Oblivion_Damage_And_Health>().m_playerUI.GetComponentInChildren<Increment_Count_Text>().IncrementCount();
            m_enemyManager.RemoveShip(this.gameObject);
        }
    }

    private IEnumerator MoveToPlayer()
    {
        yield return new WaitForEndOfFrame();

        var currentDistance = Vector3.Distance(this.transform.position, m_enemyManager.m_playerTransform.position);

        if (currentDistance > m_offsetDistance.magnitude)
        {
            m_doFire = false;
            transform.position += transform.forward * m_moveSpeed * Time.deltaTime;
        }

        else
        {
            m_doFire = true;
        }

        StartCoroutine(MoveToPlayer());
    }

    private void Dodge()
    {
        if (Vector3.Distance(this.transform.position, m_enemyManager.m_playerTransform.position) <= m_dodgeDistance)
        {
            m_doFire = false;

            var dodgeValue = m_enemyManager.m_playerTransform.position + new Vector3(Random.Range(transform.position.x - 50, transform.position.x + 50),
                                                Random.Range(transform.position.y - 30, transform.position.y + 30),
                                                Random.Range(transform.position.z - 15, transform.position.z));

            transform.position = Vector3.Lerp(transform.position, dodgeValue, m_dodgeStrength);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player Bullet") && m_vulnerable)
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

        if(m_currentHealth < 90f)
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

    private void OnDisable()
    {
        CancelInvoke("Dodge");
        StopAllCoroutines();
    }
}
