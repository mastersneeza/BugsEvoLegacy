using UnityEngine;
using TMPro;

public class FloatingText {
    public bool active;
    public GameObject prefab;
    public TMP_Text text;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void SetText(string message) {
        text.text = message;
    }

    public void FormatText(string message, int fontSize, Color color, Vector3 position, Vector3 _motion, float _duration) {
        SetText(message);
        text.fontSize = fontSize;
        text.color = color;
        prefab.transform.position = Camera.main.WorldToScreenPoint(position);
        motion = _motion;
        duration = _duration;

    }

    public void Show() {
        active = true;
        lastShown = Time.time;
        ToggleFloatingText();
    }

    public void Hide() {
        active = false;
        ToggleFloatingText();
    }

    public void ToggleFloatingText() {
        prefab.SetActive(active);
    }

    public void UpdateFloatingText() {
        if (!active)
            return;

        if (Time.time - lastShown > duration) {
            Hide();
        }

        prefab.transform.position += motion * Time.deltaTime;
    }
}
