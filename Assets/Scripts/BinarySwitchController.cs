using UnityEngine;
using UnityEngine.UI;

namespace UiElements
{
	public class BinarySwitchController : InteractableUiElement
	{
		#region Settable Parameters
		public bool InitialOn = false;
		public Color BackgroundColorOff = new Color(236, 236, 236);
		public Color BackgroundColorOn = new Color(96, 212, 91);
		public Color BackgroundFrameColor = new Color(188, 188, 188);
		public Color PinColor = new Color(255, 255, 255);
		public Color PinFameColor = new Color(188, 188, 188);
		#endregion

		#region Scene References
		private Image BackgroundImage;
		private Image BackgroundFrameImage;
		private Image PinImage;
		private Image PinFrameImage;
		private RectTransform BackgroundImageTransform;
		private RectTransform PinImageTransform;
		#endregion

		private bool On = false;
		private Vector3 LeftPinPosition, RightPinPosition;

		protected override bool CheckReferences()
		{
			return BackgroundImage != null &&
				BackgroundFrameImage != null &&
				PinImage != null &&
				PinFrameImage != null &&
				BackgroundImageTransform != null &&
				PinImageTransform != null;
		}

		protected override bool UpdateReferences()
		{
			
			var _BackgroundImage = transform.Find("BackgroundImage");
			BackgroundImage = _BackgroundImage.GetComponent<Image>();
			BackgroundImageTransform = _BackgroundImage.GetComponent<RectTransform>();
			BackgroundFrameImage = transform.Find("BackgroundFrameImage").GetComponent<Image>();
			var _PinImage = transform.Find("PinImage");
			PinImage = _PinImage.GetComponent<Image>();
			PinImageTransform = _PinImage.GetComponent<RectTransform>();
			PinFrameImage = transform.Find("PinImage/PinFrameImage").GetComponent<Image>();
			return CheckReferences();
		}

		protected override void AwakeInternal()
		{
			On = InitialOn;
			const int Offset = 5;
			LeftPinPosition = BackgroundImageTransform.position + new Vector3(Offset, 0);			
			RightPinPosition = BackgroundImageTransform.position + new Vector3(BackgroundImageTransform.rect.width - PinImageTransform.rect.width - Offset, 0);
			var _Button = PinImage.gameObject.AddComponent<Button>();
			_Button.onClick.AddListener(OnClick);
			ExplicitUpdate();
		}

		protected override void OnClick()
		{
			Toggle();
		}

		private void Toggle()
		{
			if (On)
				SetOff();
			else
				SetOn();
		}

		private void ExplicitUpdate()
		{
			if (On)
				SetOn();
			else
				SetOff();
		}

		private void SetOn()
		{
			PinImage.transform.position= RightPinPosition;
			BackgroundImage.color = BackgroundColorOn;
			On = true;
		}

		private void SetOff()
		{
			PinImage.transform.position = LeftPinPosition;
			BackgroundImage.color = BackgroundColorOff;
			On = false;
		}
	}
}