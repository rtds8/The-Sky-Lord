using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Impact : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DestroyObject", 2f);
    }

    void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke("DestroyObject");
    }
}
