using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TrophyTwo : MonoBehaviour
{
    public int totalScore;

    Text myText;

    void OnEnable()
    {
        myText = GetComponent<Text>();
        totalScore = PlayerPrefs.GetInt("TotalScore");
        //totalScore = 0;

        if (totalScore >= 100)
        {
            myText.text = "Unlocked!";

        }
        else
        {
            myText.text = "Locked";

        }


    }
}
