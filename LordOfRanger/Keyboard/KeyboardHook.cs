using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LordOfRanger.Keyboard {
	///<summary>キーボードが操作されたときに実行されるメソッドを表すイベントハンドラ。</summary>
	public delegate void KeyboardHookedEventHandler( object sender, KeyboardHookedEventArgs e );
	///<summary>KeyboardHookedイベントのデータを提供する。</summary>
	public class KeyboardHookedEventArgs :CancelEventArgs {
		///<summary>新しいインスタンスを作成する。</summary>
		internal KeyboardHookedEventArgs( KeyboardMessage message, ref KeyboardState state ) {
			this._message = message;
			this._state = state;
		}
		private KeyboardMessage _message;
		private KeyboardState _state;
		///<summary>キーボードが押されたか放されたかを表す値を取得する。</summary>
		public KeyboardUpDown UpDown {
			get {
				return ( this._message == KeyboardMessage.KEY_DOWN || this._message == KeyboardMessage.SYS_KEY_DOWN ) ?
					KeyboardUpDown.DOWN : KeyboardUpDown.UP;
			}
		}
		///<summary>操作されたキーの仮想キーコードを表す値を取得する。</summary>
		public Keys KeyCode {
			get {
				return this._state.keyCode;
			}
		}
		///<summary>操作されたキーのスキャンコードを表す値を取得する。</summary>
		public int ScanCode {
			get {
				return this._state.scanCode;
			}
		}
		///<summary>操作されたキーがテンキーなどの拡張キーかどうかを表す値を取得する。</summary>
		public bool IsExtendedKey {
			get {
				return this._state.flag.IsExtended;
			}
		}
		///<summary>ALTキーが押されているかどうかを表す値を取得する。</summary>
		public bool AltDown {
			get {
				return this._state.flag.AltDown;
			}
		}
		public int ExtraInfo {
			get {
				return (int)this._state.extraInfo;
			}
		}
	}
	///<summary>キーボードが押されているか放されているかを表す。</summary>
	public enum KeyboardUpDown {
		///<summary>キーは押されている。</summary>
		DOWN,
		///<summary>キーは放されている。</summary>
		UP,
	}

	///<summary>メッセージコードを表す。</summary>
	internal enum KeyboardMessage {
		///<summary>キーが押された。</summary>
		KEY_DOWN = 0x100,
		///<summary>キーが放された。</summary>
		KEY_UP = 0x101,
		///<summary>システムキーが押された。</summary>
		SYS_KEY_DOWN = 0x104,
		///<summary>システムキーが放された。</summary>
		SYS_KEY_UP = 0x105,
	}
	///<summary>キーボードの状態を表す。</summary>
	internal struct KeyboardState {
		///<summary>仮想キーコード。</summary>
		public Keys keyCode;
		///<summary>スキャンコード。</summary>
		public int scanCode;
		///<summary>各種特殊フラグ。</summary>
		public KeyboardStateFlag flag;
		///<summary>このメッセージが送られたときの時間。</summary>
		public int time;
		///<summary>メッセージに関連づけられた拡張情報。</summary>
		public IntPtr extraInfo;
	}
	///<summary>キーボードの状態を補足する。</summary>
	internal struct KeyboardStateFlag {
		private int _flag;
		private bool IsFlagging( int value ) {
			return ( this._flag & value ) != 0;
		}
		private void Flag( bool value, int digit ) {
			this._flag = value ? ( this._flag | digit ) : ( this._flag & ~digit );
		}
		///<summary>キーがテンキー上のキーのような拡張キーかどうかを表す。</summary>
		public bool IsExtended {
			get {
				return IsFlagging( 0x01 );
			}
			set {
				Flag( value, 0x01 );
			}
		}
		///<summary>イベントがインジェクトされたかどうかを表す。</summary>
		public bool IsInjected {
			get {
				return IsFlagging( 0x10 );
			}
			set {
				Flag( value, 0x10 );
			}
		}
		///<summary>ALTキーが押されているかどうかを表す。</summary>
		public bool AltDown {
			get {
				return IsFlagging( 0x20 );
			}
			set {
				Flag( value, 0x20 );
			}
		}
		///<summary>キーが放されたどうかを表す。</summary>
		public bool IsUp {
			get {
				return IsFlagging( 0x80 );
			}
			set {
				Flag( value, 0x80 );
			}
		}
	}
	///<summary>キーボードの操作をフックし、任意のメソッドを挿入する。</summary>
	[DefaultEvent( "KeyboardHooked" )]
	public class KeyboardHook :Component {
		[DllImport( "user32.dll", SetLastError = true )]
		private static extern IntPtr SetWindowsHookEx( int hookType, KeyboardHookDelegate hookDelegate, IntPtr hInstance, uint threadId );
		[DllImport( "user32.dll", SetLastError = true )]
		private static extern int CallNextHookEx( IntPtr hook, int code, KeyboardMessage message, ref KeyboardState state );
		[DllImport( "user32.dll", SetLastError = true )]
		private static extern bool UnhookWindowsHookEx( IntPtr hook );

		private delegate int KeyboardHookDelegate( int code, KeyboardMessage message, ref KeyboardState state );
		private const int KEYBOARD_HOOK_TYPE = 13;
		private GCHandle _hookDelegate;
		private IntPtr _hook;
		private static readonly object EVENT_KEYBOARD_HOOKED = new object();
		///<summary>キーボードが操作されたときに発生する。</summary>
		public event KeyboardHookedEventHandler KeyboardHooked {
			add {
				Events.AddHandler( EVENT_KEYBOARD_HOOKED, value );
			}
			remove {
				Events.RemoveHandler( EVENT_KEYBOARD_HOOKED, value );
			}
		}
		///<summary>
		///KeyboardHookedイベントを発生させる。
		///</summary>
		///<param name="e">イベントのデータ。</param>
		protected virtual void OnKeyboardHooked( KeyboardHookedEventArgs e ) {
			KeyboardHookedEventHandler handler = Events[EVENT_KEYBOARD_HOOKED] as KeyboardHookedEventHandler;
			if( handler != null ) {
				handler( this, e );
			}
		}
		///<summary>
		///新しいインスタンスを作成する。
		///</summary>
		public KeyboardHook() {
			KeyboardHookDelegate callback = CallNextHook;
			this._hookDelegate = GCHandle.Alloc( callback );
			IntPtr module = Marshal.GetHINSTANCE( typeof( KeyboardHook ).Assembly.GetModules()[0] );
			this._hook = SetWindowsHookEx( KEYBOARD_HOOK_TYPE, callback, module, 0 );
		}
		///<summary>
		///キーボードが操作されたときに実行するデリゲートを指定してインスタンスを作成する。
		///</summary>
		///<param name="handler">キーボードが操作されたときに実行するメソッドを表すイベントハンドラ。</param>
		public KeyboardHook( KeyboardHookedEventHandler handler ) : this() {
			KeyboardHooked += handler;
		}
		private int CallNextHook( int code, KeyboardMessage message, ref KeyboardState state ) {
			if( code >= 0 ) {
				KeyboardHookedEventArgs e = new KeyboardHookedEventArgs( message, ref state );
				OnKeyboardHooked( e );
				if( e.Cancel ) {
					return -1;
				}
			}
			return CallNextHookEx( IntPtr.Zero, code, message, ref state );
		}
		///<summary>
		///使用されているアンマネージリソースを解放し、オプションでマネージリソースも解放する。
		///</summary>
		///<param name="disposing">マネージリソースも解放する場合はtrue。</param>
		protected override void Dispose( bool disposing ) {
			if( this._hookDelegate.IsAllocated ) {
				UnhookWindowsHookEx( this._hook );
				this._hook = IntPtr.Zero;
				this._hookDelegate.Free();
			}
			base.Dispose( disposing );
		}
	}
}