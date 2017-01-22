using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatSelector : MonoBehaviour {

	public int GetHatCount () {
		return transform.childCount;
	}

	public void SelectRandomHat () {
		SelectHat( Random.Range( 0, GetHatCount() ) );
	}

	public void SelectHat (int i) {
		i = Mathf.Clamp( i, 0, GetHatCount()-1 );
		for (int j = 0; j < GetHatCount(); j++) {
			transform.GetChild( j ).gameObject.SetActive( j == i );
		}
	}
}
