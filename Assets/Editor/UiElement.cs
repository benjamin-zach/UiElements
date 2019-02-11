using UnityEngine;

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
}