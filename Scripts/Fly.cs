using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Enemy {

    public GameObject flybootsPrefab;
    public float flybootsDropChance = 0.05f;
    
    private void Start() {
        hp = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        timeBtwShots = startTimeBtwShots;
    }

    protected new void TimedAttack() {
        Vector3 euler = transform.eulerAngles;
        euler.z += Random.Range(-90f, 90f);
        transform.eulerAngles = euler;
    }

    private void Update() {
        if (timeBtwShots <= 0) {
            TimedAttack(); //Change direction
            timeBtwShots = startTimeBtwShots;
        }
        else {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        MoveEnemy();
    }

    protected void MoveEnemy() {
        rb.MovePosition(transform.position + (transform.up * moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Projectile") {
            if (hp.TakeDamage(collision.gameObject.GetComponent<Projectile>().damage)) {
                PlayerData1.GetPlayerData().KillEnemy(xpValue);
                if (Random.Range(0f, 1f) <= flybootsDropChance) {
                    Instantiate(flybootsPrefab, transform.position,Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
        else { //Turn around if we hit something
            Vector3 euler = transform.eulerAngles;
            euler.z = -euler.z;
            transform.eulerAngles = euler;
        }
    }

}
