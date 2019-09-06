/**
 * The Player script holds information of the player and controls its movement.
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int JumpPower;
    public int PlayerSpeed;
    public bool Alive;

    private bool jumping;
    private new Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Alive = true;
    }

    void Update()
    {
        //If spacebar is pressed, jump by adding force, also disable jumping
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            rigidbody2D.AddForce(Vector2.up * JumpPower * 10);
            jumping = true;
        }

        Vector2 finalVelocity = new Vector2(Vector2.right.x * Input.GetAxis("Horizontal") * PlayerSpeed, rigidbody2D.velocity.y);
        rigidbody2D.velocity = finalVelocity;
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        //Check if the Player is coming down from the top hitting the ground, then enable jumping again
        if (col.transform.tag == "Ground" && col.GetContact(0).normal.y >= 0.5f)
        {
            jumping = false;
        }
    }
}
