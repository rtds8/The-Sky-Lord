using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Bar : MonoBehaviour
{
    [SerializeField] private Slider m_fgHealthSlider, m_bgHealthSlider;
    [SerializeField] private float m_lerpTime = 0.2f;

    void Awake()
    {
        if (!m_fgHealthSlider)
            m_fgHealthSlider = this.transform.GetChild(2).GetComponent<Slider>();

        if (!m_bgHealthSlider)
            m_bgHealthSlider = this.transform.GetChild(1).GetComponent<Slider>();
    }

    public void SetMaxHealth(float maxHealth)
    {
        m_fgHealthSlider.maxValue = maxHealth;
        m_fgHealthSlider.value = maxHealth;
        m_bgHealthSlider.maxValue = maxHealth;
        m_bgHealthSlider.value = maxHealth;
    }

    public void DecrementHealth(float damageValue)
    {
        m_fgHealthSlider.value -= damageValue;

        StartCoroutine(LerpHealthSlider(m_bgHealthSlider, m_fgHealthSlider.value));
    }

    public void IncrementHealth(float amount)
    {
        StartCoroutine(LerpHealthSlider(m_fgHealthSlider, m_bgHealthSlider.value + amount));
        
        m_bgHealthSlider.value += amount;
    }

    private IEnumerator LerpHealthSlider(Slider slider, float value)
    {
        float currentValue = slider.value;
        float elapsedTime = 0f;

        while (elapsedTime < m_lerpTime)
        {
            elapsedTime += Time.deltaTime;
            m_bgHealthSlider.value = Mathf.Lerp(currentValue, value, elapsedTime / m_lerpTime);
            yield return null;
        }

        slider.value = value;

        StopAllCoroutines();
    }
}
