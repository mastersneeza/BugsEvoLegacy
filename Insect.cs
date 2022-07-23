using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : MovingEntity {
    public float moveSpeed = 3f;
    public int xpValue = 1;
    public int damage;
    public int pushForce;
    public int minimumLevel = 1; //Minimum level the player must be before spawning

    protected bool collidingWithPlayer = false;

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        Vector2 direction = FindClosestPlayer().position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        base.Update();
    }

    protected void ReceiveDamage(List<object> data) {
        Damage dmg = (Damage) data[0];
        string shooterName = data[1].ToString();
        hp -= dmg.damageAmount;
        pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

        GameManager.ShowText("-" + dmg.damageAmount + " hp", 48, Color.red, transform.position, Vector3.up * 50, 1.5f);

        if (hp <= 0) {
            hp = 0;
            Die(shooterName);
        }
    }

    protected override void OnCollide(Collider2D coll) {
        if (coll.CompareTag("Player")) {
            Damage dmg = new Damage {
                damageAmount = damage,
                pushForce = pushForce,
                origin = transform.position
            };
            coll.SendMessage("ReceiveDamage", dmg);
            collidingWithPlayer = true;
        }
    }

    protected virtual void FixedUpdate() {
        MoveEnemy(movement);
    }

    protected virtual void MoveEnemy(Vector2 direction) {
        KnockBack();
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    
    protected virtual void Die(string shooter) {
        Debug.Log("die");
        Player.FindPlayer(shooter).KillInsect(xpValue);
        GameManager.ShowText("+" + xpValue.ToString() + "xp", 48, new Color(1, 0.5f, 1), transform.position, Vector3.up * 100, 1f);
        Destroy(gameObject);
    }
}
