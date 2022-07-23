using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour {
    public TMP_Text statsText;

    PlayerData1 pd;

    void Start() {
        pd = new PlayerData1();
        pd.LoadPlayerData();

        statsText.text = "Best run: " + pd.highScore.ToString() + "/" + pd.highLevel.ToString() + "\n" +
                         "Enemies killed: " + pd.enemiesKilled.ToString() + "\n" +
                         "Queen Bees killed: " + pd.queensKilled.ToString() + "\n" +
                         "Shots fired: " + pd.shotsFired.ToString() + "\n" +
                         "Crates built: " + pd.blocksBuilt.ToString() + "\n" +
                         "Overflowed hp: " + pd.overflowedHp.ToString() + "\n" + 
                         "Games played: " + pd.gamesPlayed.ToString() + "\n" + 
                         "Time played (h): " + (pd.timePlayed / 3600).ToString("0.0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
