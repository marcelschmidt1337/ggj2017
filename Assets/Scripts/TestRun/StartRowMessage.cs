﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SingleRowProperties : MessageBase
{
    int Id;
    float Duration;

    public override void Serialize(NetworkWriter writer)
    {
        writer.Write(Id);
        writer.Write(Duration);
    }

    public override void Deserialize(NetworkReader reader)
    {
        Id = reader.ReadInt32();
        Duration = reader.ReadSingle();
    }
}
