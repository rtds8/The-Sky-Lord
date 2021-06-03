using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse_Radar : MonoBehaviour
{
    [SerializeField] private Radar_Ping m_radarPing;
    [SerializeField] private Transform m_pulse;
    [SerializeField] private LayerMask m_layerMask;
    private SpriteRenderer m_radarPulseSprite;
    private Color m_radarSpriteColor;
    private float m_currentRange, m_maxRange, m_fade;
    private List<Collider> m_alreadyHit;

    private void Awake()
    {
        m_radarPulseSprite = m_pulse.GetComponent<SpriteRenderer>();
        m_radarSpriteColor = m_radarPulseSprite.color;
        m_maxRange = 1000f;
        m_fade = 30f;
        m_alreadyHit = new List<Collider>();
    }

    private void Update()
    {
        float rangeSpeed = 1000f;
        m_currentRange += (rangeSpeed * Time.deltaTime);

        if (m_currentRange >= m_maxRange)
        {
            m_alreadyHit.Clear();   
            m_currentRange = 0f;
        }

        m_pulse.localScale = new Vector3(m_currentRange, m_currentRange);

        RaycastHit[] raycastHit = Physics.SphereCastAll(transform.position, (m_currentRange * 5) / 2f, transform.up, 5000f, m_layerMask);

        foreach(RaycastHit raycast in raycastHit)
        {
            if (raycast.collider != null)
            {
                if (!m_alreadyHit.Contains(raycast.collider))
                {
                    m_alreadyHit.Add(raycast.collider);
                    Instantiate(m_radarPing, raycast.collider.transform.position, m_radarPing.transform.rotation);
                }

                m_radarPing.SetDisappearTime(m_maxRange / rangeSpeed);
            }
        }

        if (m_currentRange > m_maxRange - m_fade)
        {
            m_radarSpriteColor.a = Mathf.Lerp(1f, 0f, (m_maxRange - m_currentRange) / m_fade);
        }

        else
            m_radarSpriteColor.a = 1f;

        m_radarPulseSprite.color = m_radarSpriteColor; 
    }
}
