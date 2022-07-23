using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f;
    public float rotationOffset = -90f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 mousePosition;
    private Vector2 lookDirection;
    private float angle;

    public void AddSpeed(float amount) {
        moveSpeed += amount;
    }

    public void KnockBack() {
        rb.AddForce(-transform.up * moveSpeed * 1000);
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //Get input for movement

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Get mouse's position in world coordinates
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime); //Move player

        lookDirection = mousePosition - rb.position; //Get the vector the player is looking at
        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + rotationOffset; //Convert the vector into an angle from the x-axis and adjust using rotationOffset
        rb.rotation = angle; //Set the player's angle
    }
}
