using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    private Health hp;
    // Start is called before the first frame update
    private void Start() {
        hp = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Projectile") {
            if (hp.TakeDamage(collision.gameObject.GetComponent<Projectile>().damage)) {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Enemy") {
            if (hp.TakeDamage(collision.gameObject.GetComponent<Enemy>().damage)) {
                Destroy(gameObject);
            }
        if (collision.gameObject.tag == "Rhino Beetle") {
                if (hp.TakeDamage(collision.gameObject.GetComponent<Enemy>().damage)) {
                    Destroy(gameObject);
                }
            }
        }
        if (collision.gameObject.tag == "Dung")
        {
            if (hp.TakeDamage(collision.gameObject.GetComponent<Proj_Dung>().damage))
            {
                Destroy(gameObject);
            }
        }
        if (collision.collider.CompareTag("Stag"))
        {
            if (hp.TakeDamage(collision.gameObject.GetComponent<StagBeetle>().hornDamage * 1.5f))
            {
                Destroy(gameObject);
            }
        }
    }
}
