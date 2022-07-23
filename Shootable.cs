using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : Collidable {
    public int damage = 1;
    public float force = 20f;
    public float knockBack = 2f;

    public GameObject explosionPrefab;

    protected override void Start() {
        base.Start();
        Invoke("Despawn", 5f);
    }

    protected override void Update() {
        base.Update();
        transform.Translate(Vector2.up * force * Time.deltaTime);
    }

    protected override void OnCollide(Collider2D _collider) {
        GameObject effect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(effect, 0.4f);
        Despawn();
    }

}
