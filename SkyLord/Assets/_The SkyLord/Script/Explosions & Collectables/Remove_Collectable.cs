using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove_Collectable : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("RemoveCollectable", 30f);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("The Oblivion"))
        {
            RemoveCollectable();
        }
    }

    void RemoveCollectable()
    {
        Destroy(gameObject);
    }

    void OnDisable()
    {
        CancelInvoke("RemoveCollectable");    
    }
}
