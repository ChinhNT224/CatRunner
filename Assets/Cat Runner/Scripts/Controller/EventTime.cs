using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTime : MonoBehaviour
{
    public Text scoreInGameText;
    private float scoreTime = 0f;
    private int scoreInTime = 0;
    void Start()
    {
        
    }

    void Update()
    {
        scoreTime += Time.deltaTime;
        if (scoreTime >= 0.1f)
        {
            scoreInTime += 1;
            scoreTime -= 0.1f;
        }
        scoreInGameText.text = scoreInTime.ToString();
    }
}
