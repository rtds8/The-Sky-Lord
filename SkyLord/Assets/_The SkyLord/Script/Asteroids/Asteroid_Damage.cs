using UnityEngine;

public class Asteroid_Damage : MonoBehaviour
{
    [SerializeField] private float m_health;
    private float m_currentHealth, m_damageAmount;

    private void Awake()
    {
        m_currentHealth = m_health;
        m_damageAmount = 25f;
    }

    private void Update()
    {
        if (m_currentHealth <= 0)
            RemoveAsteroid();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Bullet"))
        {
            m_currentHealth -= m_damageAmount;
        }      
    }

    private void RemoveAsteroid()
    {
        this.gameObject.SetActive(false);
    }
}
