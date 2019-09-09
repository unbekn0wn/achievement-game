/**
 * The teleporter... well handles the teleporter, when it activates (Or deactivates) it changes its sprites.
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Sprite TeleporterOn;
    public Sprite TeleporterOff;

    private bool activated;
    public bool Activated
    {
        get
        {
            return activated;
        }
        set
        {
            SpriteRenderer renderer = GetComponentInParent<SpriteRenderer>();
            activated = value;

            if (value == true)
                renderer.sprite = TeleporterOn;
            else
                renderer.sprite = TeleporterOff;
        }
    }

    void Awake()
    {
        Activated = false;
    }

    //When the player enters an activated teleporter, go to the next level.
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player" && Activated)
        {
            StartCoroutine(FindObjectOfType<RoomManager>().GoToNextLevel());
            //FindObjectOfType<RoomManager>().GoToNextLevel();
        }
    }
}
