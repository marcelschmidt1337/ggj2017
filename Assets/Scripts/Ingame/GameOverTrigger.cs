using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
	public TestView GameView;
	private bool triggered = false;

	private void OnTriggerExit (Collider other)
	{
		Debug.LogError (other.name);
		Debug.LogError (other.tag);

		if (!triggered)
		{
			triggered = true;

			if (other.tag.Equals ("BoatA"))
			{
				GameView.SendGameOver (PlayerConstants.GROUP_A);
			}
			else if (other.tag.Equals ("BoatB"))
			{
				GameView.SendGameOver (PlayerConstants.GROUP_B);
			}
		}

	}
}
