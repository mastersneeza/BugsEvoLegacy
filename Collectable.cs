using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable {
    protected bool collected = false;
    public float timeBeforeDespawn = 12f;


    protected override void Start() {
        base.Start();
        Invoke("Despawn", timeBeforeDespawn);
    }

    protected override void OnCollide(Collider2D _collider) {
        if (_collider.CompareTag("Player")) {
            OnCollect();
        }
    }

    protected virtual void OnCollect() {
        collected = true;
    }
}
