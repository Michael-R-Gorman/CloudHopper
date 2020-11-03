using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleImageFive : MonoBehaviour
{

    Image myImage;
    void OnEnable()
    {
        myImage = GameObject.Find("unlockImage5").GetComponent<Image>();
        int totalDeaths = PlayerPrefs.GetInt("TotalDeaths");
        //totalScore = 0;
        if (totalDeaths >= 5)
        {
            myImage.color = Color.green;
        }
        else
        {
            myImage.color = Color.red;
        }

    }
}