using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform Target;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Target.position, rotationSpeed);
    }
}