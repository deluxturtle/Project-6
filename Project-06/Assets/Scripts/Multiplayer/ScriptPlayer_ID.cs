using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ScriptPlayer_ID : NetworkBehaviour {

    [SyncVar]
    private string playerUniqueID;
    private NetworkInstanceId playerNetID;
    private Transform myTransform; 

    public override void OnStartLocalPlayer()
    {
        GetNetIdentity();
        SetIdentity();
    }

    void Awake()
    {
        myTransform = transform;
    }

    void Update()
    {
        if(myTransform.name == "" || myTransform.name == "Player(Clone)")
        {
            SetIdentity();
        }
    }

    [Client]
    void GetNetIdentity()
    {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyIdentity(MakeUniqueID());
    }

    void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            myTransform.name = playerUniqueID;
        }
        else
        {
            myTransform.name = MakeUniqueID();
        }
    }

    string MakeUniqueID()
    {
        string uniqueName = "Player " + playerNetID.ToString();
        return uniqueName;
    }

    [Command]
    void CmdTellServerMyIdentity(string pName)
    {
        playerUniqueID = pName;
    }
}
