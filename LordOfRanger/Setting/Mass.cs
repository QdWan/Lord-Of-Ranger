﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
namespace LordOfRanger.Setting {
	/// <summary>
	/// ユーザーが作成した設定ファイル1つ分を纏めるクラス
	/// 中身はDataAbを継承したCommand,Toggle,Barrageクラスのインスタンスの配列
	/// </summary>
	internal class Mass {
		internal static string setting_path = Application.StartupPath + "/setting/";
		internal string Name;
		internal byte HotKey = 0x00;
		internal const string EXTENSION = ".ard";

		private List<DataAb> _dataList;
		private List<Command> _commandList;
		private List<Barrage> _barrageList;
		private List<Toggle> _toggleList;
		internal int sequence;

		
		/// <summary>
		/// このクラスの要
		/// 全設定行をまとめた配列
		/// </summary>
		internal DataAb[] DataList {
			get {
				return _dataList.ToArray();
			}
		}

		/// <summary>
		/// コマンド行のみをまとめた配列
		/// </summary>
		internal Command[] commandList {
			get {
				return _commandList.ToArray();
			}
		}

		/// <summary>
		/// 連打行のみをまとめた配列
		/// </summary>
		internal Barrage[] barrageList {
			get {
				return _barrageList.ToArray();
			}
		}

		/// <summary>
		/// トグル行のみをまとめた配列
		/// </summary>
		internal Toggle[] toggleList {
			get {
				return _toggleList.ToArray();
			}
		}

		/// <summary>
		/// コンストラクタ
		/// 各変数に初期値を代入する
		/// </summary>
		internal Mass() {
			Name = "new";
			sequence = 0;
			_dataList = new List<DataAb>();
			_commandList = new List<Command>();
			_barrageList = new List<Barrage>();
			_toggleList = new List<Toggle>();
		}

		#region Data Interface

		/// <summary>
		/// 行追加
		/// </summary>
		/// <param name="instance"> 追加するインスタンス </param>
		/// <returns> 追加されたインスタンスのid </returns>
		internal int Add(DataAb instance) {
			if( instance.id == 0 ) {
				instance.id = ++sequence;
			}
			_dataList.Add( instance );
			switch( instance.type ) {
				case DataAb.Type.BARRAGE:
					_barrageList.Add( (Barrage)instance );
					break;
				case DataAb.Type.COMMAND:
					_commandList.Add( (Command)instance );
					break;
				case DataAb.Type.TOGGLE:
					_toggleList.Add( (Toggle)instance );
					break;
			}
			return instance.id;
		}

		/// <summary>
		/// 行削除
		/// </summary>
		/// <param name="sequence"> 削除するインスタンスのid </param>
		/// <returns> 削除が成功したかどうか </returns>
		internal bool RemoveAt(int sequence) {
			for( int i = 0; i < _dataList.Count; i++ ) {
				if( _dataList[i].id == sequence ) {
					DataAb.Type type = _dataList[i].type;
					_dataList.RemoveAt( i );
					
					switch( type ) {
						case DataAb.Type.BARRAGE:
							for( int j = 0; j < _barrageList.Count; j++ ) {
								if( _barrageList[j].id == sequence ) {
									_barrageList.RemoveAt( j );
									return true;
								}
							}
							break;
						case DataAb.Type.COMMAND:
							for( int j = 0; j < _commandList.Count; j++ ) {
								if( _commandList[j].id == sequence ) {
									_commandList.RemoveAt( j );
									return true;
								}
							}
							break;
						case DataAb.Type.TOGGLE:
							for( int j = 0; j < _toggleList.Count; j++ ) {
								if( _toggleList[j].id == sequence ) {
									_toggleList.RemoveAt( j );
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
		internal bool UpAt(int sequence) {
			for( int i = 0; i < _dataList.Count; i++ ) {
				if( _dataList[i].id == sequence ) {
					DataAb da = DataList[i];
					_dataList.RemoveAt( i );
					_dataList.Insert( i - 1, da );
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
		internal bool DownAt(int sequence) {
			for( int i = 0; i < _dataList.Count; i++ ) {
				if( _dataList[i].id == sequence ) {
					DataAb da = DataList[i];
					_dataList.RemoveAt( i );
					_dataList.Insert( i + 1, da );
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
		internal bool changeEnable(int sequence, bool enable) {
			for( int i = 0; i < _dataList.Count; i++ ) {
				if( _dataList[i].id == sequence ) {
					_dataList[i].enable = enable;
					return true;
				}
			}
			return false;
		}

		#endregion

		/// <summary>
		/// データ配列の初期化
		/// </summary>
		internal void init() {
			_dataList = new List<DataAb>();
			_commandList = new List<Command>();
			_barrageList = new List<Barrage>();
			_toggleList = new List<Toggle>();
		}

		/// <summary>
		/// この設定ファイルの保存
		/// </summary>
		internal void save() {
			Version.V v = new Version.V( this );
			v.Save();
		}

		/// <summary>
		/// 設定ファイルの読み込み
		/// </summary>
		/// <param name="filename"> 読み込むファイル名 </param>
		internal void load(string filename) {
			Version.V v = new Version.V( this );
			v.Load( filename );
		}

		/// <summary>
		/// ファイルを切り替えるホットキーを取得する
		/// </summary>
		/// <param name="filename"> 取得するファイル名 </param>
		/// <returns> ホットキー </returns>
		internal static byte getHotKey(string filename) {
			return Version.V.getHotKey( filename );
		}
	}
}