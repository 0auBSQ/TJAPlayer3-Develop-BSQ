﻿namespace OpenTaiko.Animations {
	/// <summary>
	/// A class that performs ease-in-out animation.
	/// </summary>
	class EaseInOut : Animator {
		/// <summary>
		/// Initialize Ease-in-out.
		/// </summary>
		/// <param name="startPoint">Starting point.</param>
		/// <param name="endPoint">End point.</param>
		/// <param name="timeMs">Time taken for easing, in milliseconds.</param>
		public EaseInOut(int startPoint, int endPoint, int timeMs) : base(0, timeMs, 1, false) {
			StartPoint = startPoint;
			EndPoint = endPoint;
			Sa = EndPoint - StartPoint;
			TimeMs = timeMs;
		}

		public override object GetAnimation() {
			var percent = Counter.CurrentValue / (double)TimeMs * 2.0;
			if (percent < 1) {
				return ((double)Sa / 2.0 * percent * percent * percent) + StartPoint;
			} else {
				percent -= 2;
				return ((double)Sa / 2.0 * ((percent * percent * percent) + 2)) + StartPoint;
			}
		}

		private readonly int StartPoint;
		private readonly int EndPoint;
		private readonly int Sa;
		private readonly int TimeMs;
	}
}
