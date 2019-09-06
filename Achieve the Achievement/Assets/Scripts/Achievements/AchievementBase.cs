/**
 * AchievementBase is the base of all achievement, it has a Type, completed state and Action for completion
 **/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AchievementBase : MonoBehaviour
{
    public static System.Action<AchievementBase> AchievementCompleted;

    public enum Type { Major, Minor };
    public bool Completed;
    //public int Points;

    void Awake()
    {
        Completed = false;
    }

    public virtual void OnComplete()
    {
        Completed = true;
        AchievementCompleted?.Invoke(this);
    }
}
