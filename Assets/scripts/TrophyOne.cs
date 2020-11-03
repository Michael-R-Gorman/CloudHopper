using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TrophyOne : MonoBehaviour
{
    public GameObject trophyOne;
    public int totalScore;

    Text myText;

    void OnEnable()
    {
        myText = GetComponent<Text>();
        totalScore = PlayerPrefs.GetInt("TotalScore");
        //totalScore = 0;

        if (totalScore >= 10)
        {
            myText.text = "Unlocked!";
           
        }
        else
        {
            myText.text = "Locked";
            
        }
        
        
    }
}
