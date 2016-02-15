using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		internal class CommandList {
			internal List<Command> Value {
				get;
			} = new List<Command>();

			internal void Add( Command instance ) {
				if( instance.Type == DataAb.InstanceType.COMMAND ) {
					Value.Add( instance );
				}
			}
			internal void RemoveAt( int sequence ) {
				for( var j = 0; j < Value.Count; j++ ) {
					if( Value[j].Id == sequence ) {
						Value.RemoveAt( j );
						return;
					}
				}
			}
		}
		internal class BarrageList {
			internal List<Barrage> Value {
				get;
			} = new List<Barrage>();

			internal void Add( Barrage instance ) {
				if( instance.Type == DataAb.InstanceType.BARRAGE ) {
					Value.Add( instance );
				}
			}
			internal void RemoveAt( int sequence ) {
				for( var j = 0; j < Value.Count; j++ ) {
					if( Value[j].Id == sequence ) {
						Value.RemoveAt( j );
						return;
					}
				}
			}
		}
		internal class ToggleList {
			internal List<Toggle> Value {
				get;
			} = new List<Toggle>();

			internal void Add( Toggle instance ) {
				if( instance.Type == DataAb.InstanceType.TOGGLE ) {
					Value.Add( instance );
				}
			}
			internal void RemoveAt( int sequence ) {
				for( var j = 0; j < Value.Count; j++ ) {
					if( Value[j].Id == sequence ) {
						Value.RemoveAt( j );
						return;
					}
				}
			}
		}
		internal class MouseList {
			internal List<Mouse> Value {
				get;
			} = new List<Mouse>();

			internal void Add( Mouse instance ) {
				if( instance.Type == DataAb.InstanceType.MOUSE ) {
					Value.Add( instance );
				}
			}
			internal void RemoveAt( int sequence ) {
				for( var j = 0; j < Value.Count; j++ ) {
					if( Value[j].Id == sequence ) {
						Value.RemoveAt( j );
						return;
					}
				}
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

	}
}
