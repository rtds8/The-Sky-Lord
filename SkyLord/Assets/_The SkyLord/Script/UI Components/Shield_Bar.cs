using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield_Bar : MonoBehaviour
{
    [SerializeField] private Slider m_shieldSlider;
    [SerializeField] private float m_lerpTime = 0.2f;

    void Awake()
    {
        if (!m_shieldSlider)
            m_shieldSlider = this.transform.GetChild(1).GetComponent<Slider>();
    }

    public void SetMaxEnergy(float maxEnergy)
    {
        m_shieldSlider.maxValue = maxEnergy;
        m_shieldSlider.value = maxEnergy;
    }

    public void ReduceShieldHealth(float value)
    {
        StartCoroutine(LerpEnergySlider(m_shieldSlider, m_shieldSlider.value - value));
    }

    private IEnumerator LerpEnergySlider(Slider slider, float value)
    {
        float currentValue = slider.value;
        float elapsedTime = 0f;

        while (elapsedTime < m_lerpTime)
        {
            elapsedTime += Time.deltaTime;
            m_shieldSlider.value = Mathf.Lerp(currentValue, value, elapsedTime / m_lerpTime);
            yield return null;
        }

        slider.value = value;

        StopAllCoroutines();
    }
}
