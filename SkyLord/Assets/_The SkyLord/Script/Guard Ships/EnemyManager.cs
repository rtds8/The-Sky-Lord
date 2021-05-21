using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private List<GameObject> enemyShipList;

    private float distance;
    private float minDistance = 0;
    private bool active = false;

    private GameObject shipToActivate;

    void Start()
    {
        foreach (GameObject ship in enemyShipList)
            ship.SetActive(false);
        enemyShipList[0].SetActive(true);
        active = true;
    }

    void Update()
    {
        foreach (GameObject ship in enemyShipList)
            if (ship.activeSelf == true)
            {
                active = true;
                break;
            }
            else
                active = false;

        if (active == false) 
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
            }
            shipToActivate.SetActive(true);
        }
    }
}
