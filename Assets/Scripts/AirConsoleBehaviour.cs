using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class AirConsoleBehaviour : MonoBehaviour
{
  private void Start ()
  {
    AirConsole.instance.onMessage += OnMessage;
  }

  private void OnMessage (int from, JToken data)
  {
    AirConsole.instance.Message (from, "Test");
  }
}
