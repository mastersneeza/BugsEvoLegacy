using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextManager : MonoBehaviour {
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Update() {
        foreach (FloatingText text in floatingTexts) {
            text.UpdateFloatingText();
        }        
    }

    public void Show(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        FloatingText floatingText = GetFloatingText();

        floatingText.FormatText(message, fontSize, color, position, motion, duration);
        floatingText.Show();
    }

    private FloatingText GetFloatingText() {
        FloatingText text = floatingTexts.Find(t => !t.active); //Find a floating text that isn't active

        if (text == null) {
            text = new FloatingText();
            text.prefab = Instantiate(textPrefab, textContainer.transform); //Create new FloatingText and make it a child of textContainer
            text.text = text.prefab.GetComponent<TMP_Text>();

            floatingTexts.Add(text);
        }

        return text;
    }
}
