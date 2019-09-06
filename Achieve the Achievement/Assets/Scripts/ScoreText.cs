/**
 * Used on text that shows the score (Time)
 * Listener: UpdateScore from ScoreManager
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    Text scoreText;
    void Start()
    {
        ScoreManager.ScoreUpdate += UpdateScore;
        scoreText = GetComponent<Text>();
    }

    void UpdateScore(ScoreManager baseobj)
    {
        scoreText.text = "Score: " + baseobj.CurrentPoints;
    }
}
