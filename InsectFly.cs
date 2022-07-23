using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectFly : Insect {
    public float turnSpeed = 0.15f;

    protected override void Start() {
        base.Start();
        StartCoroutine(ChangeDirection());
    }

    protected override void Update() {
        CheckCollision();
    }

    protected override void MoveEnemy(Vector2 direction) {
        KnockBack();
        rb.MovePosition(transform.position + (transform.up * moveSpeed * Time.deltaTime));
    }

    private IEnumerator ChangeDirection() {
        while (true) {
            yield return new WaitForSeconds(turnSpeed);
            Vector3 euler = transform.eulerAngles;
            euler.z += Random.Range(-45f, 45f);
            transform.eulerAngles = euler;
        }
    }

    protected override void Die(string shooter) {
        Player.FindPlayer(shooter).fliesKilled++;
        base.Die(shooter);
    }
}
