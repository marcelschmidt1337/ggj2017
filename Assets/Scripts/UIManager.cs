using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[Header ("UI Panels")]
	public GameObject Connect;
	public GameObject GroupSelect;
	public GameObject SideSelect;
	public GameObject Waiting;
	public GameObject ClientUi;

	[Header ("Connect")]
	public InputField IpInput;

	[Header ("Waiting For Start")]
	public Button BackButton;
	public GameObject PlayerStatus;
	public Text GroupAText;
	public Text GroupALeft;
	public Text GroupARight;
	public Text GroupBText;
	public Text GroupBLeft;
	public Text GroupBRight;

	public TestView WorldView;
	public TestClient ClientView;

	public bool IsPresenter;
	public bool IsClient;


	private GameState gameState;

	private void OnEnable ()
	{
		if (IsPresenter)
		{
			WorldView.StartWorldView ();
			ShowWaiting (true);
		}
		else
		{
			ShowConnect ();
		}

		if (IsPresenter)
		{
			gameState = WorldView.GameState;
			gameState.OnGameStateChanged -= UpdateText;
			gameState.OnGameStateChanged += UpdateText;
		}


	}

	public void ShowConnect ()
	{
		Connect.gameObject.SetActive (true);
		GroupSelect.gameObject.SetActive (false);
		SideSelect.gameObject.SetActive (false);
		Waiting.gameObject.SetActive (false);
		ClientUi.gameObject.SetActive(false);
		SetClientUiActive(false);
	}

	public void ShowGroupSelection ()
	{
		Connect.gameObject.SetActive (false);
		GroupSelect.gameObject.SetActive (true);
		SideSelect.gameObject.SetActive (false);
		Waiting.gameObject.SetActive (false);
		SetClientUiActive(false);
	}

	public void ShowSideSelection ()
	{
		Connect.gameObject.SetActive (false);
		GroupSelect.gameObject.SetActive (false);
		SideSelect.gameObject.SetActive (true);
		Waiting.gameObject.SetActive (false);
		SetClientUiActive(false);
	}

	public void ShowWaiting (bool showPlayerStatus)
	{
		PlayerStatus.gameObject.SetActive (showPlayerStatus);

		Connect.gameObject.SetActive (false);
		GroupSelect.gameObject.SetActive (false);
		SideSelect.gameObject.SetActive (false);
		Waiting.gameObject.SetActive (true);
		SetClientUiActive(false);
	}

	public void ShowClientUi()
	{
		Connect.gameObject.SetActive (false);
		GroupSelect.gameObject.SetActive (false);
		SideSelect.gameObject.SetActive (false);
		Waiting.gameObject.SetActive (false);
		SetClientUiActive(true);
	}

	private void SetClientUiActive(bool active)
	{
		if (IsClient)
		{
			ClientUi.gameObject.SetActive(active);
		}
	}

	public void OnConnectButton ()
	{
		string ip = string.IsNullOrEmpty (IpInput.text) ? (IpInput.placeholder as Text).text : IpInput.text;
		ClientView.StartClient (ip);
		ShowGroupSelection ();
	}


	public void OnBackButton ()
	{
		ClientView.LeaveSide ();
		ClientView.LeaveGroup ();
		ShowGroupSelection ();
	}

	public void OnStartButton ()
	{
		WorldView.SendStartGame ();
	}

	public void OnGroupSelect (int team)
	{
		ShowSideSelection ();
		ClientView.JoinGroup (team);
	}

	public void OnSideSelect (int side)
	{
		ShowWaiting (false);
		ClientView.JoinSide (side);
	}

	private void UpdateText ()
	{
		int a = gameState.GetPlayerInGroup (PlayerConstants.GROUP_A);
		int b = gameState.GetPlayerInGroup (PlayerConstants.GROUP_B);

		int aLeft = gameState.GetPlayerOnSide (PlayerConstants.GROUP_A, PlayerConstants.SIDE_LEFT);
		int aRight = gameState.GetPlayerOnSide (PlayerConstants.GROUP_A, PlayerConstants.SIDE_RIGHT);

		int bLeft = gameState.GetPlayerOnSide (PlayerConstants.GROUP_B, PlayerConstants.SIDE_LEFT);
		int bRight = gameState.GetPlayerOnSide (PlayerConstants.GROUP_B, PlayerConstants.SIDE_RIGHT);

		GroupAText.text = string.Format ("Group A: {0}", a);
		GroupALeft.text = string.Format ("Left: {0}", aLeft);
		GroupARight.text = string.Format ("Right: {0}", aRight);

		GroupBText.text = string.Format ("Group B: {0}", b);
		GroupBLeft.text = string.Format ("Left: {0}", bLeft);
		GroupBRight.text = string.Format ("Right: {0}", bRight);
	}
}
