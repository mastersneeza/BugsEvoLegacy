using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBar : MonoBehaviour {
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TMP_Text healthTxt;

    public void SetMaxXp(int maxXp) {
        slider.maxValue = maxXp;
        healthTxt.text = slider.value.ToString() + " / " + maxXp.ToString() + " XP";

        fill.color = gradient.Evaluate(1f);
    }

    public void SetXp(int xp) {
        slider.value = xp;
        healthTxt.text = slider.value.ToString() + " / " + slider.maxValue.ToString();

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
