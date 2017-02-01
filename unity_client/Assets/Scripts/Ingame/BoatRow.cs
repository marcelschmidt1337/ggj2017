using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatRow : MonoBehaviour
{

	public BoatSeat LeftSeat;
	public BoatSeat RightSeat;

	public void UpdateView (ViewInfo.Boat.Row row)
	{
		if (row.LeftSeat.Player != null)
		{
			LeftSeat.gameObject.SetActive (true);
			LeftSeat.Player = row.LeftSeat.Player;
			LeftSeat.Hat.SelectHat (row.LeftSeat.Player.HatIndex);
		}
		else
		{
			LeftSeat.gameObject.SetActive (false);
		}
		if (row.RightSeat.Player != null)
		{
			RightSeat.gameObject.SetActive (true);
			RightSeat.Player = row.RightSeat.Player;
			RightSeat.Hat.SelectHat (row.RightSeat.Player.HatIndex);
		}
		else
		{
			RightSeat.gameObject.SetActive (false);
		}
	}
}
