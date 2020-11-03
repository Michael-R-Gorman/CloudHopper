using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TrophyFive : MonoBehaviour
{
    public int totalDeaths;

    Text myText;

    void OnEnable()
    {
        myText = GetComponent<Text>();
        totalDeaths = PlayerPrefs.GetInt("TotalDeaths");
        //totalScore = 0;

        if (totalDeaths >= 5)
        {
            myText.text = "Unlocked!";

        }
        else
        {
            myText.text = "Locked";

        }


    }
}
