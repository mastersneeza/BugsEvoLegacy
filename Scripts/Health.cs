using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField]
    public float hp;
    [SerializeField]
    public float maxHp;
    public HealthBar hpBar;

    private void Start() {
        hp = maxHp;
        hpBar.SetHealth(hp, maxHp);
    }

    public bool TakeDamage(float damage) { //Returns if the being is dead or not
        hp -= damage;
        hpBar.SetHealth(hp, maxHp);

        if (hp <= 0) {
            return true;
        }
        return false;
    }

    public int HealDamage(float health) { //Returns how much hp was overflowed
        hp += health;
        int overflowed = 0;
        if (hp > maxHp) {
            overflowed = (int) hp - (int) maxHp;
            hp = maxHp;

        }
        hpBar.SetHealth(hp, maxHp);
        return overflowed;
    }
}
