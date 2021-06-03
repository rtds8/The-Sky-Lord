using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private Transform m_oblivion;

    void LateUpdate()
    {
        Vector3 pos = m_oblivion.position;
        pos.y = transform.position.y;
        transform.position = pos;

        //transform.rotation = Quaternion.Euler(90f, m_oblivion.rotation.y, 0f);
    }
}
