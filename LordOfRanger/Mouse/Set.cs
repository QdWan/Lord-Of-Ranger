namespace LordOfRanger.Mouse {
	class Set {
		internal readonly Operation op;
		internal readonly int x;
		internal readonly int y;
		internal readonly int sleepBetween;
		internal readonly int sleepAfter;

		internal Set(Operation op,int x,int y, int sleepBetween, int sleepAfter ) {
			this.op = op;
			this.x = x;
			this.y = y;
			this.sleepBetween = sleepBetween;
			this.sleepAfter = sleepAfter;
		}

	}
}
