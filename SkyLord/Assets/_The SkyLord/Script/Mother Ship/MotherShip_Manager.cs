using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip_Manager : MonoBehaviour
{
    [SerializeField] internal Transform m_playerShip;
    [SerializeField] internal float m_motherShipSpeed = 10f;
    [SerializeField] List<Vector3> m_jumpLocations = new List<Vector3>();
    [SerializeField] List<Quaternion> m_Rotations = new List<Quaternion>();
    [SerializeField] List<GameObject> m_dropShips = new List<GameObject>();

    void Awake()
    {
        foreach (GameObject ship in m_dropShips)
            ship.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.GetComponent<Animator>().GetBehaviour<MotherShip_Disappear>().m_dropShipsSpawned >= 5)
            gameObject.GetComponent<Animator>().SetBool("Has Powered Up", true);
    }

    internal GameObject GetDropShip()
    {
        GameObject dropShip = m_dropShips[0];
        m_dropShips.RemoveAt(0);
        return dropShip;
    }

    internal Vector3 GetNextLocation()
    {
        Vector3 nextPos = m_jumpLocations[0];
        m_jumpLocations.RemoveAt(0);
        return nextPos;
    }

    internal Quaternion GetNextOrientation()
    {
        Quaternion nextOrientation = m_Rotations[0];
        m_Rotations.RemoveAt(0);
        return nextOrientation;
    }
}
