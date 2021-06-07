using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [SerializeField] internal Transform m_playerTransform;
    [SerializeField] private List<GameObject> enemyShipList;

    private bool isAnyShipActive = false;

    private GameObject shipToActivate;

    void Start()
    {
        foreach (GameObject ship in enemyShipList)
            ship.SetActive(false);
        enemyShipList[0].SetActive(true);
        isAnyShipActive = true;
    }

    void Update()
    {
        foreach (GameObject ship in enemyShipList)
            if (ship.activeSelf == true)
            {
                isAnyShipActive = true;
                break;
            }
            else
                isAnyShipActive = false;

        if (isAnyShipActive == false)
        {
            ActivateNearestShip();
        }
    }

    private void ActivateNearestShip()
    {
        var nearestShipDistance = Vector3.Distance(m_playerTransform.position, enemyShipList[0].transform.position);

        foreach (GameObject ship in enemyShipList)
        {
            var myDistance = Vector3.Distance(m_playerTransform.position, ship.transform.position);

            if (nearestShipDistance >= myDistance)
            {
                nearestShipDistance = myDistance;
                shipToActivate = ship;
            }
        }

        shipToActivate.SetActive(true);
        isAnyShipActive = true;
    }

    public void RemoveShip(GameObject deactivatedShip)
    {
        for(int i = 0; i < enemyShipList.Count; i++)
        {
            if(enemyShipList[i] == deactivatedShip)
            {
                enemyShipList[i].SetActive(false);
                enemyShipList.RemoveAt(i);
            }
        }
    }
}
