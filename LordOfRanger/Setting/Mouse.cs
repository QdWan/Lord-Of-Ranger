namespace LordOfRanger.Setting {
	internal class Mouse : Act {

		internal LordOfRanger.Mouse.MouseSetForm.MouseData mouseData;
		internal Mouse() {
			this.mouseData = new LordOfRanger.Mouse.MouseSetForm.MouseData();
			this.type = InstanceType.MOUSE;
		}
	}
}