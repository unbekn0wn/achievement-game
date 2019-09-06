/**
 * Mainly handles the completion checks
 * The enemies itself are inside this prefab and this only gets all enemies in it and checks if they are alive or not
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEnemies : AchievementBase
{
    public const Type AchievementType = Type.Major;

    List<EnemyBase> enemies = new List<EnemyBase>();

    void Start()
    {
        GetComponentsInChildren<EnemyBase>(false, enemies);
    }

    //Generally used when an enemy dies, it removes it from the list and checks if the list is zero. If it is, the achievement is completed.
    //I wonder if I can better let the enemy handle this part ¯\_(ツ)_/¯
    public void UpdateEnemies(EnemyBase enemy)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<EnemyBase>() == enemy)
            {
                enemies.RemoveAt(i);

                if(enemies.Count == 0)
                {
                    OnComplete();
                }
            }
        }      
    }
}
