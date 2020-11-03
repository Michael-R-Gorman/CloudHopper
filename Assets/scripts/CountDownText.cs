using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CountDownText : MonoBehaviour {
    Text countDown;
    public delegate void CountDownFinished();
    public static event CountDownFinished OnCountDownFinished;

    void OnEnable() {   //  is called when we set CountDown page to be active
        countDown = GetComponent<Text>();
        countDown.text = "3";
        StartCoroutine("Countdown");
    }

    IEnumerator Countdown() {
        int count = 3;
        for (int i = 0; i < count; i++) {
            countDown.text = (count - i).ToString();
            yield return new WaitForSeconds(1); // Wait a second
        }
        OnCountDownFinished();  //  event sent to GameManager
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
