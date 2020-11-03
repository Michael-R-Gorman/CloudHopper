using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TrophyThree : MonoBehaviour
{
    public int totalScore;

    Text myText;

    void OnEnable()
    {
        myText = GetComponent<Text>();
        totalScore = PlayerPrefs.GetInt("HighScore");
        //totalScore = 0;

        if (totalScore >= 50)
        {
            myText.text = "Unlocked!";

        }
        else
        {
            myText.text = "Locked";

        }


    }
}
