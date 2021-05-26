using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] Animator doorAnimator;

    [Header("Door Scan")]
    [SerializeField] GameObject DoorSignal1;
    [SerializeField] GameObject DoorSignal2;
    [SerializeField] GameObject DoorScanner1;
    [SerializeField] GameObject DoorScanner2;

    [Header("Door Scan Lights")]
    [SerializeField] GameObject RedLight;
    [SerializeField] GameObject GreenLight;

    [Header("Materials")]
    [SerializeField] Material green;
    [SerializeField] Material red;
    [SerializeField] Material blue;
    [SerializeField] Material glass;

    MeshRenderer signal1, signal2, scanner1, scanner2;

    // Start is called before the first frame update
    void Start()
    {
        signal1 = DoorSignal1.GetComponent<MeshRenderer>();
        signal2 = DoorSignal2.GetComponent<MeshRenderer>();
        scanner1 = DoorScanner1.GetComponent<MeshRenderer>();
        scanner2 = DoorScanner2.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        RaycastHit hit;
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(position, transform.TransformDirection(Vector3.forward), out hit, 1, layerMask) || Physics.Raycast(position, transform.TransformDirection(-Vector3.forward), out hit, 3, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            doorAnimator.SetBool("character_nearby", true);
            signal1.material = green;
            signal2.material = green;
            scanner1.material = blue;
            scanner2.material = blue;
            RedLight.SetActive(false);
            GreenLight.SetActive(true);
        }
        else
        {
            Debug.DrawRay(position, transform.TransformDirection(Vector3.forward) * 1, Color.white);
            Debug.Log("Did not Hit");
            doorAnimator.SetBool("character_nearby", false);
            signal1.material = red;
            signal2.material = red;
            scanner1.material = glass;
            scanner2.material = glass;
            GreenLight.SetActive(false);
            RedLight.SetActive(true);
        }
    }
}
