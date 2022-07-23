using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_Dung : Projectile {
    private void Start() {
        damage = 3;
        timeBeforeDespawning = 3f;
        rb = GetComponent<Rigidbody2D>();
        Invoke("DestroyProjectile", timeBeforeDespawning);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.collider.CompareTag("Enemy"))
            ApplyEffect(collision.gameObject);
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.4f);
        Destroy(gameObject);
    }
}
