using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleImageSix : MonoBehaviour
{

    Image myImage;
    void OnEnable()
    {
        myImage = GameObject.Find("unlockImage6").GetComponent<Image>();
        int totalDeaths = PlayerPrefs.GetInt("TotalDeaths");
        //totalScore = 0;
        if (totalDeaths >= 10)
        {
            myImage.color = Color.green;
        }
        else
        {
            myImage.color = Color.red;
        }

    }
}