using UnityEngine;

namespace DevTools.Extensions
{

	public static class MathExtensions
	{
		/// <summary>
		/// Remaps a float value from one range to another.
		/// <br>Example: <code>float alpha = currentHealth.Remap(0, 100, 0, 1);</code></br>
		/// </summary>
		public static float Remap(this float value, float fromSource, float toSource, float fromTarget, float toTarget)
		{
			// Предотвращаем деление на ноль, если диапазон пустой
			if (Mathf.Approximately(fromSource, toSource)) return fromTarget;

			return fromTarget + (value - fromSource) * (toTarget - fromTarget) / (toSource - fromSource);
		}
	}
}