using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RunningScore : MonoBehaviour
{

    Text runningScore;

    void OnEnable()
    {
        runningScore = GetComponent<Text>();
        runningScore.text = "Score: " + PlayerPrefs.GetInt("RunningScore").ToString();
    }
}
