using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFirstAid : Collectable {
    public int healAmount = 3;

    protected override void OnCollide(Collider2D _collider) {
        if (_collider.CompareTag("Player")) {
            Player player = _collider.gameObject.GetComponent<Player>();
            OnCollect(player);
        }
    }

    protected void OnCollect(Player player) {
        collected = true;
        player.SendMessage("ReceiveHp", healAmount);
        GameManager.ShowText("+" + healAmount.ToString() + " hp", 48, Color.white, transform.position, Vector3.up * 50, 1.5f);
        Despawn();
    }
}
