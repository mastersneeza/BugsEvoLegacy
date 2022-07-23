using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable {
    public int weaponLevel = 0;
    private SpriteRenderer sren;

    protected float cooldown = 0.625f;
    private float lastShot;

    protected override void Start() {
        base.Start();
        sren = GetComponent<SpriteRenderer>();
    }

    protected override void Update() {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Time.time - lastShot > cooldown) {
                lastShot = Time.time;
                Attack();
            }
        }
    }

    protected virtual void Attack() {
        
    }
}
