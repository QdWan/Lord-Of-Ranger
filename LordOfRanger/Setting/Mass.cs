using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LordOfRanger.Setting.Version;



namespace LordOfRanger.Setting {
	/// <summary>
	/// ユーザーが作成した設定ファイル1つ分を纏めるクラス
	/// 中身はActを継承したCommand,Toggle,Barrage,Mouseクラスのインスタンスの配列
	/// </summary>
	internal class Mass {
		internal static readonly string SETTING_PATH = Application.StartupPath + "/setting/";
		internal string name;
		internal byte hotKey = 0x00;
		internal const string EXTENSION = ".ard";
		private List<Act> _value = new List<Act>();
		internal IEnumerable<Act> Value {
			get {
				return this._value;
			}
		}
		private ActList<Barrage> _barrageList;
		internal IEnumerable<Barrage> Barrages {
			get {
				return this._barrageList.Value;
			}
		}
		private ActList<Command> _commandList;
		internal IEnumerable<Command> Commands {
			get {
				return this._commandList.Value;
			}
		}
		private ActList<Toggle> _toggleList;
		internal IEnumerable<Toggle> Toggles {
			get {
				return this._toggleList.Value;
			}
		}
		private ActList<Mouse> _mouseList;
		internal IEnumerable<Mouse> Mice {
			get {
				return this._mouseList.Value;
			}
		}
		private byte[] _cancelList = {};
		internal IEnumerable<byte> CancelList {
			get {
				return this._cancelList;
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

		internal void Reload() {
			Load(this.name);
		}

		#region Data Interface

		/// <summary>
		/// 行追加
		/// </summary>
		/// <param name="instance"> 追加するインスタンス </param>
		/// <returns> 追加されたインスタンスのid </returns>
		internal int Add( Act instance ) {
			if( instance.Id == 0 ) {
				instance.Id = ++Sequence;
			}
			this._value.Add( instance );

			switch( instance.Type ) {
				case Act.InstanceType.BARRAGE:
					this._barrageList.Add( (Barrage)instance );
					break;
				case Act.InstanceType.COMMAND:
					this._commandList.Add( (Command)instance );
					break;
				case Act.InstanceType.TOGGLE:
					this._toggleList.Add( (Toggle)instance );
					break;
				case Act.InstanceType.MOUSE:
					this._mouseList.Add( (Mouse)instance );
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
						case Act.InstanceType.BARRAGE:
							this._barrageList.RemoveAt( sequence );
							break;
						case Act.InstanceType.COMMAND:
							this._commandList.RemoveAt( sequence );
							break;
						case Act.InstanceType.TOGGLE:
							this._toggleList.RemoveAt( sequence );
							break;
						case Act.InstanceType.MOUSE:
							this._mouseList.RemoveAt( sequence );
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

		/// <summary>
		/// キーボードキャンセル有効無効の切り替え
		/// </summary>
		/// <param name="sequence"> 有効無効を切り替えるインスタンスのid </param>
		/// <param name="enable"> 有効無効どちらに切り替えるか </param>
		/// <returns></returns>
		internal void ChangeKeyboardCancel( int sequence, bool enable) {
			foreach( var x in this._value.Where( x => x.Id == sequence ) ) {
				x.KeyboardCancel = enable;
				break;
			}
			CancelListReBuild();
		}

		#endregion

		/// <summary>
		/// データ配列の初期化
		/// </summary>
		internal void Init() {
			this._value = new List<Act>();
			this._barrageList = new ActList<Barrage>();
			this._commandList = new ActList<Command>();
			this._toggleList = new ActList<Toggle>();
			this._mouseList = new ActList<Mouse>();
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
			this._cancelList = this._barrageList.CancelList.Concat(this._commandList.CancelList).Concat(this._toggleList.CancelList).Concat(this._mouseList.CancelList ).Distinct().ToArray();
		}
	}
}
