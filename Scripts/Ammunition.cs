using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : Collectible {
    public int ammoCount = 5;
    private void OnTriggerEnter2D(Collider2D collision) {
        Pickup(collision);
        Destroy(gameObject);
    }

    private void Pickup(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Destroy(gameObject);
            PlayerData1.GetPlayerData().AddAmmo(ammoCount);
        }
    }
}
