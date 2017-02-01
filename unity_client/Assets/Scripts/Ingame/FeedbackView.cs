using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackView : MonoBehaviour
{

	public Image Background;
	public Text Text;

	[System.Serializable]
	public class FeedbackInfo
	{
		public Color backgroundColor;
		public Sprite backgroundSprite;
		public string text;
		public Color textColor;
	}

	public enum FeedbackType
	{
		Bad,
		Good,
		Great
	}

	public FeedbackInfo BadFeedback;
	public FeedbackInfo GoodFeedback;
	public FeedbackInfo GreatFeedback;

	public float MoveUpSpeed = 0.3f;
	public float ShowTime = 0.8f;
	public AnimationCurve alphaFade;

	public void Init (FeedbackType feedbackType) {
		switch (feedbackType) {
			case FeedbackType.Bad:
				break;
		}
	}

	void ApplyFeedbackInfo (FeedbackInfo feedbackInfo) {
		this.Background.sprite = feedbackInfo.backgroundSprite;
		this.Background.color = feedbackInfo.backgroundColor;
		this.Text.text = feedbackInfo.text;
		this.Text.color = feedbackInfo.textColor;
	}

	void Update () {

	}
}
