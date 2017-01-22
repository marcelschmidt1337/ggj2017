using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
	public TestClient Client;
	public Transform HatCollection;

	private int hatCount;
	private int current = 0;

	void Start ()
	{
		hatCount = HatCollection.childCount;
	}

	public void NextHat ()
	{
		HatCollection.GetChild (current).gameObject.SetActive (false);
		current = (current + 1) % hatCount;
		HatCollection.GetChild (current).gameObject.SetActive (true);
	}

	public void PreviousHat ()
	{
		HatCollection.GetChild (current).gameObject.SetActive (false);
		current = (current - 1) % hatCount;
		if (current == -1)
		{
			current = hatCount - 1;
		}
		HatCollection.GetChild (current).gameObject.SetActive (true);
	}

	public void Confirm ()
	{
		Client.ChangeHat (current);
	}
}
