using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : MonoBehaviour {
    public float healAmount;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
        {
            int overflowedHp = collision.gameObject.GetComponent<Health>().HealDamage(healAmount);
            PlayerData1.GetPlayerData().overflowedHp += overflowedHp;
            Destroy(gameObject);
        }
    }

}
