using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    void Start()
    {
        Text txt = GetComponent<Text>();
        txt.text = "Your highest room is: " + ApplicationModel.HighRoom;
    }
}
