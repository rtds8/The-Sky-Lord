using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] internal Transform m_PlayerTransform;
    [SerializeField] private float m_moveSpeed = 20f;
    [SerializeField] private Vector3 m_offsetDistance;
    [SerializeField] internal float m_minDistance;
    private Vector3 m_randomDirection;

    private void OnEnable()
    {
        Move();
    }

    void Start()
    {
        StartCoroutine(RandomPosition());        
    }

    void Update()
    {
        transform.DOLookAt(m_PlayerTransform.position, 1); 
    }

    private void Move()
    {
        transform.DOMove((m_PlayerTransform.position - m_offsetDistance), m_moveSpeed, false).OnComplete(() => Move()); 
    }

    IEnumerator RandomPosition()
    {
        while (true)
        {
            m_randomDirection = new Vector3(Random.Range(transform.position.x - 200, transform.position.x + 200),
                                                Random.Range(transform.position.y - 100, transform.position.y + 100),
                                                Random.Range(transform.position.z - 200, transform.position.z + 200));
            transform.DOMove(m_randomDirection, m_moveSpeed, false).OnComplete(() => transform.DOLookAt(m_PlayerTransform.position, 2));
            yield return new WaitForSeconds(10);
        }
    }
}