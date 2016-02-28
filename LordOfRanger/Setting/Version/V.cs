using System;
using System.IO;



namespace LordOfRanger.Setting.Version {
	/// <summary>
	/// 設定ファイルのバージョンを管理するクラス
	/// このクラスを通して、Version1,Version2などの設定ファイルを読み込む
	/// 保存する際は必ず最新バージョンで保存される。
	/// バージョンを追加した際には、VERSIONの数値を書き換え、常に最新を参照するようにする。
	/// 尚、プログラムのバージョンと設定のバージョンは別に管理する。
	/// </summary>
	class V {
		private IF _vif;
		private readonly Mass _instance;

		/// <summary>
		/// コンストラクタ
		/// 操作するインスタンスを使って、最新バージョンのインスタンスを作成する
		/// </summary>
		/// <param name="instance"> 操作する対象のインスタンス </param>
		internal V( Mass instance ) {
			this._instance = instance;
			this._vif = new V6( instance );
		}

		/// <summary>
		/// インスタンスのバージョンを取得する
		/// </summary>
		/// <param name="filename"> バージョンを取得するファイルのファイル名 </param>
		/// <returns> バージョン </returns>
		private static int GetVersion( string filename ) {
			var fs = new FileStream( Mass.SETTING_PATH + filename + Mass.EXTENSION, FileMode.Open, FileAccess.Read );
			var array = new byte[fs.Length];

			fs.Read( array, 0, (int)fs.Length );
			fs.Close();

			return BitConverter.ToInt32( array, 0 );
		}

		/// <summary>
		/// ファイルの読み込み
		/// ファイルのバージョンを取得してから読み込む
		/// バージョンによって欠落している要素があればデフォルト値が入る
		/// </summary>
		/// <param name="filename"> 読み込むファイルのファイル名 </param>
		internal void Load( string filename ) {
			switch( GetVersion( filename ) ) {
				case 1:
					this._vif = new V1( this._instance );
					break;
				case 2:
					this._vif = new V2( this._instance );
					break;
				case 3:
					this._vif = new V3(this._instance );
					break;
				case 4:
					this._vif = new V4( this._instance );
					break;
				case 5:
					this._vif = new V5( this._instance );
					break;
				case 6:
					this._vif = new V6( this._instance );
					break;
				default:
					return;
			}
			this._vif.Load( filename );
		}

		/// <summary>
		/// 最新バージョンで保存する
		/// </summary>
		internal void Save() {
			this._vif.Save();
		}

		/// <summary>
		/// ファイルのホットキーを取得する
		/// </summary>
		/// <param name="filename"> ファイルのホットキーを取得する </param>
		/// <returns></returns>
		internal static byte GetHotKey( string filename ) {
			switch( GetVersion( filename ) ) {
				case 1:
					return V1.GetHotKey( filename );
				case 2:
					return V2.GetHotKey( filename );
				case 3:
					return V3.GetHotKey( filename );
				case 4:
					return V4.GetHotKey( filename );
				case 5:
					return V5.GetHotKey( filename );
				case 6:
					return V6.GetHotKey( filename );
				default:
					return 0x00;
			}
		}
	}
}
