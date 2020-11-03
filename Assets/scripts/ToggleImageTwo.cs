using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleImageTwo : MonoBehaviour
{

    Image myImage;
    void OnEnable()
    {
        myImage = GameObject.Find("unlockImage2").GetComponent<Image>();
        int totalScore = PlayerPrefs.GetInt("TotalScore");
        //totalScore = 0;
        if (totalScore >= 100)
        {
            myImage.color = Color.green;
        }
        else
        {
            myImage.color = Color.red;
        }

    }
}