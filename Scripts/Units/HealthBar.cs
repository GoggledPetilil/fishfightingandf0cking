using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image m_FillImage;

    public void SetHealthBar(float maxHealth, float currentHealth)
    {
        float h = currentHealth / maxHealth;
        m_FillImage.fillAmount = h;
    }
}
