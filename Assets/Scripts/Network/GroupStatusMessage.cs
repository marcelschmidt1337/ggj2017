using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GroupStatusMessage : MessageBase
{
  public int GroupAPlayer;
  public int GroupBPlayer;

  public override void Serialize (NetworkWriter writer)
  {
    writer.Write (GroupAPlayer);
    writer.Write (GroupBPlayer);
  }

  public override void Deserialize (NetworkReader reader)
  {
    GroupAPlayer = reader.ReadInt32 ();
    GroupBPlayer = reader.ReadInt32 ();
  }
}
