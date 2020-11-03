using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TrophySeven : MonoBehaviour
{
    public int totalScore;

    Text myText;

    void OnEnable()
    {
        myText = GetComponent<Text>();
        totalScore = PlayerPrefs.GetInt("TotalScore");
        //totalScore = 0;

        if (totalScore >= 1000)
        {
            myText.text = "Unlocked!";

        }
        else
        {
            myText.text = "Locked";

        }


    }
}
