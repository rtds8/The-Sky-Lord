using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar_Ping : MonoBehaviour
{
    private SpriteRenderer m_pingSprite;
    private float m_disapperTime, m_maxDisappearTime;
    private Color m_pingColor;

    private void Awake()
    {
        m_pingSprite = this.gameObject.GetComponent<SpriteRenderer>();
        m_disapperTime = 0f;
        m_maxDisappearTime = 1f;
        m_pingColor = new Color(1, 0, 0, 1f);
        m_pingSprite.color = m_pingColor;
    }

    void Update()
    {
        m_disapperTime += Time.deltaTime;

        m_pingColor.a = Mathf.Lerp(m_maxDisappearTime, 0f, m_disapperTime / m_maxDisappearTime);

        if (m_disapperTime >= m_maxDisappearTime)
            Destroy(gameObject);
    }

    public void SetPingColor(Color color)
    {
        this.m_pingColor = color;
        this.m_pingSprite.color = m_pingColor;
    }

    public void SetDisappearTime(float maxDisappearTime)
    {
        this.m_maxDisappearTime = maxDisappearTime;
        m_disapperTime = 0f;
    }
}
