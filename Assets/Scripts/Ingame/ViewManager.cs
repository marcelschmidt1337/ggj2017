using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour {

	public Boat BoatA;
	public Boat BoatB;
	public GameObject PlayerPrefab;

	void UpdateView () {
		var gameState = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<GameState>();
		//gameState.OnGameStateChanged
	}
}
