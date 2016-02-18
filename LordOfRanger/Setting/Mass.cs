using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LordOfRanger.Setting.Version;



namespace LordOfRanger.Setting {
	/// <summary>
	/// ユーザーが作成した設定ファイル1つ分を纏めるクラス
	/// 中身はDataAbを継承したCommand,Toggle,Barrage,Mouseクラスのインスタンスの配列
	/// </summary>
	internal class Mass {
		internal static readonly string SETTING_PATH = Application.StartupPath + "/setting/";
		internal string name;
		internal byte hotKey = 0x00;
		internal const string EXTENSION = ".ard";
		private List<DataAb> _value = new List<DataAb>();
		internal IEnumerable<DataAb> Value {
			get {
				return this._value;
			}
		}
		internal BarrageList barrageList;
		internal CommandList commandList;
		internal ToggleList toggleList;
		internal MouseList mouseList;
		private byte[] _cancelList = {};
		internal IEnumerable<byte> CancelList {
			get {
				return this._cancelList;
			}
		}

		internal class CommandList {
			private List<Command> _value = new List<Command>();
			internal IEnumerable<Command> Value {
				get {
					return this._value;
				}
			}
			private byte[] _cancelList = {};
			internal IEnumerable<byte> CancelList {
				get {
					return this._cancelList;
				}
			}

			internal void Add( Command instance ) {
				if( instance.Type == DataAb.InstanceType.COMMAND ) {
					this._value.Add( instance );
					CancelListReBuild();
				}
			}
			internal void RemoveAt( int sequence ) {
				for( var j = 0; j < this._value.Count; j++ ) {
					if( this._value[j].Id == sequence ) {
						this._value.RemoveAt( j );
						CancelListReBuild();
						return;
					}
				}
			}
			private void CancelListReBuild() {
				if( !Options.Options.options.keyboardCancelCommand ) {
					this._cancelList = new byte[0];
					return;
				}
				var tmp = new List<byte>();
				foreach( var val in Value ) {
					tmp.AddRange( val.Push );
				}
				this._cancelList = tmp.Distinct().ToArray();
			}
		}
		internal class BarrageList {
			private List<Barrage> _value = new List<Barrage>();
			internal IEnumerable<Barrage> Value {
				get {
					return this._value;
				}
			}
			private byte[] _cancelList = {};
			internal IEnumerable<byte> CancelList {
				get {
					return this._cancelList;
				}
			}

			internal void Add( Barrage instance ) {
				if( instance.Type == DataAb.InstanceType.BARRAGE ) {
					this._value.Add( instance );
					CancelListReBuild();
				}
			}
			internal void RemoveAt( int sequence ) {
				for( var j = 0; j < this._value.Count; j++ ) {
					if( this._value[j].Id == sequence ) {
						this._value.RemoveAt( j );
						CancelListReBuild();
						return;
					}
				}
			}
			private void CancelListReBuild() {
				if( !Options.Options.options.keyboardCancelBarrage ) {
					this._cancelList = new byte[0];
					return;
				}
				var tmp = new List<byte>();
				foreach( var val in Value ) {
					tmp.AddRange( val.Push );
				}
				this._cancelList = tmp.Distinct().ToArray();
			}
		}
		internal class ToggleList {
			private List<Toggle> _value = new List<Toggle>();
			internal IEnumerable<Toggle> Value {
				get {
					return this._value;
				}
			}
			private byte[] _cancelList = {};
			internal IEnumerable<byte> CancelList {
				get {
					return this._cancelList;
				}
			}

			internal void Add( Toggle instance ) {
				if( instance.Type == DataAb.InstanceType.TOGGLE ) {
					this._value.Add( instance );
					CancelListReBuild();
				}
			}
			internal void RemoveAt( int sequence ) {
				for( var j = 0; j < this._value.Count; j++ ) {
					if( this._value[j].Id == sequence ) {
						this._value.RemoveAt( j );
						CancelListReBuild();
						return;
					}
				}
			}
			private void CancelListReBuild() {
				if( !Options.Options.options.keyboardCancelToggle ) {
					this._cancelList = new byte[0];
					return;
				}
				var tmp = new List<byte>();
				foreach( var val in Value ) {
					tmp.AddRange( val.Push );
				}
				this._cancelList = tmp.Distinct().ToArray();
			}
		}
		internal class MouseList {
			private List<Mouse> _value = new List<Mouse>();
			internal IEnumerable<Mouse> Value {
				get {
					return this._value;
				}
			}
			private byte[] _cancelList = {};
			internal IEnumerable<byte> CancelList {
				get {
					return this._cancelList;
				}
			}

			internal void Add( Mouse instance ) {
				if( instance.Type == DataAb.InstanceType.MOUSE ) {
					this._value.Add( instance );
					CancelListReBuild();
				}
			}
			internal void RemoveAt( int sequence ) {
				for( var j = 0; j < this._value.Count; j++ ) {
					if( this._value[j].Id == sequence ) {
						this._value.RemoveAt( j );
						CancelListReBuild();
						return;
					}
				}
			}
			private void CancelListReBuild() {
				if( !Options.Options.options.keyboardCancelMouse ) {
					this._cancelList = new byte[0];
					return;
				}
				var tmp = new List<byte>();
				foreach( var val in Value ) {
					tmp.AddRange( val.Push );
				}
				this._cancelList = tmp.Distinct().ToArray();
			}
		}


		internal int Sequence {
			get;
			set;
		}


		/// <summary>
		/// コンストラクタ
		/// 各変数に初期値を代入する
		/// </summary>
		internal Mass() {
			this.name = "new";
			Sequence = 0;
			Init();
		}

		#region Data Interface

		/// <summary>
		/// 行追加
		/// </summary>
		/// <param name="instance"> 追加するインスタンス </param>
		/// <returns> 追加されたインスタンスのid </returns>
		internal int Add( DataAb instance ) {
			if( instance.Id == 0 ) {
				instance.Id = ++Sequence;
			}
			this._value.Add( instance );

			switch( instance.Type ) {
				case DataAb.InstanceType.BARRAGE:
					this.barrageList.Add( (Barrage)instance );
					break;
				case DataAb.InstanceType.COMMAND:
					this.commandList.Add( (Command)instance );
					break;
				case DataAb.InstanceType.TOGGLE:
					this.toggleList.Add( (Toggle)instance );
					break;
				case DataAb.InstanceType.MOUSE:
					this.mouseList.Add( (Mouse)instance );
					break;
			}
			CancelListReBuild();
			return instance.Id;
		}

		/// <summary>
		/// 行削除
		/// </summary>
		/// <param name="sequence"> 削除するインスタンスのid </param>
		/// <returns> 削除が成功したかどうか </returns>
		internal void RemoveAt( int sequence ) {
			for( var i = 0; i < this._value.Count; i++ ) {
				if( this._value[i].Id == sequence ) {
					var instanceType = this._value[i].Type;

					switch( instanceType ) {
						case DataAb.InstanceType.BARRAGE:
							this.barrageList.RemoveAt( sequence );
							break;
						case DataAb.InstanceType.COMMAND:
							this.commandList.RemoveAt( sequence );
							break;
						case DataAb.InstanceType.TOGGLE:
							this.toggleList.RemoveAt( sequence );
							break;
						case DataAb.InstanceType.MOUSE:
							this.mouseList.RemoveAt( sequence );
							break;
					}
					this._value.RemoveAt( i );
					break;
				}
			}
			CancelListReBuild();
		}

		/// <summary>
		/// 行を1行上へ移動
		/// </summary>
		/// <param name="sequence"> 移動するインスタンスのid </param>
		/// <returns> 移動が成功したかどうか </returns>
		internal bool UpAt( int sequence ) {
			for( var i = 0; i < this._value.Count; i++ ) {
				if( this._value[i].Id == sequence ) {
					var da = this._value[i];
					this._value.RemoveAt( i );
					this._value.Insert( i - 1, da );
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 行を1行下へ移動
		/// </summary>
		/// <param name="sequence"> 移動するインスタンスのid </param>
		/// <returns> 移動が成功したかどうか </returns>
		internal bool DownAt( int sequence ) {
			for( var i = 0; i < this._value.Count; i++ ) {
				if( this._value[i].Id == sequence ) {
					var da = this._value[i];
					this._value.RemoveAt( i );
					this._value.Insert( i + 1, da );
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 有効無効の切り替え
		/// </summary>
		/// <param name="sequence"> 有効無効を切り替えるインスタンスのid </param>
		/// <param name="enable"> 有効無効どちらに切り替えるか </param>
		/// <returns> 切り替えが成功したかどうか </returns>
		internal bool ChangeEnable( int sequence, bool enable ) {
			foreach( var t in this._value.Where( t => t.Id == sequence ) ) {
				t.Enable = enable;
				return true;
			}
			return false;
		}

		#endregion

		/// <summary>
		/// データ配列の初期化
		/// </summary>
		internal void Init() {
			this._value = new List<DataAb>();
			this.barrageList = new BarrageList();
			this.commandList = new CommandList();
			this.toggleList = new ToggleList();
			this.mouseList = new MouseList();
		}

		/// <summary>
		/// この設定ファイルの保存
		/// </summary>
		internal void Save() {
			var v = new V( this );
			v.Save();
		}

		/// <summary>
		/// 設定ファイルの読み込み
		/// </summary>
		/// <param name="filename"> 読み込むファイル名 </param>
		internal void Load( string filename ) {
			try {
				var v = new V( this );
				v.Load( filename );
			} catch( Exception ) {
				MessageBox.Show(filename+"設定ファイルを読み込めませんでした。");
			}
		}

		/// <summary>
		/// ファイルを切り替えるホットキーを取得する
		/// </summary>
		/// <param name="filename"> 取得するファイル名 </param>
		/// <returns> ホットキー </returns>
		internal static byte GetHotKey( string filename ) {
			try {
				return V.GetHotKey( filename );
			} catch( Exception ) {
				MessageBox.Show( filename + "設定ファイルを読み込めませんでした。" );
				return 0x00;
			}
		}
		private void CancelListReBuild() {
			this._cancelList = this.barrageList.CancelList.Concat(this.commandList.CancelList).Concat(this.toggleList.CancelList).Concat(this.mouseList.CancelList ).Distinct().ToArray();
		}
	}
}
