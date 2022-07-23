using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public float despawnTime = 10f;

    private void Start() {
        Invoke("Despawn", despawnTime);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        Pickup(collision);
    }

    private void Despawn() {
        Destroy(gameObject);
    }

    private void Pickup(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
