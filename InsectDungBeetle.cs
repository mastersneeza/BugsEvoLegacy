using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectDungBeetle : Insect {
    public float stopDistance;
    public float retreatDistance;
    public float timeBtwShots = 4f;

    public GameObject firePoint;
    public GameObject dungPrefab;

    protected override void Start() {
        base.Start();
        StartCoroutine(ShootDung());
    }
    protected override void MoveEnemy(Vector2 direction) {
        KnockBack();
        if (Vector2.Distance(transform.position, FindClosestPlayer().position) > stopDistance) {
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
        else if (Vector2.Distance(transform.position, FindClosestPlayer().position) < retreatDistance) {
            rb.MovePosition((Vector2)transform.position + (direction * -moveSpeed * Time.deltaTime));

        }
    }

    private IEnumerator ShootDung() {
        while (true) {
            yield return new WaitForSeconds(timeBtwShots);
            Instantiate(dungPrefab, firePoint.transform.position, firePoint.transform.rotation);


        }
    }
}
