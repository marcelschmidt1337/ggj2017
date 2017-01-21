using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NewMonoBehaviour : MessageBase
{
  public int Left;
  public int Right;

  public override void Serialize (NetworkWriter writer)
  {
    writer.Write (Left);
    writer.Write (Right);
  }

  public override void Deserialize (NetworkReader reader)
  {
    Left = reader.ReadInt32 ();
    Right = reader.ReadInt32 ();
  }
}
