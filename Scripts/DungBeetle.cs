using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungBeetle : Enemy {
    public float stopDistance;
    public float retreatDistance;

    private Shooter shooter;
    private void Start() {
        hp = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
        timeBtwShots = startTimeBtwShots;
    }

    protected new void TimedAttack() {
        shooter.Shoot();
    }

    private void FixedUpdate() {
        MoveEnemy(direction);
    }

    protected new void MoveEnemy(Vector2 direction) {
        //rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));

        if (Vector2.Distance(transform.position, FindClosestPlayer().position) > stopDistance) {
            rb.MovePosition((Vector2) transform.position + (direction * moveSpeed * Time.deltaTime));
        } else if (Vector2.Distance(transform.position, FindClosestPlayer().position) < retreatDistance)
        {
            rb.MovePosition((Vector2)transform.position + (direction * -moveSpeed * Time.deltaTime));

        }
    }
}
