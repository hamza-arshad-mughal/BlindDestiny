using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Bolt.Matchmaking;
using UdpKit;
using System;
using UnityEngine.UI;

public class MenuScripts : Bolt.GlobalEventListener
{
    public InputField roomidhost;
    public InputField roomidjoin;

    private void Start()
    {
    }
    public void StartServer()
    {
        BoltLauncher.StartServer();
    }
    public void StartClient()
    {
        BoltLauncher.StartClient();
    }

    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {
            BoltMatchmaking.CreateSession(
                sessionID: roomidhost.text,
                sceneToLoad: "SampleScene"
            );
        }
        else if (BoltNetwork.IsClient)
        {
            BoltMatchmaking.JoinSession(roomidjoin.text.ToString());
        }
    }


 

    public void generateroomID()
    {
        string id_full = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        string id_withoutsymbol = id_full.Substring(0, 15);
        string id = id_withoutsymbol.Substring(0, 2) + id_withoutsymbol.Substring(7, 2);
        roomidhost.text = id;
        GameObject.FindGameObjectWithTag("GAMEMANAGER").GetComponent<GAMEMANAGER>().roomIdHost = id;
    }
}
