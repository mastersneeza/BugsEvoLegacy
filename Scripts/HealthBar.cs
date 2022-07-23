using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset;
    public bool attachedToPlayer = false;

    public void SetHealth(float health, float maxHealth) {
        if (!attachedToPlayer)
            slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }

    private void Update() {
        if (!attachedToPlayer)
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

}
