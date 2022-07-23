using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectStagBeetle : Insect {
    public float wallDamageMultiplier = 1.5f;

    protected override void OnCollide(Collider2D coll) {
        base.OnCollide(coll);
        if (coll.CompareTag("Wall")) {
            Damage dmg = new Damage {
                damageAmount = (int)(damage * wallDamageMultiplier),
                pushForce = pushForce,
                origin = transform.position
            };
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
