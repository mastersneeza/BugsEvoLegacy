using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InsectSpawner : MonoBehaviour {
    public GameObject[] insects;
    public float defaultTime = 2f;
    public float timeBtwSpawn = 0f;
    public GameObject insectsContainer;

    public Bounds bounds;
    private Vector2 insectPos;

    private void Start() {
        bounds = new Bounds(transform.position, transform.localScale);
    }

    private void Update() {
        if (timeBtwSpawn <= 0) {
            SpawnInsect();
            timeBtwSpawn = defaultTime;
        }
        else {
            timeBtwSpawn -= Time.deltaTime;
        }
    }

    private void SpawnInsect() {
        float xCoord, yCoord;
        xCoord = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        yCoord = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        insectPos = new Vector2(xCoord, yCoord);
        Instantiate(insects[UnityEngine.Random.Range(0, insects.Length)], insectPos, Quaternion.identity, insectsContainer.transform);
        Debug.Log(gameObject.name + ": spawn");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            
        }
     }
}
