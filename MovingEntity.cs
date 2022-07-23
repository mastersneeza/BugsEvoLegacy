using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : Entity {
    protected Rigidbody2D rb;
    protected Vector2 movement;

    protected override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Update() {
        base.Update();
    }

    protected virtual void KnockBack() {
        movement += (Vector2)pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);
    }
}
