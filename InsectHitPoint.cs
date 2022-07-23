using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectHitPoint : Collidable {
    public int damage;
    public float pushForce;

    protected override void OnCollide(Collider2D _collider) {
        if (_collider.CompareTag("Player")) {
            Damage dmg = new Damage {
                damageAmount = damage,
                pushForce = pushForce,
                origin = transform.position
            };
            _collider.SendMessage("ReceiveDamage", dmg);
        }
    }
}
