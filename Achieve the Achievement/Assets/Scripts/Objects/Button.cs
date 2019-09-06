using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public static System.Action<Button> ButtonPressed;
    public static System.Action<Button> ButtonReleased;

    public SpriteRenderer Number;
    public bool ActiveButton = false;

    float detectionRange = 10f;

    bool playerInRange;
    public bool PlayerInRange
    {
        get
        {
            return playerInRange;
        }
        set
        {
            playerInRange = value;
            Number.gameObject.SetActive(value);
        }
    }

    void Start()
    {
        playerInRange = false;
    }

    void Update()
    {
        Vector2 playerPos = FindObjectOfType<RoomManager>().Player.transform.position;
        PlayerInRange = (Vector2.Distance(playerPos, transform.position) <= detectionRange) ? true : false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Player")
            if(col.GetContact(0).normal.y == -1)
                ButtonPressed?.Invoke(this);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.transform.tag == "Player")
            ButtonReleased?.Invoke(this);
    }
}
