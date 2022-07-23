using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Shootable {
    public string shooter;

    protected override void OnCollide(Collider2D _collider) {
        if (_collider.tag == "Insect") {
            Damage dmg = new Damage {
                damageAmount = damage,
                pushForce = knockBack,
                origin = transform.position
            };
            List<object> data = new List<object>();
            data.Add(dmg);
            data.Add(shooter);
            Debug.Log(data.ToString());
            _collider.SendMessage("ReceiveDamage", data);
        }

        base.OnCollide(_collider);

    }
}
