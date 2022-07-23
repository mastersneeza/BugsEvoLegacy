using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public string username;
    public int skinId;
    public int playerLevel = 1;
    public int playerXpToNext;
    public int playerXp;
    public int totalXp;
    public int highScore;
    public int highLevel;
    public int shotsFired;
    public int blocksBuilt;
    public int insectsKilled;
    public int queensKilled;
    public int fliesKilled;
    public int gamesPlayed;
    public float timePlayed;

    public PlayerData(Player player) {
        username = player.username;
        skinId = player.skinId;
        playerLevel = player.playerLevel;
        totalXp = player.totalXp;
        playerXpToNext = player.playerXpToNext;
        playerXp = player.playerXp;
        shotsFired = player.shotsFired;
        highScore = player.highScore;
        highLevel = player.highLevel;
        blocksBuilt = player.blocksBuilt;
        insectsKilled = player.insectsKilled;
        queensKilled = player.queensKilled;
        fliesKilled = player.fliesKilled;
        gamesPlayed = player.gamesPlayed;
        timePlayed = player.timePlayed;

    }
}
