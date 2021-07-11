using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private GameObject m_collectableOrb;

    public void SpawnOrb()
    {
        Instantiate(m_collectableOrb, transform.position, Quaternion.identity);
    }
}
