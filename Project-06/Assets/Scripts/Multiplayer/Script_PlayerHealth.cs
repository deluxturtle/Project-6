using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Script_PlayerHealth : NetworkBehaviour {

    float seconds = 0;
    float health = 100;

	
	// Update is called once per frame
	void Update () {
        seconds += Time.deltaTime;
        if(seconds > 2)
        {
            seconds = 0;
            Debug.Log(health);
        }
	}
}
