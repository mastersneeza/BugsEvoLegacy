using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public Transform buildPoint;
    public GameObject wallPrefab;
    public float bulletForce = 20f;

    private Health hp;
    private GameObject bullet;
    private Rigidbody2D bulletRb;
    private PlayerData1 pd;
    private Shooter shooter;
    
    private void Start() {
        hp = GetComponent<Health>();
        hp.hpBar.SetHealth(hp.hp, hp.maxHp);
        pd = GetComponent<PlayerData1>();
        shooter = GetComponent<Shooter>();
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            if (pd.loadedAmmo > 0) {
                pd.Shoot();
                shooter.Shoot();
            }
            else {
                Reload();
            }
        }
        if (Input.GetButtonDown("Fire2")) {
            pd.BuildBlock();
            Build();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            Reload();
        }
    }

    private void Reload() {
        int ammoRequired = pd.magSize - pd.loadedAmmo;
        if (ammoRequired > pd.ammo) {
            pd.loadedAmmo += pd.ammo;
            pd.ammo = 0;
        } else {
            pd.loadedAmmo += ammoRequired;
            pd.ammo -= ammoRequired;
        }
    }

    private void Build() {
        Vector2 position = new Vector2(Mathf.Round(buildPoint.position.x) + 0.5f, Mathf.Round(buildPoint.position.y) + 0.5f);
        Instantiate(wallPrefab, position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Enemy")) {
            if (hp.TakeDamage(collision.gameObject.GetComponent<Enemy>().damage))
                Die();
        }
        if (collision.collider.CompareTag("Projectile"))
        {
            if (hp.TakeDamage(collision.gameObject.GetComponent<Projectile>().damage))
                Die();
        }
        if (collision.collider.CompareTag("Dung"))
        {
            if (hp.TakeDamage(collision.gameObject.GetComponent<Proj_Dung>().damage))
                Die();
        }
        if (collision.collider.CompareTag("Stag"))
        {
            if (hp.TakeDamage(collision.gameObject.GetComponent<StagBeetle>().hornDamage))
                Die();
        }
        if (collision.collider.CompareTag("Rhino Beetle")) {
            GetComponent<PlayerMovement>().KnockBack();
            if (hp.TakeDamage(collision.gameObject.GetComponent<Enemy>().damage))
                Die();
        }
    }

    private void Die() {
        GameManager_.GetGameManager().EndGame();
    }
}
