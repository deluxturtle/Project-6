using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Author: Andrew Seba
/// Description: Controls health updates
/// </summary>
public class Script_PlayerHealth : NetworkBehaviour {

    float seconds = 0;
    [SyncVar(hook = "OnHealthChange")]
    private int health = 100;
    private Text healthText;

    private bool shouldDie = false;
    public bool isDead = false;

    public delegate void DieDelegate();
    public event DieDelegate EventDie;

    void Start()
    {
        healthText = GameObject.Find("Health Text").GetComponent<Text>();
        SetHealthText();
    }
	
	// Update is called once per frame
	void Update () {
        CheckCondition();
        SetHealthText();
    }

    void CheckCondition()
    {
        if(health <= 0 && !shouldDie && !isDead)
        {
            shouldDie = true;
        }

        if(health <= 0 && shouldDie)
        {
            if(EventDie != null)
            {
                EventDie();
            }

            shouldDie = false;
        }
    }

    void SetHealthText()
    {
        if (isLocalPlayer)
        {
            healthText.text = "Health: " + health.ToString();
        }
    }

    public void DeductHealth(int dmg)
    {
        health -= dmg;
    }

    void OnHealthChange(int hlth)
    {
        health = hlth;
        SetHealthText();
    }
}
