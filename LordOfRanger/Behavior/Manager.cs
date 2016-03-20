using System;
using System.IO;

namespace LordOfRanger.Behavior {
	/// <summary>
	/// 設定ファイルのバージョンを管理するクラス
	/// このクラスを通して設定ファイルを読み込む
	/// 読み込んだバージョンがいくつであろうと、保存する際は必ず最新バージョンで保存される。
	/// 尚、プログラムのバージョンと設定のバージョンは別に管理する。
	/// </summary>
	static class Manager {

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
			switch( GetVersion( filename ) ) {
				case 1:
					return V1.Load( filename );
				case 2:
					return V2.Load( filename );
				case 3:
					return V3.Load( filename );
				case 4:
					return V4.Load( filename );
				case 5:
					return V5.Load( filename );
				case 6:
					return Current.Load( filename );
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// 最新バージョンで保存する
		/// </summary>
		internal static void Save( Mass mass ) {
			Current.Save( mass );
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
					return Current.GetHotKey( filename );
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
