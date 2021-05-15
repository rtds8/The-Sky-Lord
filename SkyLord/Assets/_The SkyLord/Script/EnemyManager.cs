using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private List<GameObject> enemyShipList;

    private float distance;
    private float minDistance = 0;
    private bool active;

    private GameObject shipToActivate;

    // Start is called before the first frame update
    void Start()
    {
        //At start the first ship is set active
        foreach (GameObject ship in enemyShipList)
            ship.SetActive(false);
        enemyShipList[0].SetActive(true);
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        //when 
        foreach (GameObject ship in enemyShipList)
            if (ship.activeSelf == true)
            {
                active = true;
                break;
            }
            else
                active = false;

        if (active == false) //if(nothing is active in list)
        {
            minDistance = Vector3.Distance(player.position, enemyShipList[0].transform.position);
            foreach (GameObject ship in enemyShipList)
            {
                distance = Vector3.Distance(player.position, ship.transform.position);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    shipToActivate = ship;
                }
                //calculate the dist. between all enemy ships & the player.
                //find the min. dist. & set active the one with min. dist.
            }
            shipToActivate.SetActive(true);
        }
    }
}
