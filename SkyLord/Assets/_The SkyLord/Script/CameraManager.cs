using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera FPP;
    [SerializeField] private Camera TPP;

    // Start is called before the first frame update
    void Start()
    {
        TPP.enabled = true;
        FPP.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            FPP.enabled = !FPP.enabled;
            TPP.enabled = !TPP.enabled;
        }
    }
}
