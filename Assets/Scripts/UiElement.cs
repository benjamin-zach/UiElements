using UnityEngine;
using UnityEngine.EventSystems;

namespace UiElements
{
	public abstract class UiElement : MonoBehaviour
	{
		protected abstract bool CheckReferences();
		protected abstract bool UpdateReferences();
		protected abstract void AwakeInternal();

		public void Awake()
		{
			if (!CheckReferences() && !UpdateReferences())
			{
				// TODO: logging
				return;
			}

			AwakeInternal();
		}
	}

	public abstract class InteractableUiElement : UiElement, IDragHandler, IBeginDragHandler, IEndDragHandler
	{
		public abstract void OnBeginDrag(PointerEventData eventData);
		public abstract void OnDrag(PointerEventData eventData);
		public abstract void OnEndDrag(PointerEventData eventData);

		protected abstract void OnClick();
	}
}