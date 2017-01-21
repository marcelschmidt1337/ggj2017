using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class TestServer : NetworkManager
{
    private int WorldClientId;
    private bool WorldClientIdSet = false;
    private Dictionary<int, RowerData> RowerData = new Dictionary<int, RowerData>();

    // Use this for initialization
    void Start()
    {
        StartServer();
        NetworkServer.RegisterHandler((short)CustomMsgType.StartRowing, OnStartRowing);
        NetworkServer.RegisterHandler((short)CustomMsgType.StopRowing, OnStopRowing);
        NetworkServer.RegisterHandler((short)CustomMsgType.RegisterView, (netMsg) =>
        {
            WorldClientId = netMsg.conn.connectionId;
            WorldClientIdSet = true;
        });
    }

    private void OnStopRowing(NetworkMessage netMsg)
    {
        var data = RowerData[netMsg.conn.connectionId];
        data.Stop = DateTime.Now;

        var updateMsg = new ViewUpdateRowerMessage { Id = netMsg.conn.connectionId, Duration = (float)(data.Stop - data.Start).TotalMilliseconds };

        if (WorldClientIdSet)
        {
            NetworkServer.SendToClient(WorldClientId, (short)CustomMsgType.UpdateFromFinishedRow, updateMsg);
        }
    }

    private void OnStartRowing(NetworkMessage netMsg)
    {
        RowerData[netMsg.conn.connectionId] = new RowerData { Start = DateTime.Now };
    }
}
