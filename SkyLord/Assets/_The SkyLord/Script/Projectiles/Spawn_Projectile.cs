using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Projectile : MonoBehaviour
{
    [SerializeField] Oblivion_Main_Controller m_mainController;
    [SerializeField] private GameObject m_firePoint;
    [SerializeField] private List<GameObject> m_projectiles = new List<GameObject>();

    private GameObject m_toSpawn;
    private float m_recoilTime = 0f;

    void Start()
    {
        m_toSpawn = m_projectiles[0];
    }

    void Update()
    {
        if (m_mainController.m_inputController.m_doFire && Time.time >= m_recoilTime)
        {
            m_recoilTime = Time.time + 1 / m_toSpawn.GetComponent<Move_Projectile>()._fireRate;   
            spawnProjectile();
        }
    }

    void spawnProjectile()
    {
        GameObject projectile;

        if(m_firePoint != null)
        {
            projectile = Instantiate(m_toSpawn, m_firePoint.transform.position, m_firePoint.transform.rotation);
        }

        else
        {
            Debug.Log("Fire Point Missing!!!!!");
        }
    }
}
