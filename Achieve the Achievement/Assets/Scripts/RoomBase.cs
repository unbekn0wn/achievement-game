/**
 * The RoomBase is responsible for holding the point value of the majorachievement.
 * Getting all the variables used by other scripts like spawnpoints, teleporters etc.
 * Listeners: OnComplete (When this MajorAchievement completes, add score and activate the teleporter)
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBase : MonoBehaviour
{
    public int AchievementPoints;

    AchievementBase majorAchievement;
    Teleporter teleporter;

    //Spawnpoint is only set at awaking so setting is made private to prevent accidental overrides.
    SpawnPoint spawnPoint;
    public SpawnPoint SpawnPoint
    {
        get;
        private set;
    }

    void Awake()
    {
        //Start listening to the OnComplete event of all achievementbases
        AchievementBase.AchievementCompleted += OnComplete;

        //Get its own components to use
        majorAchievement = GetComponentInChildren<AchievementBase>();
        teleporter = GetComponentInChildren<Teleporter>();
        SpawnPoint = GetComponentInChildren<SpawnPoint>();
    }

    void OnComplete(AchievementBase baseobj)
    {
        //If the OnComplete event is coming from this MajorAchievement, call the RoomManager and add points
        //Do I really want to let this script handle this kind of active operations???
        if (baseobj == majorAchievement)
        {
            teleporter.Activated = true;
            FindObjectOfType<RoomManager>()._ScoreManager.AddPoints(AchievementPoints);
        }
    }
}
