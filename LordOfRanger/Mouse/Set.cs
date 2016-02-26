namespace LordOfRanger.Mouse {
	class Set {
		internal enum Operation{
			LEFT,
			RIGHT,
			MOVE
		}
		internal Operation op;
		internal int x;
		internal int y;
		internal int sleepBetween;
		internal int sleepAfter;

		internal Set(Operation op,int x,int y, int sleepBetween, int sleepAfter ) {
			this.op = op;
			this.x = x;
			this.y = y;
			this.sleepBetween = sleepBetween;
			this.sleepAfter = sleepAfter;
		}

	}
}
