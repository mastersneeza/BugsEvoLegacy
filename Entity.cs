using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    public int hp;
    public int maxHp = 20;
    public float pushRecoverySpeed = 0.2f;

    public ContactFilter2D filter;

    protected Collider2D collider_;
    protected Collider2D[] hits = new Collider2D[10];

    protected Vector3 pushDirection;

    protected virtual Transform FindClosestPlayer() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest.transform;
    }

    protected virtual void Start() {
        hp = maxHp;
        collider_ = GetComponent<Collider2D>();
    }

    protected virtual void ReceiveDamage(Damage dmg) {
        hp -= dmg.damageAmount;
        pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

        GameManager.ShowText("-" + dmg.damageAmount + " hp", 48, Color.red, transform.position, Vector3.up * 50, 1.5f);

        if (hp <= 0) {
            hp = 0;
            Die();
        }
    }

    protected virtual void ReceiveHp(int amount) {
        hp += amount;
        if (hp > maxHp) {
            hp = maxHp;
        }
    }

    protected virtual void Die() {
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

    protected virtual void OnCollide(Collider2D coll) {
        
    }
}
