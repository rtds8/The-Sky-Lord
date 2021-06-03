using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Rotate : MonoBehaviour
{
    [SerializeField] private Transform m_planetTransform;
    [SerializeField] private float m_rotateSpeed = 10f;
    void Awake()
    {
        m_planetTransform = this.transform;
    }

    void Update()
    {
        m_planetTransform.Rotate(new Vector3(0, m_rotateSpeed * Time.deltaTime, 0), Space.World);
    }
}
