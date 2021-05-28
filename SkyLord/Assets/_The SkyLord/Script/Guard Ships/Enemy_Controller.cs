using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    [SerializeField] Enemy_Manager m_enemyManager;
    [SerializeField] private float m_moveSpeed = 100f, m_dodgeDistance = 100f;
    [SerializeField] internal float m_minDistanceForFire, m_recoilTime = 2f;
    [SerializeField] private Vector3 m_offsetDistance;
    [Range(0.1f,0.5f)]
    [SerializeField] private float m_dodgeStrength;
    internal bool m_doFire;
    
    private void OnEnable()
    {
        m_doFire = false;
        StartCoroutine(MoveToPlayer());
    }

    private void LateUpdate()
    {
        this.transform.LookAt(m_enemyManager.m_playerTransform, m_enemyManager.m_playerTransform.up);
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

        else if(currentDistance <= m_dodgeDistance)
        {
            m_doFire = false;

            var dodgeValue = m_enemyManager.m_playerTransform.position + new Vector3(Random.Range(transform.position.x - 50, transform.position.x + 50),
                                                Random.Range(transform.position.y - 30, transform.position.y + 30),
                                                Random.Range(transform.position.z - 15, transform.position.z));

            transform.position = Vector3.Lerp(transform.position, dodgeValue, m_dodgeStrength);
        }

        else
        {
            m_doFire = true;
        }

        StartCoroutine(MoveToPlayer());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
