using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData1 : MonoBehaviour {
    public string username;
    public int xp = 0;
    public int xpToNext = 0;
    public int accumXp = 0;
    public int highScore = 0;
    public int highLevel = 1;
    public int level = 1;
    public int enemiesKilled = 0;
    public int shotsFired = 0;
    public int blocksBuilt = 0;
    public int magSize = 30;
    public int ammo = 60;
    public int loadedAmmo = 30;
    public int overflowedHp = 0;
    public int queensKilled = 0;
    public int gamesPlayed = 0;
    public float timePlayed = 0;

    private Health hp;

    public static PlayerData1 GetPlayerData () {
        return GameObject.FindWithTag("Player").GetComponent<PlayerData1>();
    }

    void Start() {
        hp = GetComponent<Health>();
        xpToNext = CalculateXpToNext();
        CheckXp();
    }

    private int CalculateXpToNext() {
        return level * level + 5 * level;
    }

    void Update() {
        
    }

    public void AddExp(int xpAmount) {
        xp += xpAmount;
        accumXp += xpAmount;
        if (accumXp > highScore)
        {
            highScore = accumXp;
            Debug.Log("New high score!");
        }
        CheckXp();
    }

    public void AddAmmo(int amount)
    {
        ammo += amount;
    }

    public void SavePlayerData() {
        PlayerPrefs.SetString("PLAYER_USERNAME", username);
        PlayerPrefs.SetInt("PLAYER_XP", xp);
        PlayerPrefs.SetInt("PLAYER_XPTONEXT", xpToNext);
        PlayerPrefs.SetInt("PLAYER_ACCUMXP", accumXp);
        PlayerPrefs.SetInt("PLAYER_HIGHSCORE", highScore);
        PlayerPrefs.SetInt("PLAYER_HIGHLEVEL", highLevel);
        PlayerPrefs.SetInt("PLAYER_LEVEL", level);
        PlayerPrefs.SetInt("PLAYER_ENEMIES_KILLED", enemiesKilled);
        PlayerPrefs.SetInt("PLAYER_SHOTS_FIRED", shotsFired);
        PlayerPrefs.SetInt("PLAYER_BLOCKS_BUILT", blocksBuilt);
        PlayerPrefs.SetInt("PLAYER_HP_OVERFLOWED", overflowedHp);
        PlayerPrefs.SetInt("PLAYER_QUEENS_KILLED", queensKilled);
        PlayerPrefs.SetInt("PLAYER_GAMES_PLAYED", gamesPlayed);
        PlayerPrefs.SetFloat("PLAYER_TIME_PLAYED", timePlayed);
        PlayerPrefs.SetInt("PLAYER_AMMO", ammo);
        PlayerPrefs.SetInt("PLAYER_LOADED_AMMO", loadedAmmo);
        PlayerPrefs.SetInt("PLAYER_MAGSIZE", magSize);
        PlayerPrefs.Save();
    }

    public bool LoadPlayerData() { //Returns wether there is save data or not
        if (PlayerPrefs.HasKey("PLAYER_USERNAME")) {
            xp = PlayerPrefs.GetInt("PLAYER_XP");
            xpToNext = PlayerPrefs.GetInt("PLAYER_XPTONEXT");
            accumXp = PlayerPrefs.GetInt("PLAYER_ACCUMXP");
            highScore = PlayerPrefs.GetInt("PLAYER_HIGHSCORE");
            highLevel = PlayerPrefs.GetInt("PLAYER_HIGHLEVEL");
            level = PlayerPrefs.GetInt("PLAYER_LEVEL");
            enemiesKilled = PlayerPrefs.GetInt("PLAYER_ENEMIES_KILLED");
            shotsFired = PlayerPrefs.GetInt("PLAYER_SHOTS_FIRED");
            blocksBuilt = PlayerPrefs.GetInt("PLAYER_BLOCKS_BUILT");
            queensKilled = PlayerPrefs.GetInt("PLAYER_QUEENS_KILLED");
            timePlayed = PlayerPrefs.GetFloat("PLAYER_TIME_PLAYED");
            ammo = PlayerPrefs.GetInt("PLAYER_AMMO");
            loadedAmmo = PlayerPrefs.GetInt("PLAYER_LOADED_AMMO");
            overflowedHp = PlayerPrefs.GetInt("PLAYER_HP_OVERFLOWED");
            magSize = PlayerPrefs.GetInt("PLAYER_MAGSIZE");
            gamesPlayed = PlayerPrefs.GetInt("PLAYER_GAMES_PLAYED");
            Debug.Log("Save Data loaded successfully!");
            return false;
        } else
        {
            Debug.Log("No Save Data available!");
            return true;
        }
    }

    public void ResetPlayerData() {
        xp = 0;
        level = 1;
        accumXp = 0;
        xpToNext = CalculateXpToNext();
        magSize = 30;
        ammo = 30;
        loadedAmmo = 30;
        SavePlayerData();
    }

    public void BuildBlock()
    {
        blocksBuilt++;
    }

    public void ResetPlayerDataFull()
    {
        xp = 0;
        level = 1;
        highLevel = 1;
        accumXp = 0;
        xpToNext = CalculateXpToNext();
        magSize = 30;
        ammo = 30;
        loadedAmmo = 30;
        enemiesKilled = 0;
        blocksBuilt = 0;
        shotsFired = 0;
        highScore = 0;
        overflowedHp = 0;
        queensKilled = 0;
        gamesPlayed = 0;
        timePlayed = 0;
        username = "";
        SavePlayerData();
    }

    private void LevelUp() {
        level++;
        if (level > highLevel)
        {
            highLevel = level;
            Debug.Log("New level best");
        }
        ammo += 10;
        float percentage = hp.hp / hp.maxHp;
        hp.maxHp++;
        hp.hp = Mathf.Round(hp.maxHp * percentage);
        hp.HealDamage(1);
        xp -= xpToNext;
        xpToNext = CalculateXpToNext();
        SavePlayerData();
        Debug.Log("Level up!");
    }

    private void CheckXp() {
        while (xp >= xpToNext) {
            LevelUp();
        }
    }

    public void KillEnemy(int xpAmount) {
        enemiesKilled++;
        AddExp(xpAmount);
    }

    public void Shoot() {
        loadedAmmo--;
        shotsFired++;
    }
}
