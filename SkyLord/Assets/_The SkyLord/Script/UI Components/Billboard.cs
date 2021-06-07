using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Canvas m_healthBarCanvas;
    [SerializeField] CameraManager m_cameraManager;

    private void OnEnable()
    {
        m_healthBarCanvas = gameObject.GetComponent<Canvas>();
        m_healthBarCanvas.worldCamera = m_cameraManager._activeCam;
    }

    private void LateUpdate()
    {
        m_healthBarCanvas.worldCamera = m_cameraManager._activeCam;
    }
}
