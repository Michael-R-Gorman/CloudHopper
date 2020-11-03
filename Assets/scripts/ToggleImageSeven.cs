using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleImageSeven : MonoBehaviour
{

    Image myImage;
    void OnEnable()
    {
        myImage = GameObject.Find("unlockImage7").GetComponent<Image>();
        int totalScore = PlayerPrefs.GetInt("TotalScore");
        //totalScore = 0;
        if (totalScore >= 1000)
        {
            myImage.color = Color.green;
        }
        else
        {
            myImage.color = Color.red;
        }

    }
}