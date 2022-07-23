using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPBar : MonoBehaviour {
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TMP_Text healthTxt;

    public void SetMaxHealth(int maxHealth) {
        slider.maxValue = maxHealth;
        healthTxt.text = slider.value.ToString() + " / " + maxHealth.ToString() + " HP";

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health) {
        slider.value = health;
        healthTxt.text = slider.value.ToString() + " / " + slider.maxValue.ToString();

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
