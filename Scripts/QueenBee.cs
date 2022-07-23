using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBee : Enemy {
    public GameObject beePrefab;
    public Transform[] spawnPoints;

    private void Start() {
        hp = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        timeBtwShots = startTimeBtwShots;
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

    protected new void TimedAttack() {
        Instantiate(beePrefab, spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Projectile") {
            if (hp.TakeDamage(collision.gameObject.GetComponent<Projectile>().damage)) {
                PlayerData1.GetPlayerData().KillEnemy(xpValue);
                PlayerData1.GetPlayerData().queensKilled++;
                Destroy(gameObject);
            }
        }
        else {
            Vector3 euler = transform.eulerAngles;
            euler.z = -euler.z;
            transform.eulerAngles = euler;
        }
    }
}
