using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Boat))]
public class BoatMovement : MonoBehaviour
{
	public float boatForce;
	public List<float> boatDirection;

	
}
