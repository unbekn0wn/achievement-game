using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public bool Collected;

    private void Awake()
    {
        Collected = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            Collected = true;

            AchievementCollectible achievement = GetComponentInParent<AchievementCollectible>();
            achievement.CollectedItem();
        }
    }
}
