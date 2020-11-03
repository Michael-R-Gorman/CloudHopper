using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleImageFour : MonoBehaviour
{

    Image myImage;
    void OnEnable()
    {
        myImage = GameObject.Find("unlockImage4").GetComponent<Image>();
        int hiScore = PlayerPrefs.GetInt("HighScore");
        //totalScore = 0;
        if (hiScore >= 100)
        {
            myImage.color = Color.green;
        }
        else
        {
            myImage.color = Color.red;
        }

    }
}