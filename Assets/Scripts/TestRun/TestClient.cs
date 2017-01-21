using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class TestClient : NetworkManager
{
    public RowingView View;
    private float CountdownMove;
    private float CountdownWait;
    private float MoveDuration;
    private float WaitDuration;

    bool IsMoving = false;
    // Use this for initialization
    void Start()
    {
        client = StartClient();
    }

    // Update is called once per frame
    void Update()
    {
        if (client == null || !client.isConnected)
        {
            return;
        }
        if (!IsMoving)
        {
            if (CountdownWait <= 0)
            {
                client.Send ((short)CustomMsgType.StartRowing, new IntegerMessage (0));
                View.StartRowing();
				MoveDuration = 2; // Random.Range(0.5f, 4);
                View.SetAnimationDuration(MoveDuration);
                CountdownMove = MoveDuration;
                IsMoving = true;
            }
            else
            {
                CountdownWait -= Time.deltaTime;
            }
        }
        else
        {
            CountdownMove -= Time.deltaTime;
            if (CountdownMove <= 0)
            {
                WaitDuration = Random.Range(1, 3);
				CountdownWait = 6; // WaitDuration;
                client.Send((short)CustomMsgType.StopRowing, new IntegerMessage(0));
                View.StopRowing();
                IsMoving = false;
            }
        }
    }
}
