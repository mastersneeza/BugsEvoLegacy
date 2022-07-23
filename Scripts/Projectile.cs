using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public GameObject hitEffect;
    public float damage;
    public float timeBeforeDespawning = 5f;
    public float speed;

    protected Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        Invoke("DestroyProjectile", timeBeforeDespawning);
    }

    private void DestroyProjectile() {
        Destroy(gameObject);
    }

    private void Update() {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void ApplyEffect(GameObject obj) {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        ApplyEffect(collision.gameObject);
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.4f);
        Destroy(gameObject);
    }
}
