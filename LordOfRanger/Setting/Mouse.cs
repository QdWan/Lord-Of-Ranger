using System.Drawing;

namespace LordOfRanger.Setting {
	internal class Mouse : Act {
		internal LordOfRanger.Mouse.Set[] sendList = new LordOfRanger.Mouse.Set[0];
		internal Mouse() {
			this.type = InstanceType.MOUSE;
		}
	}
}