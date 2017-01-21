using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  [Header ("UI Panels")]
  public GameObject GroupSelect;
  public GameObject SideSelect;
  public GameObject Waiting;

  [Header ("Group Selection")]
  public Text TeamAText;
  public Button TeamAButton;
  public Text TeamBText;
  public Button TeamBButton;

  [Header ("Side Selection")]
  public Text LeftText;
  public Button LeftButton;
  public Text RightText;
  public Button RightButton;

  private int selectedGroup = -1;
  private int selectedSide = -1;

  private void OnEnable ()
  {
    ShowGroupSelection ();
  }

  public void ShowGroupSelection ()
  {
    GroupSelect.gameObject.SetActive (true);
    SideSelect.gameObject.SetActive (false);
    Waiting.gameObject.SetActive (false);
  }

  public void ShowSideSelection ()
  {
    GroupSelect.gameObject.SetActive (false);
    SideSelect.gameObject.SetActive (true);
    Waiting.gameObject.SetActive (false);
  }

  public void ShowWaiting ()
  {
    GroupSelect.gameObject.SetActive (false);
    SideSelect.gameObject.SetActive (false);
    Waiting.gameObject.SetActive (true);
  }

  public void OnBackButton ()
  {
    selectedGroup = -1;
    selectedSide = -1;
    ShowGroupSelection ();
  }

  public void OnGroupSelect (int team)
  {
    selectedGroup = team;
    ShowSideSelection ();
  }

  public void OnSideSelect (int side)
  {
    selectedSide = side;
    ShowWaiting ();
  }
}
