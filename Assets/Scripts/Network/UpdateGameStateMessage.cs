using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class UpdateGameStateMessage : MessageBase
{
	public List<Player> ConnectedPlayers;

	public override void Serialize (NetworkWriter writer)
	{
		string json = JsonUtility.ToJson (ConnectedPlayers);
		Debug.LogError (json);
		writer.Write (json);
	}

	public override void Deserialize (NetworkReader reader)
	{
		string json = reader.ReadString ();
		ConnectedPlayers = JsonUtility.FromJson<List<Player>> (json);
	}
}
