using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectQueenBee : Insect {
    public GameObject beePrefab;
    public Transform[] spawnPoints;
    public float timeBetweenSpawn = 3.5f;

    protected override void Start() {
        base.Start();
        StartCoroutine(SpawnBees());
    }

    private IEnumerator SpawnBees() {
        while (true) {
            yield return new WaitForSeconds(timeBetweenSpawn);
            Instantiate(beePrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }
    }

    protected override void Die(string shooter) {
        Player.FindPlayer(shooter).queensKilled++;
        base.Die(shooter);
    }

}
