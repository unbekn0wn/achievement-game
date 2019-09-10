/**
 * Not much happening here, the collision calls the completion action
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementCollectible : AchievementBase
{
    public const Type AchievementType = Type.Major;
    Collectible[] collectibles;

    private void Awake()
    {
        collectibles = GetComponentsInChildren<Collectible>();
    }

    public void CollectedItem()
    {
        foreach(Collectible c in collectibles)
        {
            if (c.Collected == false)
                return;

            if(!Completed)
                OnComplete();
        }
    }
}
