using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy_Controller : MonoBehaviour
{
    [SerializeField] Enemy_Manager m_enemyManager;
    [SerializeField] internal float m_moveSpeed = 20f, m_recoilTime = 2f;
    [SerializeField] private Vector3 m_offsetDistance;
    [SerializeField] internal float m_minDistanceForFire;
    internal bool m_hasFired, m_doFire;

    private void OnEnable()
    {
        m_hasFired = false;
        m_doFire = false;
        MakeMovement(m_moveSpeed);
        StartCoroutine(MakeRandomMovement());
    }

    void FixedUpdate()
    {
        transform.DOLookAt(m_enemyManager.m_playerTransform.position, 0f);
    }

    private void MakeMovement(float speed)
    {
        transform.DOMove((m_enemyManager.m_playerTransform.position - m_offsetDistance), speed, false).OnComplete(() => m_doFire = true);
    }

    private bool FiredAtPlayer()
    {
        if(m_hasFired)
            return true;

        else
            return false;
    }

    private IEnumerator MakeRandomMovement()
    {
        yield return new WaitUntil(FiredAtPlayer);

        m_hasFired = false;

        var currentDistance = Vector3.Distance(transform.position, m_enemyManager.m_playerTransform.position);

        if (currentDistance <= 50f || currentDistance >= m_offsetDistance.magnitude)
        {
            m_doFire = false;
            MakeMovement(m_recoilTime);
        }
        
        else
        {
            m_doFire = false;
            var m_randomMovement = new Vector3(Random.Range(transform.position.x - 30, transform.position.x + 30),
                                                Random.Range(transform.position.y - 10, transform.position.y + 10),
                                                Random.Range(transform.position.z - 20, transform.position.z + 20));

            transform.DOMove(transform.position + m_randomMovement, m_recoilTime, false).OnComplete(() => m_doFire = true);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
