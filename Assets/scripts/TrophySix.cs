using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TrophySix : MonoBehaviour
{
    public int totalDeaths;

    Text myText;

    void OnEnable()
    {
        myText = GetComponent<Text>();
        totalDeaths = PlayerPrefs.GetInt("TotalDeaths");
        //totalScore = 0;

        if (totalDeaths >= 10)
        {
            myText.text = "Unlocked!";

        }
        else
        {
            myText.text = "Locked";

        }


    }
}
