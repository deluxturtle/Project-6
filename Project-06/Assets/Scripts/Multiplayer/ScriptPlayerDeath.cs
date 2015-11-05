using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class ScriptPlayerDeath : NetworkBehaviour {

    private Script_PlayerHealth healthScript;
    private Image crossHairImage;

    // Use this for initialization
    void Start () {

        crossHairImage = GameObject.Find("SimpleCrosshairImage").GetComponent<Image>();
        healthScript = GetComponent<Script_PlayerHealth>();
        healthScript.EventDie += DisablePlayer;
    }
	
    void OnDisable()
    {
        healthScript.EventDie -= DisablePlayer;
    }

    void DisablePlayer()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        GetComponent<ScriptPlayerShoot>().enabled = false;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach(Renderer rend in renderers)
        {
            rend.enabled = false;
        }

        healthScript.isDead = true;

        if (isLocalPlayer)
        {
            crossHairImage.enabled = false;
            //Respawn Button Needs to Appear.
        }
    }
}
