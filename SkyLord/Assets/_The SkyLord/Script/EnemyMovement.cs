using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform Player;
    //[SerializeField] private int MoveSpeed = 100;
    [SerializeField] private Vector3 OffsetDist;
    [SerializeField] private int MinDist;
    private Vector3 randomDirection;
    private Vector3 currentDirection;

    void Start()
    {
        StartCoroutine("RandomPosition");
    }

    void Update()
    {
        transform.DOLookAt(Player.position, 5); //because we need to look at the ship at all times

        Move();

    }

    private void Move()
    {
        transform.DOMove((Player.position - OffsetDist), 50, false).OnComplete(() => Move()); //move towards the ship, maintain distance by offset

        //if (Vector3.Distance(transform.position, (Player.position - OffsetDist)) > MinDist)
        {
            //if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            //{
            //Here Call any function U want Like Shoot at here or something
            //}
        }

       //else
        {

            //if our spaceship is too close, it will make a random direction vector, and move towards it.
            //since this calculates a vector inside 1 unit, so i am substracting by a vector to maintain the distance

            // transform.DOMove(new Vector3(Random.Range(transform.position.x - 1000, transform.position.x + 1000),
            //                              Random.Range(transform.position.y - 1000, transform.position.y + 1000),
            //                              Random.Range(transform.position.z - 1000, transform.position.z + 1000)), 50, false).OnComplete(() => transform.DOLookAt(Player.position, 5)).OnComplete(() => Move());


            //randomDirection = Random.insideUnitSphere.normalized;
            //randomDirection += new Vector3(1000, 1000, 1000);


            // Debug.Log(randomDirection);
        }
    }

    IEnumerator RandomPosition()
    {
        // the if condition isn't working out as whenever the condition is true, it starts workig like update()
        // so i just put 30 sec interval 

        // if (Vector3.Distance(transform.position, (Player.position)) <= MinDist)
        while (true)
        {
            Debug.Log(randomDirection);
            randomDirection = new Vector3(Random.Range(transform.position.x - 1000, transform.position.x + 1000),
                                                Random.Range(transform.position.y - 1000, transform.position.y + 1000),
                                                Random.Range(transform.position.z - 1000, transform.position.z + 1000));
            transform.DOMove(randomDirection, 10, false).OnComplete(() => transform.DOLookAt(Player.position, 5));//.OnComplete(() => Move());
            yield return new WaitForSeconds(30);
        }
    }
}