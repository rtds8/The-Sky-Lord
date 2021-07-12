using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip_Manager : MonoBehaviour
{
    [SerializeField] internal Transform m_playerShip;
    [SerializeField] internal float m_motherShipSpeed = 10f, m_health = 500f, m_healRate = 10f;
    [SerializeField] internal int m_shieldEnergy = 5;
    private float m_currentHealth = 0f;
    [SerializeField] List<Vector3> m_jumpLocations = new List<Vector3>();
    [SerializeField] List<Quaternion> m_Rotations = new List<Quaternion>();
    [SerializeField] List<GameObject> m_dropShips = new List<GameObject>();
    [SerializeField] private Canvas m_motherShipUI;
    private bool m_gotHit, m_vulnerable;
    private int m_dropShipCount;
    [SerializeField] GameObject m_explosion;

    private void OnEnable()
    {
        m_currentHealth = m_health;
    }

    void Awake()
    {
        m_dropShipCount = 0;
        m_gotHit = false;
        m_vulnerable = false;

        foreach (GameObject ship in m_dropShips)
            ship.SetActive(false);
    }

    void Start()
    {
        m_motherShipUI.GetComponentInChildren<Health_Bar>().SetMaxHealth(m_health);
        m_motherShipUI.GetComponentInChildren<Shield_Bar>().SetMaxEnergy(m_shieldEnergy);
    }

    private void Update()
    {
        if(m_playerShip.GetComponent<Oblivion_Damage_And_Health>().m_playerUI.GetComponentInChildren<Increment_Count_Text>().count > m_dropShipCount &&
            m_playerShip.GetComponent<Oblivion_Damage_And_Health>().m_playerUI.GetComponentInChildren<Increment_Count_Text>().count < m_shieldEnergy)
        {
            m_motherShipUI.GetComponentInChildren<Shield_Bar>().ReduceShieldHealth(1);
            m_dropShipCount = m_playerShip.GetComponent<Oblivion_Damage_And_Health>().m_playerUI.GetComponentInChildren<Increment_Count_Text>().count;
        }

        if (m_playerShip.GetComponent<Oblivion_Damage_And_Health>().m_playerUI.GetComponentInChildren<Increment_Count_Text>().count >= 5)
        {
            m_motherShipUI.GetComponentInChildren<Shield_Bar>().ReduceShieldHealth(1);
            gameObject.GetComponent<Animator>().SetBool("Has Powered Up", true);
            m_vulnerable = true;
        }
    }

    internal GameObject GetDropShip()
    {
        GameObject dropShip = m_dropShips[0];
        m_dropShips.RemoveAt(0);
        return dropShip;
    }

    internal Vector3 GetNextLocation()
    {
        Vector3 nextPos = m_jumpLocations[0];
        m_jumpLocations.RemoveAt(0);
        return nextPos;
    }

    internal Quaternion GetNextOrientation()
    {
        Quaternion nextOrientation = m_Rotations[0];
        m_Rotations.RemoveAt(0);
        return nextOrientation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Bullet") && m_vulnerable)
        {
            Instantiate(m_explosion, other.transform.position, m_explosion.transform.rotation);
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
            if (m_currentHealth > .1 * m_health)
                damageAmount = Mathf.Round(m_currentHealth * 0.01f);
            else
                damageAmount = 10f;

            m_motherShipUI.GetComponentInChildren<Health_Bar>().DecrementHealth(damageAmount);
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

        if (m_currentHealth < .9f * m_health)
        {
            if (m_currentHealth > .5 * m_health)
                amount = Mathf.Round(m_health * 0.5f);

            else
                amount = 50f;
            m_motherShipUI.GetComponentInChildren<Health_Bar>().IncrementHealth(amount);
        }

        m_currentHealth += amount;
        m_vulnerable = true;

        StartCoroutine(RestoreHealth());
    }
}
