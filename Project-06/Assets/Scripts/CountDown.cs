using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDown : MonoBehaviour 
{
    [Tooltip("Label countdown appears in.")]
    public Text countdownText; // Text object countdown appears on
    [Tooltip("Panel containing Respawn GUI.")]
    public GameObject respawnPanel; // Panel with respawning GUI
    [Tooltip("How long it takes player to respawn.")]
    [Range(1, 10)]
    public int countdownTime; // Designer-editable countdown time

    int count; // used by script to count down
    bool countdown = false; // flag for if a countdown is occurring

	void Awake() 
    {
        // Debugging code
        // Initiate a respawn countdown
        BeginCountdown();
	}

	void Update () 
    {
        // Debugging Code
        // When not counting down and space is pressed, begin counting down
	    if(!countdown && Input.GetKeyDown(KeyCode.Space))
        {
            BeginCountdown();
        }
	}

    // Begin Countdown sets the flag for a countdown occurring and
    //    starts the countdown.
    // During a countdown, the respawning panel GUI is visible.
    void BeginCountdown()
    {
        countdown = true;
        respawnPanel.SetActive(true);
        count = 5;
        InvokeRepeating("CountdownTimer", 0f, 1.0f);
    }

    // CountdownTimer shows the timer on screen
    // CountdownTimer resets when count is 0.
    // When countdown ends, hide the respawning panel GUI.
    void CountdownTimer()
    {
        countdownText.text = count.ToString();
        count--;
        if(count < 0)
        {
            count = countdownTime;
            countdown = false;
            respawnPanel.SetActive(false);
            CancelInvoke("CountdownTimer");
        }
    }
}
