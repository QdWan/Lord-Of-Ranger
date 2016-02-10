using System.Drawing;

namespace LordOfRanger.Setting {
	internal class Mouse : DataAb {
		
		internal LordOfRanger.Mouse.Set[] sendList = new LordOfRanger.Mouse.Set[0];

		internal override byte[] Push {
			get;
			set;
		}

		internal override int Id {
			get;
			set;
		}

		internal override InstanceType Type {
			get {
				return InstanceType.MOUSE;
			}
		}

		internal override int Priority {
			get;
			set;
		}

		internal override Bitmap SkillIcon {
			get;
			set;
		}

		internal override Bitmap DisableSkillIcon {
			get;
			set;
		}

		internal override bool Enable {
			get;
			set;
		}

	}
}