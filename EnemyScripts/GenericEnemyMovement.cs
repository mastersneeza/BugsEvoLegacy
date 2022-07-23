using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyMovement : MonoBehaviour {
    public float rotationOffset = -90f;
    public float moveSpeed;

    protected Rigidbody2D rb;
    protected Vector3 direction;
    protected float angle;
    protected Vector2 movement;
    protected Transform FindClosestPlayer() {
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
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update() {
        direction = FindClosestPlayer().position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate() {
        MoveEnemy(movement);
    }

    protected void MoveEnemy(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
