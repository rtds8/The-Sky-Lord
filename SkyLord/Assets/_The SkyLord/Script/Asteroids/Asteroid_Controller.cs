using UnityEngine;

public class Asteroid_Controller : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotateSpeed = 0.1f;

    void Awake()
    {
        if (target == null)
            target = this.transform;

        rotateSpeed = Random.Range(rotateSpeed / 2, rotateSpeed);
    }

    void Update()
    {
        transform.RotateAround(target.position, target.up, rotateSpeed * Time.deltaTime);        
    }
}
