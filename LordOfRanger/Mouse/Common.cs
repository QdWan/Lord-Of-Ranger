using System.Collections.Generic;

namespace LordOfRanger.Mouse {
	internal enum Operation {

		LEFT,
		RIGHT,
		MOVE

	}

	internal class MouseData {

		internal MouseData() {
			SwitchState = SwitchingStyle.BOTH;
			Value = new List<Set>();
		}

		internal SwitchingStyle SwitchState {
			get;
			set;
		}
		internal List<Set> Value {
			get;
			set;
		}

	}
}
