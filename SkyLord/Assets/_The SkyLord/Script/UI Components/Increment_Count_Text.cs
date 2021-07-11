using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Increment_Count_Text : MonoBehaviour
{
    [SerializeField] private Text m_countText;
    internal int count = 0;

    void Awake()
    {
        if (!m_countText)
            m_countText = this.transform.GetChild(1).GetComponent<Text>();
    }

    void Start()
    {
        m_countText.text = count.ToString();   
    }

    internal void IncrementCount()
    {
        count++;
        m_countText.text = count.ToString();
    }
}
