using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fire : MonoBehaviour
{
    [SerializeField] Enemy_Manager m_enemyManager;
    [SerializeField] Enemy_Controller m_enemyController;
    [SerializeField] private GameObject m_enemyProjectile;
    [SerializeField] private int m_Projectile_Quantity = 4;
    private float m_elapsedTIme = 0f;

    List<GameObject> Enemy_Projectile_Pool;

    void Start()
    {
        Enemy_Projectile_Pool = new List<GameObject>();

        for (int i = 0; i < m_Projectile_Quantity; i++)
        {
            GameObject objEnemyProjectile = Instantiate(m_enemyProjectile);
            objEnemyProjectile.SetActive(false);
            Enemy_Projectile_Pool.Add(objEnemyProjectile);
            objEnemyProjectile.transform.parent = this.transform;
        }
    }
    void Update()
    {
        if(m_enemyController.m_doFire == true)
        {
            if (Vector3.Distance(m_enemyController.transform.position, m_enemyManager.m_playerTransform.position) <= m_enemyController.m_minDistanceForFire && Time.time > m_elapsedTIme)
            {
                GenerateEnemyBullet();
                m_elapsedTIme = Time.time + m_enemyController.m_recoilTime;
            }
        }      
    }

    private void GenerateEnemyBullet()
    {
        for (int i = 0; i < Enemy_Projectile_Pool.Count; i++)
        {
            if (!Enemy_Projectile_Pool[i].activeInHierarchy)
            {
                Enemy_Projectile_Pool[i].transform.position = this.transform.position;
                Enemy_Projectile_Pool[i].transform.LookAt(m_enemyManager.m_playerTransform);
                Enemy_Projectile_Pool[i].SetActive(true);
                m_enemyController.m_hasFired = true;
                break;
            }
        }
    }
}
