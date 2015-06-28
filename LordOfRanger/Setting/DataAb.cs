using System.Drawing;

namespace LordOfRanger.Setting {
	internal abstract class DataAb {

		internal enum Type {
			COMMAND,
			BARRAGE,
			TOGGLE,
		}

		internal abstract int id {
			get;
			set;
		}

		internal abstract Type type {
			get;
		}

		internal abstract int priority {
			get;
			set;
		}

		internal abstract Bitmap skillIcon {
			get;
			set;
		}

		internal abstract Bitmap disableSkillIcon {
			get;
			set;
		}

		internal abstract bool enable {
			get;
			set;
		}
	}
}
