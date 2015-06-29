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

		/// <summary>
		/// 最新のバージョン
		/// </summary>
		private readonly int VERSION = 2;

		private If vif;
		private Mass instance;

		/// <summary>
		/// コンストラクタ
		/// 操作するインスタンスを使って、各バージョンのインスタンスを作成する
		/// VERSIONはreadonlyなので基本的には最新版のインスタンスしか生成されない
		/// </summary>
		/// <param name="instance"> 操作する対象のインスタンス </param>
		internal V(Mass instance) {
			this.instance = instance;
			switch( VERSION ) {
				case 1:
					vif = new V1( instance );
					break;
				case 2:
					vif = new V2( instance );
					break;
			}
		}

		/// <summary>
		/// インスタンスのバージョンを取得する
		/// </summary>
		/// <param name="filename"> バージョンを取得するファイルのファイル名 </param>
		/// <returns> バージョン </returns>
		private static int getVersion(string filename) {
			FileStream fs = new FileStream( Mass.setting_path + filename + Mass.EXTENSION, FileMode.Open, FileAccess.Read );
			byte[] array = new byte[fs.Length];

			fs.Read( array, 0, (int)fs.Length );
			fs.Close();

			int offset = 0;
			return BitConverter.ToInt32( array, offset );
		}

		/// <summary>
		/// ファイルの読み込み
		/// ファイルのバージョンを取得してから読み込む
		/// バージョンによって欠落している要素があればデフォルト値が入る
		/// </summary>
		/// <param name="filename"> 読み込むファイルのファイル名 </param>
		internal void Load(string filename) {
			switch( getVersion( filename ) ) {
				case 1:
					vif = new V1( instance );
					break;
				case 2:
					vif = new V2( instance );
					break;
				default:
					return;
			}
			vif.Load( filename );
		}

		/// <summary>
		/// 最新バージョンで保存する
		/// </summary>
		internal void Save() {
			vif.Save();
		}

		/// <summary>
		/// ファイルのホットキーを取得する
		/// </summary>
		/// <param name="filename"> ファイルのホットキーを取得する </param>
		/// <returns></returns>
		internal static byte getHotKey(string filename) {
			switch( getVersion( filename ) ) {
				case 1:
					return V1.getHotKey( filename );
				case 2:
					return V2.getHotKey( filename );
				default:
					return 0x00;
			}
		}
	}
}
