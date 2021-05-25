using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] internal Transform Player;
    //[SerializeField] private int MoveSpeed = 100;
    [SerializeField] private Vector3 OffsetDist;
    [SerializeField] internal int MinDist;
    private Vector3 randomDirection;

    void Start()
    {
        StartCoroutine("RandomPosition");

        Move();
    }

    void Update()
    {
        transform.DOLookAt(Player.position, 2); 
    }

    private void Move()
    {
        transform.DOMove((Player.position - OffsetDist), 20, false).OnComplete(() => Move()); 
    }

    IEnumerator RandomPosition()
    {
        while (true)
        {
            randomDirection = new Vector3(Random.Range(transform.position.x - 200, transform.position.x + 200),
                                                Random.Range(transform.position.y - 100, transform.position.y + 100),
                                                Random.Range(transform.position.z - 200, transform.position.z + 200));
            transform.DOMove(randomDirection, 20, false).OnComplete(() => transform.DOLookAt(Player.position, 2));
            yield return new WaitForSeconds(10);
        }
    }
}