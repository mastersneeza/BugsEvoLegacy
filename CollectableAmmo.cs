using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAmmo : Collectable {
    public int ammoCount = 5;

    protected override void OnCollide(Collider2D _collider) {
        base.OnCollide(_collider);
        if (_collider.CompareTag("Insect")) {
            Despawn();
        }
    }

    protected override void OnCollect() {
        collected = true;
        //Grant player 'ammoCount' ammo
        Gun.ammo += ammoCount;
        GameManager.ShowText("+" + ammoCount.ToString() + " ammo", 48, Color.white, transform.position, Vector3.up * 50, 1.5f);
        Despawn();
    }
}
