using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public TMP_Text ammoText;

    private float sessionStartTime;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        sessionStartTime = Time.time;
        DontDestroyOnLoad(gameObject);

    }

    private void UpdateUI() {
        ammoText.text = player.GetComponentInChildren<Gun>().loadedAmmo.ToString() + " | " + Gun.ammo.ToString();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            ScreenCapture.CaptureScreenshot("TestImage.png", 1);
        }
        UpdateUI();
    }

    //Resources
    public List<Sprite> skins;

    //References
    public Player player;
    public FloatingTextManager ftman;


    //Floating text
    public static void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        instance.ftman.Show(message, fontSize, color, position, motion, duration);
    }

    /*
        highScore, highLevel, shotsFired, blocksBuilt, insectsKilled, queensKilled, fliesKilled, gamesPlayer, timePlayed, overflowedHp
     */

    

}
