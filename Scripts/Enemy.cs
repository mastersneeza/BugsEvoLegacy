using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float rotationOffset = -90f;
    public float moveSpeed;
    public int xpValue;

    protected Rigidbody2D rb;
    protected Vector3 direction;
    protected float angle;
    protected Vector2 movement;
    public float damage = 1;
    protected Health hp;
    protected float timeBtwShots;
    public float startTimeBtwShots;

    protected Transform FindClosestPlayer() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Debug.Log(go.name);
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest.transform;
    }

    private void Start() {
        hp = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        timeBtwShots = startTimeBtwShots;
    }
    protected void TimedAttack() {

    }

    private void Update() {
        direction = FindClosestPlayer().position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        if (timeBtwShots <= 0) {
            TimedAttack();
            timeBtwShots = startTimeBtwShots;
        }
        else {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        MoveEnemy(movement);
    }

    protected void MoveEnemy(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Projectile") {
            if (hp.TakeDamage(collision.gameObject.GetComponent<Projectile>().damage)) {
                PlayerData1.GetPlayerData().KillEnemy(xpValue);
                Destroy(gameObject);
            }
        }
    }

}
