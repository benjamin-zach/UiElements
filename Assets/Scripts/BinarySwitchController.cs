using UnityEngine;
using UnityEngine.EventSystems;
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
		private GameObject Pin;
		//private Image PinFrameImage;
		private RectTransform BackgroundImageTransform;
		private RectTransform PinImageTransform;
		private Animator Animator;
		#endregion

		private bool On = false;
		private Vector3 LeftPinPosition, RightPinPosition;

		protected override bool CheckReferences()
		{
			return BackgroundImage != null &&
				BackgroundFrameImage != null &&
				Pin != null &&
				//PinFrameImage != null &&
				BackgroundImageTransform != null &&
				PinImageTransform != null &&
				Animator != null;
		}

		protected override bool UpdateReferences()
		{
			
			var _BackgroundImage = transform.Find("BackgroundImage");
			BackgroundImage = _BackgroundImage.GetComponent<Image>();
			BackgroundImageTransform = _BackgroundImage.GetComponent<RectTransform>();
			BackgroundFrameImage = transform.Find("BackgroundFrameImage").GetComponent<Image>();
			Pin = transform.Find("Pin").gameObject;
			PinImageTransform = Pin.GetComponent<RectTransform>();
			//PinFrameImage = transform.Find("PinImage/PinFrameImage").GetComponent<Image>();
			Animator = gameObject.GetComponent<Animator>();
			return CheckReferences();
		}

		protected override void AwakeInternal()
		{
			On = InitialOn;
			const int Offset = 5;
			LeftPinPosition = new Vector3(Offset, 0);			
			RightPinPosition = new Vector3(BackgroundImageTransform.rect.width - PinImageTransform.rect.width - Offset, 0);
			var _Button = Pin.gameObject.AddComponent<Button>();
			_Button.onClick.AddListener(OnClick);
			ExplicitUpdate();
		}

		protected override void OnClick()
		{
			Toggle();
		}

		public void Toggle()
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
			Pin.transform.localPosition = RightPinPosition;
			BackgroundImage.color = BackgroundColorOn;
			On = true;
		}

		private void SetOff()
		{
			Pin.transform.localPosition = LeftPinPosition;
			BackgroundImage.color = BackgroundColorOff;
			On = false;
		}

		public override void OnBeginDrag(PointerEventData eventData)
		{
			Animator.SetBool("Expanded", !Animator.GetBool("Expanded"));
		}

		public override void OnDrag(PointerEventData eventData)
		{

		}

		public override void OnEndDrag(PointerEventData eventData)
		{
			Animator.SetBool("Expanded", !Animator.GetBool("Expanded"));
		}
	}
}