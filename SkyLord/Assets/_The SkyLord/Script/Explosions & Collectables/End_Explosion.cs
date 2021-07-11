using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Explosion : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("InstantiateOrb", 0f);
        Invoke("DestroyExplosionPrefab", 2.5f);
    }

    void InstantiateOrb()
    {
        gameObject.GetComponent<Collectable>().SpawnOrb();
    }

    void DestroyExplosionPrefab()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke("InstantiateOrb");
        CancelInvoke("DestroyExplosionPrefab");
    }
}
