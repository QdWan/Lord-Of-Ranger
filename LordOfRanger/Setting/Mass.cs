using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LordOfRanger.Setting.Version;



namespace LordOfRanger.Setting {
	/// <summary>
	/// ユーザーが作成した設定ファイル1つ分を纏めるクラス
	/// 中身はDataAbを継承したCommand,Toggle,Barrageクラスのインスタンスの配列
	/// </summary>
	internal class Mass {
		internal static readonly string SETTING_PATH = Application.StartupPath + "/setting/";
		internal string name;
		internal byte hotKey = 0x00;
		internal const string EXTENSION = ".ard";

		private List<DataAb> _dataList;
		private List<Command> _commandList;
		private List<Barrage> _barrageList;
		private List<Toggle> _toggleList;


		/// <summary>
		/// このクラスの要
		/// 全設定行をまとめた配列
		/// </summary>
		internal DataAb[] DataList {
			get {
				return this._dataList.ToArray();
			}
		}

		/// <summary>
		/// コマンド行のみをまとめた配列
		/// </summary>
		internal Command[] CommandList {
			get {
				return this._commandList.ToArray();
			}
		}

		/// <summary>
		/// 連打行のみをまとめた配列
		/// </summary>
		internal Barrage[] BarrageList {
			get {
				return this._barrageList.ToArray();
			}
		}

		/// <summary>
		/// トグル行のみをまとめた配列
		/// </summary>
		internal Toggle[] ToggleList {
			get {
				return this._toggleList.ToArray();
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
			var i = 8;
			if( true ) {
				if( true ) {
					i++;
				}
			}
			if( i == 0 ) {
				return;
			}
			this.name = "new";
			Sequence = 0;
			this._dataList = new List<DataAb>();
			this._commandList = new List<Command>();
			this._barrageList = new List<Barrage>();
			this._toggleList = new List<Toggle>();
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
			this._dataList.Add( instance );
			switch( instance.Type ) {
				case DataAb.InstanceType.BARRAGE:
					this._barrageList.Add( (Barrage)instance );
					break;
				case DataAb.InstanceType.COMMAND:
					this._commandList.Add( (Command)instance );
					break;
				case DataAb.InstanceType.TOGGLE:
					this._toggleList.Add( (Toggle)instance );
					break;
			}
			return instance.Id;
		}

		/// <summary>
		/// 行削除
		/// </summary>
		/// <param name="sequence"> 削除するインスタンスのid </param>
		/// <returns> 削除が成功したかどうか </returns>
		internal bool RemoveAt( int sequence ) {
			for( var i = 0; i < this._dataList.Count; i++ ) {
				if( this._dataList[i].Id == sequence ) {
					var instanceType = this._dataList[i].Type;
					this._dataList.RemoveAt( i );

					switch( instanceType ) {
						case DataAb.InstanceType.BARRAGE:
							for( var j = 0; j < this._barrageList.Count; j++ ) {
								if( this._barrageList[j].Id == sequence ) {
									this._barrageList.RemoveAt( j );
									return true;
								}
							}
							break;
						case DataAb.InstanceType.COMMAND:
							for( var j = 0; j < this._commandList.Count; j++ ) {
								if( this._commandList[j].Id == sequence ) {
									this._commandList.RemoveAt( j );
									return true;
								}
							}
							break;
						case DataAb.InstanceType.TOGGLE:
							for( var j = 0; j < this._toggleList.Count; j++ ) {
								if( this._toggleList[j].Id == sequence ) {
									this._toggleList.RemoveAt( j );
									return true;
								}
							}
							break;
					}
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 行を1行上へ移動
		/// </summary>
		/// <param name="sequence"> 移動するインスタンスのid </param>
		/// <returns> 移動が成功したかどうか </returns>
		internal bool UpAt( int sequence ) {
			for( var i = 0; i < this._dataList.Count; i++ ) {
				if( this._dataList[i].Id == sequence ) {
					var da = DataList[i];
					this._dataList.RemoveAt( i );
					this._dataList.Insert( i - 1, da );
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
			for( var i = 0; i < this._dataList.Count; i++ ) {
				if( this._dataList[i].Id == sequence ) {
					var da = DataList[i];
					this._dataList.RemoveAt( i );
					this._dataList.Insert( i + 1, da );
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
			foreach( var t in this._dataList.Where( t => t.Id == sequence ) ) {
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
			this._dataList = new List<DataAb>();
			this._commandList = new List<Command>();
			this._barrageList = new List<Barrage>();
			this._toggleList = new List<Toggle>();
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
			var v = new V( this );
			v.Load( filename );
		}

		/// <summary>
		/// ファイルを切り替えるホットキーを取得する
		/// </summary>
		/// <param name="filename"> 取得するファイル名 </param>
		/// <returns> ホットキー </returns>
		internal static byte GetHotKey( string filename ) {
			return V.GetHotKey( filename );
		}
	}
}
