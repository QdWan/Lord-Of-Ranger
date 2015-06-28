using System;
using System.IO;

namespace LordOfRanger.Setting.Version {
	class V {
		private readonly int VERSION = 2;
		private If vif;
		Mass instance;
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

		private static int getVersion(string filename) {
			FileStream fs = new FileStream( Mass.setting_path + filename + Mass.EXTENSION, FileMode.Open, FileAccess.Read );
			byte[] array = new byte[fs.Length];

			fs.Read( array, 0, (int)fs.Length );
			fs.Close();

			int offset = 0;
			return BitConverter.ToInt32( array, offset );
		}

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

		internal void Save() {
			vif.Save();
		}

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
