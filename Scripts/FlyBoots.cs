using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBoots : Collectible {
    public float speedBoost = 1f;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerMovement>().AddSpeed(speedBoost);
            Destroy(gameObject);
        }
    }
}
