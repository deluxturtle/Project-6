using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/// <summary>
/// Author: Andrew Seba
/// Description: Shoots a ray foward from the camera and will get a reference
/// of the game object hit, then sends a command to the server and find the
/// identity and apply damage amount.
/// </summary>
public class ScriptPlayerShoot : NetworkBehaviour {

    private int damage = 25;
    private float range = 200;
    [SerializeField]
    private Transform camTransform;

    private RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckifShooting();
	}

    void CheckifShooting()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Physics.Raycast(camTransform.TransformPoint(0, 0, 0.5f), camTransform.forward, out hit, range))
        {
            Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "Player")
            {
                string uIdentity = hit.transform.name;
                CmdTellServerWhoWasShot(uIdentity, damage);
            }
        }
    }

    [Command]
    void CmdTellServerWhoWasShot(string pIdentity, int pDamage)
    {
        GameObject go = GameObject.Find(pIdentity);
        go.GetComponent<Script_PlayerHealth>().DeductHealth(pDamage);
        //Apply damage to that player.
    }
}
