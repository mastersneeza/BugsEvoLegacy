using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dung : Shootable {
    protected override void OnCollide(Collider2D _collider) {
        if (_collider.tag == "Player") {
            Damage dmg = new Damage {
                damageAmount = damage,
                pushForce = knockBack,
                origin = transform.position
            };
            _collider.SendMessage("ReceiveDamage", dmg);
        }

        base.OnCollide(_collider);

    }
}
