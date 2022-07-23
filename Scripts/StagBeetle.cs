using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagBeetle : Enemy {
    public float hornDamage = 4;
    public GameObject horn;

    private void Start() {
        hp = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        timeBtwShots = startTimeBtwShots;
    }
    protected new void TimedAttack() {

    }

    private void Update() {
        direction = FindClosestPlayer().position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        if (timeBtwShots <= 0)
        {
            TimedAttack();
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        MoveEnemy(movement);
    }

}
