using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RandomHeadSelect : MonoBehaviour {

	public static int currentHead = 0;

	public void Start() {
		SelectHat(currentHead);
		currentHead++; 
	}

	public void SelectHat(int i)
	{
		i = Mathf.Clamp(i, 0, GetHatCount() - 1);
		for (int j = 0; j < GetHatCount(); j++) {
			transform.GetChild(j).gameObject.SetActive(j == i);
		}
	}

	public int GetHatCount()
	{
		return transform.childCount;
	}



}
