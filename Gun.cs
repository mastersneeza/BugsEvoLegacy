using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {
    public GameObject firePoint;
    public GameObject bulletPrefab;

    public int loadedAmmo;
    public int magSize = 30;
    [SerializeField]
    public static int ammo;

    protected override void Start() {
        base.Start();

        cooldown = 0.15f;
        loadedAmmo = magSize;
        ammo = magSize;
    }

    protected override void Update() {
        base.Update();
        if (Input.GetKeyDown(KeyCode.R)) {
            Reload();
        }
    }

    protected override void Attack() {
        if (loadedAmmo > 0) {
            Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation).GetComponent<Bullet>().shooter = gameObject.name;
            Player.FindPlayer(gameObject.name).shotsFired++;
            loadedAmmo--;
        }
        else {
            Reload();
        }

    }

    public void Shoot() {
        Attack();
    }

    public virtual void Reload() {
        int ammoRequired = magSize - loadedAmmo;
        if (ammoRequired > ammo) {
            loadedAmmo += ammo;
            ammo = 0;
        }
        else {
            loadedAmmo += ammoRequired;
            ammo -= ammoRequired;
        }
    }
}
