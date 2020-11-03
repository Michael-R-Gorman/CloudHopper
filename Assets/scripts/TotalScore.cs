using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TotalScore : MonoBehaviour
{

    Text totalScore;

    void OnEnable()
    {
        totalScore = GetComponent<Text>();
        totalScore.text = "Total Score: " + PlayerPrefs.GetInt("TotalScore").ToString();
    }
}
