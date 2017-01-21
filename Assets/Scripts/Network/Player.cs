public class Player
{
  public int Id;
  public int Group = -1;
  public int Side = -1;

  public void ResetSide ()
  {
    Side = -1;
  }

  public void ResetGroup ()
  {
    Group = -1;
  }
}
