using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private int MoveSpeed = 4;
    [SerializeField] private int OffsetDist = 10;
    [SerializeField] private int MinDist = 5;
    //private float activeForwardSpeed, forwardAcceleration = 2.5f;

    void Start()
    {

    }

    void Update()
    {
        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {

            //activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * MoveSpeed, forwardAcceleration * Time.deltaTime);

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            //if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            //{
            //Here Call any function U want Like Shoot at here or something
            //}


        }
        else //if (Vector3.Distance(transform.position, Player.position) < MinDist - OffsetDist || Vector3.Distance(transform.position, Player.position) < MinDist + OffsetDist)
        { 

            transform.position -= transform.forward * MoveSpeed * Time.deltaTime;

        }
    }
}
