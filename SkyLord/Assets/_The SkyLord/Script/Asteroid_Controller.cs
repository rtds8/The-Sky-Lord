using UnityEngine;

public class Asteroid_Controller : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotateSpeed = 0.1f;
    private Quaternion randomRotation;

    void Awake()
    {
        randomRotation = Random.rotation;
        rotateSpeed = Random.Range(0.1f, rotateSpeed);
    }

    void Update()
    {
        transform.Rotate(randomRotation.eulerAngles * rotateSpeed/10 * Time.deltaTime);
        
        transform.RotateAround(target.position, target.up, rotateSpeed * Time.deltaTime);        
    }
}
