using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Text ScoreText;

    float elapsedTime = 0.0f;
    Boolean timerRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            ScoreText.text = ((int)elapsedTime).ToString();
        }
    }

    public void StopGameTimer()
    {
        timerRunning = false;
    }
}
