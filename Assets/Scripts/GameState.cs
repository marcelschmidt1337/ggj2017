using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
  List<Player> connectedPlayers = new List<Player> ();

  public Player FindPlayer (int id)
  {
    return connectedPlayers.Find (p => p.Id == id);
  }

  public void AddPlayer (int id, int groupId = -1, int sideId = -1)
  {
    Player newPlayer = new Player ();
    newPlayer.Id = id;

    connectedPlayers.Add (newPlayer);
  }

  public void RemovePlayer (int id)
  {
    var player = FindPlayer (id);
    if (player != null)
    {
      connectedPlayers.Remove (player);
    }
  }
}
