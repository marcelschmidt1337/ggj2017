using UnityEngine;

[CreateAssetMenu(fileName = "Balancing")]
public class Balancing : ScriptableObject {
	public static Balancing Instance
	{
		get
		{
			return Resources.Load<Balancing>( "Balancing" );
		}
	}
	
	[Tooltip("The amount of time in seconds, after which a row gets force-executed."), Range(0.0f, 5.0f)]
	public float RowTimeOut;
	public float MaxRowPowerBaseValue = 6.0f;
	public float MaxRowPowerPerPlayerValue = 1.0f;
	public AnimationCurve PowerFactorCurve;
	public AnimationCurve TimeFactorCurve;
}
