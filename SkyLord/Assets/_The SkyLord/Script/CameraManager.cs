using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera FPP;
    public Camera TPP;
    [SerializeField] private float sensitivity;
    public Vector3 currentRotation;

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

    public void TPPCamera()
    {
        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        //currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        //currentRotation.y = Mathf.Repeat(currentRotation.y, 360);
        currentRotation.x = Mathf.Clamp(currentRotation.x, -45, 45);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -30, 15);
        TPP.transform.localRotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        //if (Input.GetMouseButtonDown(0))
        //    Cursor.lockState = CursorLockMode.Locked;
    }

    public void FPPCamera()
    {
        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        //currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.x = Mathf.Clamp(currentRotation.x, -45, 45);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -15, 5);
        FPP.transform.localRotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
       // if (Input.GetMouseButtonDown(0))
          //  Cursor.lockState = CursorLockMode.Locked;
    }
}
