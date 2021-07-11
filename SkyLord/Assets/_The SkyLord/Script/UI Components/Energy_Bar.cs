using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy_Bar : MonoBehaviour
{
    [SerializeField] private Slider m_energySlider;
    [SerializeField] private float m_lerpTime = 0.2f;

    void Awake()
    {
        if (!m_energySlider)
            m_energySlider = this.transform.GetChild(1).GetComponent<Slider>();
    }

    public void SetMaxEnergy(float maxEnergy)
    {
        m_energySlider.maxValue = maxEnergy;
        m_energySlider.value = maxEnergy;
    }

    public void ReduceEnergy(float value)
    {
        StartCoroutine(LerpEnergySlider(m_energySlider, m_energySlider.value - value));
    }

    private IEnumerator LerpEnergySlider(Slider slider, float value)
    {
        float currentValue = slider.value;
        float elapsedTime = 0f;

        while (elapsedTime < m_lerpTime)
        {
            elapsedTime += Time.deltaTime;
            m_energySlider.value = Mathf.Lerp(currentValue, value, elapsedTime / m_lerpTime);
            yield return null;
        }

        slider.value = value;

        StopAllCoroutines();
    }
}
