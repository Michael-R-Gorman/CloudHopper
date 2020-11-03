using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TotalDeaths : MonoBehaviour
{

    Text totalDeaths;

    void OnEnable()
    {
        totalDeaths = GetComponent<Text>();
        totalDeaths.text = "Total Deaths: " + PlayerPrefs.GetInt("TotalDeaths").ToString();
    }
}
