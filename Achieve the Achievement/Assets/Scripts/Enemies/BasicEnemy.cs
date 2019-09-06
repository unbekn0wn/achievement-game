using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicEnemy : EnemyBase
{
    public int EnemySpeed;

    //FUCK THIS GREEN SCRIBBLE, this is the correct way
    Rigidbody2D rigidbody;

    //Getter and Setter for velocity, keep this only at 1 as this is mainly used to hold 1 or -1 or 0
    //Every time velocity gets set, the enemyspeed will take that velocity and multiply it by its base speed
    int velocity;
    public int Velocity
    {
        get
        {
            return velocity;
        }
        set
        {
            velocity = value;
            rigidbody.velocity = transform.right * velocity * EnemySpeed;
        }
    }

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Velocity = 1;
    }

    void Update()
    {
        //Direction of 45 degrees downleft and downright
        Vector2 leftAngle = (-transform.up + -transform.right).normalized;
        Vector2 rightAngle = (-transform.up + transform.right).normalized;

        //If going left check left angle, if going right check right
        if(Velocity == -1)
            //If there is no ground anymore go the other way.
            if (!CheckForGround(leftAngle))
            {
                Velocity *= -1;
                return;
            }

        if(Velocity == 1)
            if (!CheckForGround(rightAngle))
            {
                Velocity *= -1;
                return;
            }
    }

    //Raycast in an angle if it hits ground, if not return false
    bool CheckForGround(Vector2 angle)
    {
        ContactFilter2D contactFilter = new ContactFilter2D().NoFilter();
        List<RaycastHit2D> hitlist = new List<RaycastHit2D>();

        if (Physics2D.Raycast(transform.position, angle, contactFilter, hitlist, 1f) != 0)
            if (hitlist.Exists(i => i.transform.tag == "Ground") || hitlist.Exists(i => i.transform.tag == "Ground"))
                return true;

        return false;
    }

    //If it hits an enemy, turn around
    //If it hits a player and the player hits it from the top, kill the enemy. Otherwise kill the player
    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Enemy")
            Velocity *= -1;

        if (col.transform.tag == "Player")
        {
            if (col.GetContact(0).normal.y <= -0.5f)
            {
                GetComponentInParent<AchievementEnemies>().UpdateEnemies(this);
                col.rigidbody.velocity *= -2;
                Destroy(gameObject);
            }
            else
            {
                Velocity *= 1;
                KillPlayer();
            }
        }
    }
}
