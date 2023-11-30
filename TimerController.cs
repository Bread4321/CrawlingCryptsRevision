using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public Text timeCounter;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;

    private int endScene = 6;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeCounter.text = "00:00.00";
        timerGoing = false;
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            /*
            if (elapsedTime > 120)
            {
                EndTimer();
                SceneManager.LoadScene(endScene);
            }
            */
            if (SceneManager.GetActiveScene().buildIndex == 5) // set to buildindex of win scene
            {
                EndTimer();
            }
            
            yield return null;
        }
    }
}

/*
 *  In player movement controller add:
 * 
 * private bool timerStartedThisGame = false;
 * 
 *  Under Update() add:
 *  
 *  if (Input.anyKeyDown && timerStartedThisGame == false)
         {
            TimerController.instance.BeginTimer();
            timerStartedThisGame = true;
         }
*/
