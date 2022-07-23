using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour {
    public ContactFilter2D filter;

    private Collider2D collider_;
    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start() {
        collider_ = GetComponent<Collider2D>();
    }

    protected virtual void Update() {
        CheckCollision();
    }

    protected virtual void CheckCollision() {
        collider_.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D _collider) {
        //Debug.Log(gameObject.name + ".OnCollide(Collider2D collider");   
    }

    protected virtual void Despawn() {
        Destroy(gameObject);
    }
}
