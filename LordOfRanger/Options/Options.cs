namespace LordOfRanger.Options {
	public class Options {
		public enum StartupState {
			NORMAL,
			MINIMIZED
		}
		public enum IconDisplayPosition {
			TOP_LEFT,
			TOP_RIGHT,
			BOTTOM_LEFT,
			BOTTOM_RIGHT
		}

		public static Options options;

		/* general */
		public int startupState = (int)StartupState.NORMAL;

		/* job */
		public int commandInterval = 50;
		public int upDownInterval = 15;
		public int commandUpDownInterval = 30;
		public int timerInterval = 30;

		/* KeyBoard */
		public bool keyboardCancelBarrage = false;
		public bool keyboardCancelCommand = false;
		public bool keyboardCancelToggle = false;
		public bool keyboardCancelMouse = false;

		/*Mouse*/
		public int mouseReClick = 0;

		/* icon */
		public bool iconViewFlag = true;
		public int oneRowIcons = 6; //the number of one row of icons.
		public int iconDisplayPosition = (int)IconDisplayPosition.BOTTOM_RIGHT;

		/* active window */
		public bool activeWindowMonitoring = true;
		public int activeWindowMonitoringinterval = 500;
		public string processName = "ARAD";

		/* Hot Key */
		public byte hotKeyLorSwitching = 0x00;

		/*restore*/
		public string currentSettingName = "";
	}
}
