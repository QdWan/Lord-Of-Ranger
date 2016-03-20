namespace LordOfRanger.Behavior.Action {
	internal class Mouse :Act {

		internal LordOfRanger.Mouse.MouseData mouseData;
		internal Mouse() {
			this.mouseData = new LordOfRanger.Mouse.MouseData();
			this.type = InstanceType.MOUSE;
		}
	}
}