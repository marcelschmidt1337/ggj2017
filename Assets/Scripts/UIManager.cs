using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[Header ("Group Color")]
	public Color DefaultColor;
	public Color GroupAColor;
	public Color GroupBColor;
	public Image[] Backgrounds;
	public Camera Camera;

	[Header ("UI Panels")]
	public GameObject Connect;
	public GameObject GroupSelect;
	public GameObject SideSelect;
	public GameObject HatSelect;
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
		Connect.SetActive (true);
		GroupSelect.SetActive (false);
		SideSelect.SetActive (false);
		Waiting.SetActive (false);
		ClientUi.SetActive (false);
		SetClientUiActive (false);
		SetHatSelectionActive (false);
	}

	public void ShowGroupSelection ()
	{
		Connect.SetActive (false);
		GroupSelect.SetActive (true);
		SideSelect.SetActive (false);
		Waiting.SetActive (false);
		SetClientUiActive (false);
		SetHatSelectionActive (false);
	}

	public void ShowSideSelection ()
	{
		Connect.SetActive (false);
		GroupSelect.SetActive (false);
		SideSelect.SetActive (true);
		Waiting.SetActive (false);
		SetClientUiActive (false);
		SetHatSelectionActive (false);
	}

	public void ShowWaiting (bool showPlayerStatus)
	{
		PlayerStatus.SetActive (showPlayerStatus);

		Connect.SetActive (false);
		GroupSelect.SetActive (false);
		SideSelect.SetActive (false);
		Waiting.SetActive (true);
		SetClientUiActive (false);
		SetHatSelectionActive (false);
	}

	public void ShowClientUi ()
	{
		Connect.SetActive (false);
		GroupSelect.SetActive (false);
		SideSelect.SetActive (false);
		Waiting.SetActive (false);
		SetClientUiActive (true);
		SetHatSelectionActive (false);
	}

	private void SetClientUiActive (bool active)
	{
		if (IsClient)
		{
			ClientUi.SetActive (active);
		}
	}

	public void ShowHatSelection ()
	{
		Connect.SetActive (false);
		GroupSelect.SetActive (false);
		SideSelect.SetActive (false);
		Waiting.SetActive (false);
		SetClientUiActive (false);
		SetHatSelectionActive (true);
	}

	private void SetHatSelectionActive (bool active)
	{
		if (IsClient)
		{
			HatSelect.SetActive (active);
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
		SetBackgroundColor (DefaultColor);
		ClientView.LeaveSide ();
		ClientView.LeaveGroup ();
		ShowGroupSelection ();
	}

	public void OnStartButton ()
	{
		Waiting.SetActive (false);
		WorldView.SendStartGame ();
	}

	public void OnGroupSelect (int team)
	{
		SetBackgroundColor (team == 0 ? GroupAColor : GroupBColor);
		ShowSideSelection ();
		ClientView.JoinGroup (team);
	}

	public void OnSideSelect (int side)
	{
		ShowHatSelection ();
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

	private void SetBackgroundColor (Color color)
	{
		for (int i = 0; i < Backgrounds.Length; i++)
		{
			Backgrounds[i].color = color;
		}

		Camera.backgroundColor = color;
	}
}
