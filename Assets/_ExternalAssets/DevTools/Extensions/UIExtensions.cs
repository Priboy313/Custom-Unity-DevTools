using UnityEngine;

namespace DevTools.Extensions
{

	public static class UIExtensions
	{
		/// <summary>
		/// Safely sets the visibility, interactability, and raycast blocking of a CanvasGroup.
		/// </summary>
		public static void SetVisibility(this CanvasGroup canvasGroup, bool isVisible)
		{
			if (canvasGroup == null) return;

			canvasGroup.alpha = isVisible ? 1f : 0f;
			canvasGroup.interactable = isVisible;
			canvasGroup.blocksRaycasts = isVisible;
		}
	}
}