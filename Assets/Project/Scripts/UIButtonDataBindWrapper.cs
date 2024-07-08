using AxGrid;
using AxGrid.Tools.Binders;
using UnityEngine.EventSystems;

namespace LootBox
{
    class UIButtonDataBindWrapper : UIButtonDataBind
    {
		public override void OnClick(BaseEventData bd)
		{
			if (!Cancel)
				OnClick();
			Cancel = false;
		}

		public override void OnClick()
		{
			if (!Button.interactable || !isActiveAndEnabled)
				return;

			if (!Cancel)
			{
				//Settings. Added
				Settings.Model?.EventManager.Invoke("SoundPlay", "Click");

				Settings.Fsm?.Invoke("OnBtn", buttonName);

				Settings.Model?.EventManager.Invoke($"On{buttonName}Click");
			}
			Cancel = false;
		}
	}
}
