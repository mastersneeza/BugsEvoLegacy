using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_ : MonoBehaviour {
    public float mapWidth = 36, mapHeight = 22;
    public Text levelText;
    public Text xpText;
    public Text ammoText;
    public Text highscoreText;
    public Text scoreText;
    public GameObject deathUI;
    public enum Location { LOCATION_FOREST, LOCATION_GARDEN, LOCATION_DESERT, LOCATION_WATER };
    public Location playerLocation;
    public GameObject[] spawners;

    private PlayerData1 pd;
    private void UpdateUI() {
        levelText.text = "Level " + pd.level.ToString();
        xpText.text = pd.xp.ToString() + "/" + pd.xpToNext.ToString() + "xp"; 
        ammoText.text = pd.loadedAmmo.ToString() + " | " + pd.ammo.ToString();
        highscoreText.text = "Highscore: " + pd.highScore.ToString() + "\nHighest Level: " + pd.highLevel.ToString();
        scoreText.text = "Score: " + pd.accumXp.ToString();
    }

    private void Start() {
        ChangePlayerLocation(Location.LOCATION_FOREST);
        pd = PlayerData1.GetPlayerData();
        if (pd.LoadPlayerData()) {
            SaveProfile();
        } else {
            pd.ResetPlayerData();
            SaveProfile();
        }
        pd.gamesPlayed++;
        UpdateUI();
    }

    public void ChangePlayerLocation(Location location) {
        /*playerLocation = location;
        Debug.Log(location);
        foreach (GameObject spawner in spawners) {
            InsectSpawner _spawner = spawner.GetComponent<InsectSpawner>();
            if (_spawner.locationType != playerLocation) {
                _spawner.enabled = false;
            }
            else {
                _spawner.enabled = true;
            }
        }*/

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SaveProfile();
            pd.ResetPlayerData();
            ReloadLevel();
        }

        if (Input.GetKeyDown(KeyCode.H)) {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.X))
            pd.ResetPlayerData();
        if (Input.GetKeyDown(KeyCode.C))
        {
            pd.ResetPlayerDataFull();
            ReloadLevel();
        }
        pd.timePlayed += Time.deltaTime;
        UpdateUI();
    }

    private void OnApplicationQuit() {
        SaveProfile();
    }

    public static GameManager_ GetGameManager() {
        return GameObject.FindWithTag("GameController").GetComponent<GameManager_>();
    }

    public void EndGame() {
        SaveProfile();
        Debug.Log("Game over");
        Time.timeScale = 0;
        deathUI.SetActive(true);
    }

    public void ReloadLevel() {
        SaveProfile();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        LoadProfile();
    }

    private void SaveProfile() {
        pd.SavePlayerData();
        Debug.Log("Save successful");
    }

    private void LoadProfile() {
        pd.LoadPlayerData();
    }

}
