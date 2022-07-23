using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingEntity {
    public float moveSpeed = 5f;
    public float rotationOffset = -90f;
    public float rotationSpeed = 10f;

    public HPBar hpbar;
    public XPBar xpbar;
    public GameObject mobileUI;
    public FixedJoystick moveStick;

    //Home stats
    public string username;
    public int skinId = 0;
    public int totalXp = 0;
    public int playerLevel = 1;
    public int playerXp = 0;
    public int playerXpToNext = 1;

    public int highLevel = 1;
    public int highScore = 0;
    public int shotsFired = 0;
    public int blocksBuilt = 0;
    public int insectsKilled = 0;
    public int queensKilled = 0;
    public int fliesKilled = 0;
    public int gamesPlayed = 0;
    public float timePlayed = 0;

    //In game stats
    [SerializeField]
    private int level = 1;
    [SerializeField]
    private int xpToNext = 1;
    [SerializeField]
    private int xp = 0;
    private int accumXp = 0;

    private float startTime;

    private Vector2 mousePosition;
    private Vector2 lookDirection;
    private float angle;
    private bool newHighScoreReachedDuringSession = false;
    private bool isMobile;

    protected override void Die() {
        Application.Quit();
    }

    public void KillInsect(int xpValue) {
        insectsKilled++;
        AddExp(xpValue);
    }

    public void AddExp(int amount) {
        xp += amount;
        accumXp += amount;
        xpbar.SetXp(xp);
        if (accumXp > highScore) {
            highScore = accumXp;
            if (!newHighScoreReachedDuringSession) {
                GameManager.ShowText("New high score!", 48, new Color(1, 0.5f, 1), transform.position, Vector3.up * 100, 1f);
                newHighScoreReachedDuringSession = true;
            }
        }
        totalXp += amount;
        playerXp += amount;
        CheckXp();
    }

    private void CheckXp() {
        while (xp >= xpToNext) {
            xp -= xpToNext;
            xpToNext = CalculateNextXp();
            xpbar.SetXp(xp);
            xpbar.SetMaxXp(xpToNext);
            LevelUp(true, false);
        }
        while (playerXp >= playerXpToNext) {
            playerXp -= playerXpToNext;
            playerXpToNext = CalculateNextXp();
            LevelUp(false, true);
        }

    }

    private void LevelUp(bool showText, bool playerLevelYesNo) {
        if (playerLevelYesNo) {
            playerLevel++;
        }
        else {
            level++;
            if (level > highLevel) {
                highLevel = level;
            }
        }
        maxHp++;
        hpbar.SetMaxHealth(maxHp);
        ReceiveHp(1);
        if (showText)
            GameManager.ShowText("Level Up!", 48, Color.white, transform.position, Vector3.up * 50, 1.5f);
    }

    private int CalculateNextXp() {
        return level * level + 5 * level;
    }

    public static Player FindPlayer(string name) {
        return GameObject.Find(name).GetComponent<Player>();
    }

    protected override void ReceiveHp(int amount) {
        base.ReceiveHp(amount);
        hpbar.SetHealth(hp);
    }

    private void SetStats(PlayerData pd) {
        username = pd.username;
        skinId = pd.skinId;
        playerLevel = pd.playerLevel;
        playerXpToNext = pd.playerXpToNext;
        totalXp = pd.totalXp;
        playerXp = pd.playerXp;
        highLevel = pd.highLevel;
        highScore = pd.highScore;
        shotsFired = pd.shotsFired;
        blocksBuilt = pd.blocksBuilt;
        insectsKilled = pd.insectsKilled;
        queensKilled = pd.queensKilled;
        fliesKilled = pd.fliesKilled;
        gamesPlayed = pd.gamesPlayed;
        timePlayed = pd.timePlayed;
    }

    protected override void Start() {
        base.Start();
        //isMobile = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
        isMobile = true;
        mobileUI.SetActive(isMobile);
        startTime = Time.time;
        hpbar.SetMaxHealth(maxHp);
        gameObject.name = "p_" + username;

        PlayerData pd = SaveSystem.LoadPlayer(); //When in multiplayer load player's data on joining a match
        if (pd == null) {
            xpToNext = CalculateNextXp();
            playerXpToNext = CalculateNextXp();
            SaveSystem.SavePlayer(this);
        }
        else {
            SetStats(pd);
        }
        xpbar.SetXp(xp);
        xpbar.SetMaxXp(xpToNext);
        gamesPlayed++;
    }

    private void OnApplicationQuit() { //In multiplayer save player's stats on death or on quitting match
        timePlayed += Time.time - startTime;
        SaveSystem.SavePlayer(this);
    }

    protected override void ReceiveDamage(Damage dmg) {
        hp -= dmg.damageAmount;
        hpbar.SetHealth(hp);

        pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

        GameManager.ShowText("-" + dmg.damageAmount + " hp", 48, Color.red, transform.position, Vector3.up * 50, 1.5f);

        if (hp <= 0) {
            hp = 0;
            Die();
        }
    }

    protected override void Update() {
        base.Update();

        //Get input for movement

        if (Input.GetKeyDown(KeyCode.C)) {
            skinId = 0;
            level = 1;
            xpToNext = 0;
            xp = 0;
            shotsFired = 0;
            blocksBuilt = 0;
            insectsKilled = 0;
            queensKilled = 0;
            fliesKilled = 0;
            gamesPlayed = 0;
            timePlayed = 0;
            SaveSystem.SavePlayer(this);
        }

    }

    private void FixedUpdate() {
        KnockBack();

        if (isMobile) {
            movement.x = moveStick.Horizontal * moveSpeed;
            movement.y = moveStick.Vertical * moveSpeed;
            mousePosition = GetComponent<Gun>().firePoint.transform.position;
        }
        else {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Get mouse's position in world coordinates
        }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime); //Move player

        lookDirection = mousePosition - rb.position; //Get the vector the player is looking at
        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + rotationOffset; //Convert the vector into an angle from the x-axis and adjust using rotationOffset
        rb.rotation = angle; //Set the player's angle
    }
}
