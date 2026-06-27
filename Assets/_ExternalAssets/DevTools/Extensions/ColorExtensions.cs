using UnityEngine;

namespace DevTools.Extensions
{
	public static class ColorExtensions
	{
		/// <summary>
		/// Returns a new Color with the Alpha component modified.
		/// <br>Example: <code>myImage.color = myImage.color.WithAlpha(0.5f);</code></br>
		/// </summary>
		public static Color WithAlpha(this Color color, float alpha)
		{
			return new Color(color.r, color.g, color.b, alpha);
		}
	}

}