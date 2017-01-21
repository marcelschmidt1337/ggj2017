using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestServer : NetworkManager
{
    private int worldClientId;

    // Use this for initialization
    void Start()
    {
        StartServer();
        NetworkServer.RegisterHandler(MsgType.Highest + 1, OnStartRowing);
        NetworkServer.RegisterHandler(MsgType.Highest + 2, OnStopRowing);
        NetworkServer.RegisterHandler((short)CustomMsgType.RegisterView, (netMsg) =>
       {
           worldClientId = netMsg.conn.connectionId;
       });
    }

    private void OnStopRowing(NetworkMessage netMsg)
    {
    }

    private void OnStartRowing(NetworkMessage netMsg)
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
