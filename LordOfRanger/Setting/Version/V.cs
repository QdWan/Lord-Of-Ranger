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
	static class V {

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
		internal static Mass Load( string filename ) {
			IF vif;
			switch( GetVersion( filename ) ) {
				case 1:
					vif = new V1();
					break;
				case 2:
					vif = new V2();
					break;
				case 3:
					vif = new V3();
					break;
				case 4:
					vif = new V4();
					break;
				case 5:
					vif = new V5();
					break;
				case 6:
					vif = new Current();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			return vif.Load( filename );
		}

		/// <summary>
		/// 最新バージョンで保存する
		/// </summary>
		internal static void Save(Mass mass) {
			(new Current()).Save(mass);
		}

		/// <summary>
		/// ファイルのホットキーを取得する
		/// </summary>
		/// <param name="filename"> ファイルのホットキーを取得する </param>
		/// <returns></returns>
		internal static byte GetHotKey( string filename ) {
			IF vif;
			switch( GetVersion( filename ) ) {
				case 1:
					vif = new V1();
					break;
				case 2:
					vif = new V2();
					break;
				case 3:
					vif = new V3();
					break;
				case 4:
					vif = new V4();
					break;
				case 5:
					vif = new V5();
					break;
				case 6:
					vif = new Current();
					break;
				default:
					return 0x00;
			}
			return vif.GetHotKey( filename );
		}
	}
}
