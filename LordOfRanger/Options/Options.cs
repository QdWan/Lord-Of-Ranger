namespace LordOfRanger.Options {
	public class Options {
		public enum STARTUP_STATE {
			NORMAL,
			MINIMIZED
		}
		public enum ICON_DISPLAY_POSITION {
			TOP_LEFT,
			TOP_RIGHT,
			BOTTOM_LEFT,
			BOTTOM_RIGHT
		}

		public static Options options;

		/* general */
		public int startupState = (int)STARTUP_STATE.NORMAL;

		/* job */
		public int commandInterval = 50;
		public int upDownInterval = 15;
		public int commandUpDownInterval = 30;
		public int timerInterval = 30;

		/* icon */
		public bool iconViewFlag = true;
		public int oneRowIcons = 6; //the number of one row of icons.
		public int iconDisplayPosition = (int)ICON_DISPLAY_POSITION.BOTTOM_RIGHT;

		/* active window */
		public bool activeWindowMonitoring = true;
		public int activeWindowMonitoringinterval = 500;
		public string ProcessName = "ARAD";

		/* Hot Key */
		public byte hotKeyLORSwitching = 0x00;

		/* Advanced */
		public bool commandUpArrowKeys = true;

		/*restore*/
		public string currentSettingName = "";
	}
}
