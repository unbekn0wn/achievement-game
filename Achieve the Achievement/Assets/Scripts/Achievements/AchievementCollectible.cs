/**
 * Not much happening here, the collision calls the completion action
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementCollectible : AchievementBase
{
    public const Type AchievementType = Type.Major;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            AchievementBase achievement = GetComponentInParent<AchievementBase>();
            if (!achievement.Completed)
                achievement.OnComplete();
        }
    }
}
