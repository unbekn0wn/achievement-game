/**
 * Basically only manages the score, increments etc
 * Also has an action for other scripts to listen to when score updates
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static System.Action<ScoreManager> ScoreUpdate;

    public int StarterPoints;

    int currentPoints;

    public int CurrentPoints
    {
        get
        {
            return currentPoints;
        }
        set
        {
            currentPoints = value;

            if (currentPoints > ApplicationModel.HighScore)
                ApplicationModel.HighScore = currentPoints;

            ScoreUpdate?.Invoke(this);
        }
    }

    public Text ScoreText;

    public void AddPoints(int amount)
    {
        CurrentPoints += amount;
    }

    public void RemovePoints(int amount)
    {
        CurrentPoints -= amount;
        if (CurrentPoints == 0)
        {
            FindObjectOfType<Player>().Alive = false;
            SceneManager.LoadScene("GameOver");
        }
    }

    //Timer stuff, StartTimer just starts the timer, TimerRoutine is the actual timer.
    public void StartTimer()
    {
        StartCoroutine(TimerRoutine());
    }

    public IEnumerator TimerRoutine()
    {
        while (FindObjectOfType<Player>().Alive)
        {
            RemovePoints(1);
            yield return new WaitForSeconds(1);
        }
    }
}
