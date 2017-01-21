using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[Header ("UI Panels")]
	public GameObject Connect;
	public GameObject GroupSelect;
	public GameObject SideSelect;
	public GameObject Waiting;

	[Header ("Connect")]
	public InputField IpInput;

	[Header ("Waiting For Start")]
	public Button BackButton;

	private Manager netManager;

	private void OnEnable ()
	{
		ShowConnect ();
		var gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		netManager = gameManager.GetComponent<Manager> ();
	}

	public void ShowConnect ()
	{
		Connect.gameObject.SetActive (true);
		GroupSelect.gameObject.SetActive (false);
		SideSelect.gameObject.SetActive (false);
		Waiting.gameObject.SetActive (false);
	}

	public void ShowGroupSelection ()
	{
		Connect.gameObject.SetActive (false);
		GroupSelect.gameObject.SetActive (true);
		SideSelect.gameObject.SetActive (false);
		Waiting.gameObject.SetActive (false);
	}

	public void ShowSideSelection ()
	{
		Connect.gameObject.SetActive (false);
		GroupSelect.gameObject.SetActive (false);
		SideSelect.gameObject.SetActive (true);
		Waiting.gameObject.SetActive (false);
	}

	public void ShowWaiting (bool showBackButton)
	{
		BackButton.gameObject.SetActive (showBackButton);

		Connect.gameObject.SetActive (false);
		GroupSelect.gameObject.SetActive (false);
		SideSelect.gameObject.SetActive (false);
		Waiting.gameObject.SetActive (true);
	}

	public void OnHostButton ()
	{
		netManager.StartGameServer ();
		ShowWaiting (false);
	}

	public void OnConnectButton ()
	{
		string ip = string.IsNullOrEmpty (IpInput.text) ? (IpInput.placeholder as Text).text : IpInput.text;
		netManager.StartClient (ip);
		ShowGroupSelection ();
	}

	public void OnConnectAsGameViewClient ()
	{
		string ip = string.IsNullOrEmpty (IpInput.text) ? (IpInput.placeholder as Text).text : IpInput.text;
		netManager.StartGameViewClient (ip);
		ShowGroupSelection ();
	}

	public void OnBackButton ()
	{
		netManager.LeaveSide ();
		netManager.LeaveGroup ();
		ShowGroupSelection ();
	}

	public void OnGroupSelect (int team)
	{
		ShowSideSelection ();
		netManager.JoinGroup (team);
	}

	public void OnSideSelect (int side)
	{
		ShowWaiting (true);
		netManager.JoinSide (side);
	}
}
