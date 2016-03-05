using System.Diagnostics.CodeAnalysis;

namespace LordOfRanger.Options {
	[SuppressMessage( "ReSharper", "UnusedMember.Global" )]
	internal enum StartupState {
		NORMAL,
		MINIMIZED
	}

	internal enum IconDisplayPosition {
		TOP_LEFT,
		TOP_RIGHT,
		BOTTOM_LEFT,
		BOTTOM_RIGHT
	}
}
