using System.Drawing;

namespace LordOfRanger.Setting {
	internal class Barrage : DataAb {
		private int _id;
		private int _priority;
		private bool _enable = true;
		private Bitmap _skillIcon;
		private Bitmap _disableSkillIcon;
		internal byte push;
		internal byte send;

		internal override int id {
			get {
				return _id;
			}
			set {
				_id = value;
			}
		}

		internal override Type type {
			get {
				return Type.BARRAGE;
			}
		}

		internal override int priority {
			get {
				return _priority;
			}
			set {
				_priority = value;
			}
		}

		internal override Bitmap skillIcon {
			get {
				return _skillIcon;
			}
			set {
				_skillIcon = value;
			}
		}

		internal override Bitmap disableSkillIcon {
			get {
				return _disableSkillIcon;
			}
			set {
				_disableSkillIcon = value;
			}
		}

		internal override bool enable {
			get {
				return _enable;
			}
			set {
				_enable = value;
			}
		}
	}
}
