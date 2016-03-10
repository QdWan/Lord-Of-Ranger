using System;
using System.IO;

namespace LordOfRanger.Common {
	class Logging {

		private readonly string _filename;
		internal Logging(string filename) {
			var logDir = Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location ) + Path.DirectorySeparatorChar + "log" + Path.DirectorySeparatorChar;
			if( !Directory.Exists( logDir ) ) {
				Directory.CreateDirectory( logDir );
			}
			this._filename = logDir +  filename;
		}

		internal void Write( Exception ex ) {
			var text = DateTime.Now.ToString( "yyyy/MM/dd HH:mm:ss | " ) + ex.StackTrace + "|" + ex.Message + Environment.NewLine;
			File.AppendAllText( this._filename, text );
		}
	}
}