using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Drawing;

namespace LordOfRanger.Setting.Version {
	internal class V1 : If {
		private Mass mass;
		private const int VERSION = 1;

		public V1(Mass instance) {
			mass = instance;
		}

		private struct ArdHeader {
			internal int id;
			internal DataAb.Type type;
			internal int priority;
			internal int skillIconSize;
			internal int disableSkillIconSize;
			internal int pushDataSize;
			internal int sendDataSize;
		}

		public void Load(string filename) {
			mass.init();
			mass.Name = filename;

			FileStream fs = new FileStream( Mass.setting_path + mass.Name + Mass.EXTENSION, FileMode.Open, FileAccess.Read );
			byte[] array = new byte[fs.Length];

			fs.Read( array, 0, (int)fs.Length );
			fs.Close();

			int offset = 0;
			int version = BitConverter.ToInt32( array, offset );
			offset += 4;

			if( version != VERSION ) {
				return;
			}

			int titleSize = BitConverter.ToInt32( array, offset );
			offset += 4;
			int hotKeySize = BitConverter.ToInt32( array, offset );
			offset += 4;
			int headerSize = BitConverter.ToInt32( array, offset );
			offset += 4;
			mass.sequence = BitConverter.ToInt32( array, offset );
			offset += 4;
			string title = Encoding.UTF8.GetString( array, offset, titleSize );
			offset += titleSize;
			mass.HotKey = array.Skip( offset ).Take( hotKeySize ).ToArray()[0];
			offset += hotKeySize;
			int headerCount = headerSize / 28;
			List<ArdHeader> headers = new List<ArdHeader>();
			for( int i = 0; i < headerCount; i++ ) {
				ArdHeader ardHeader = new ArdHeader();
				ardHeader.id = BitConverter.ToInt32( array, offset );
				offset += 4;
				ardHeader.type = (DataAb.Type)BitConverter.ToInt32( array, offset );
				offset += 4;
				ardHeader.priority = BitConverter.ToInt32( array, offset );
				offset += 4;
				ardHeader.skillIconSize = BitConverter.ToInt32( array, offset );
				offset += 4;
				ardHeader.disableSkillIconSize = BitConverter.ToInt32( array, offset );
				offset += 4;
				ardHeader.pushDataSize = BitConverter.ToInt32( array, offset );
				offset += 4;
				ardHeader.sendDataSize = BitConverter.ToInt32( array, offset );
				offset += 4;
				headers.Add( ardHeader );
			}

			foreach( ArdHeader ardHeader in headers ) {
				switch( ardHeader.type ) {
					case DataAb.Type.COMMAND:
						Command c = new Command();
						c.id = ardHeader.id;
						c.priority = ardHeader.priority;
						c.skillIcon = binaryToBitmap( array.Skip( offset ).Take( ardHeader.skillIconSize ).ToArray() );
						offset += ardHeader.skillIconSize;
						c.disableSkillIcon = binaryToBitmap( array.Skip( offset ).Take( ardHeader.disableSkillIconSize ).ToArray() );
						offset += ardHeader.disableSkillIconSize;
						c.push = array.Skip( offset ).Take( ardHeader.pushDataSize ).ToArray()[0];
						offset += ardHeader.pushDataSize;
						c.sendList = array.Skip( offset ).Take( ardHeader.sendDataSize ).ToArray();
						offset += ardHeader.sendDataSize;
						mass.Add( c );
						break;
					case DataAb.Type.BARRAGE:
						Barrage b = new Barrage();
						b.id = ardHeader.id;
						b.priority = ardHeader.priority;
						b.skillIcon = binaryToBitmap( array.Skip( offset ).Take( ardHeader.skillIconSize ).ToArray() );
						offset += ardHeader.skillIconSize;
						b.disableSkillIcon = binaryToBitmap( array.Skip( offset ).Take( ardHeader.disableSkillIconSize ).ToArray() );
						offset += ardHeader.disableSkillIconSize;
						b.push = array.Skip( offset ).Take( ardHeader.pushDataSize ).ToArray()[0];
						offset += ardHeader.pushDataSize;
						b.send = array.Skip( offset ).Take( ardHeader.sendDataSize ).ToArray()[0];
						offset += ardHeader.sendDataSize;
						mass.Add( b );
						break;
					case DataAb.Type.TOGGLE:
						Toggle t = new Toggle();
						t.id = ardHeader.id;
						t.priority = ardHeader.priority;
						t.skillIcon = binaryToBitmap( array.Skip( offset ).Take( ardHeader.skillIconSize ).ToArray() );
						offset += ardHeader.skillIconSize;
						t.disableSkillIcon = binaryToBitmap( array.Skip( offset ).Take( ardHeader.disableSkillIconSize ).ToArray() );
						offset += ardHeader.disableSkillIconSize;
						t.push = array.Skip( offset ).Take( ardHeader.pushDataSize ).ToArray()[0];
						offset += ardHeader.pushDataSize;
						t.send = array.Skip( offset ).Take( ardHeader.sendDataSize ).ToArray()[0];
						offset += ardHeader.sendDataSize;
						mass.Add( t );
						break;
				}
			}
		}

		public void Save() {
			if( !Directory.Exists( Mass.setting_path ) ) {
				Directory.CreateDirectory( Mass.setting_path );
				Thread.Sleep( 300 );
			}
			/*
				version 32bit
				This is setting version, not program version.
			*/
			byte[] version = BitConverter.GetBytes( VERSION );
			/*
				titleSize 32bit
			*/
			byte[] titleSize;
			/*
				hotKeySize 32bit
				8 bit only.
			*/
			byte[] hotKeySize;

			/*
				headerSize 32bit
			*/
			byte[] headerSize;

			/* 
				sequence
			*/
			byte[] sequence = BitConverter.GetBytes( mass.sequence );
			/*
				headerSize 32bit
			*/
			/*
				title variable
			*/
			byte[] title = Encoding.UTF8.GetBytes( mass.Name );
			/*
				hotKey 8bit
			*/
			byte hotKey = mass.HotKey;
			/*
				id 32bit
				type 32bit
				priority 32bit
				skillIconSize 32bit
				disableSkillIconSize 32bit
				pushDataSize 32bit
				sendDataSize 32bit
			*/
			List<byte> header = new List<byte>();

			/*
				skillIcon variable
				disableSkillIcon variable
				push 8bit
				(sendList variable || send 8bit)
			*/
			List<byte> data = new List<byte>();
			foreach( DataAb da in mass.DataList ) {
				byte[] skillIcon = (byte[])new ImageConverter().ConvertTo( da.skillIcon, typeof( byte[] ) );
				byte[] disableSkillIcon = (byte[])new ImageConverter().ConvertTo( da.disableSkillIcon, typeof( byte[] ) );
				//id
				header.AddRange( BitConverter.GetBytes( da.id ) );
				//type
				header.AddRange( BitConverter.GetBytes( (int)da.type ) );
				//priority
				header.AddRange( BitConverter.GetBytes( da.priority ) );
				//skillIconSize
				header.AddRange( BitConverter.GetBytes( skillIcon.Length ) );
				//disableSkillIconSize
				header.AddRange( BitConverter.GetBytes( disableSkillIcon.Length ) );


				data.AddRange( skillIcon );
				data.AddRange( disableSkillIcon );
				switch( da.type ) {
					case DataAb.Type.COMMAND:
						//pushDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//push
						data.Add( ( (Command)da ).push );
						//sendDataSize
						header.AddRange( BitConverter.GetBytes( ( ( (Command)da ).sendList.Length ) ) );
						//sendList
						data.AddRange( ( (Command)da ).sendList );
						break;
					case DataAb.Type.BARRAGE:
						//pushDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//push
						data.Add( ( (Barrage)da ).push );
						//sendDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//send
						data.Add( ( (Barrage)da ).send );
						break;
					case DataAb.Type.TOGGLE:
						//pushDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//push
						data.Add( ( (Toggle)da ).push );
						//sendDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//send
						data.Add( ( (Toggle)da ).send );
						break;
				}

			}
			titleSize = BitConverter.GetBytes( title.Length );
			hotKeySize = BitConverter.GetBytes( 1 );
			headerSize = BitConverter.GetBytes( header.Count );

			List<byte> settingBinary = new List<byte>();
			settingBinary.AddRange( version );
			settingBinary.AddRange( titleSize );
			settingBinary.AddRange( hotKeySize );
			settingBinary.AddRange( headerSize );
			settingBinary.AddRange( sequence );
			settingBinary.AddRange( title );
			settingBinary.Add( hotKey );
			settingBinary.AddRange( header );
			settingBinary.AddRange( data );
			FileStream fs = new FileStream( Mass.setting_path + mass.Name + Mass.EXTENSION, FileMode.Create, FileAccess.Write );
			fs.Write( settingBinary.ToArray(), 0, settingBinary.Count );
			fs.Close();
		}

		public static byte getHotKey(string filename) {
			try {
				FileStream fs = new FileStream( Mass.setting_path + filename + Mass.EXTENSION, FileMode.Open, FileAccess.Read );
				byte[] array = new byte[fs.Length];

				fs.Read( array, 0, (int)fs.Length );
				fs.Close();

				int offset = 0;
				int version = BitConverter.ToInt32( array, offset );
				offset += 4;
				int titleSize = BitConverter.ToInt32( array, offset );
				offset += 4;
				int hotKeySize = BitConverter.ToInt32( array, offset );
				offset += 4;
				int headerSize = BitConverter.ToInt32( array, offset );
				offset += 4;
				//	sequence = BitConverter.ToInt32( array, offset );
				offset += 4;
				//	string title = Encoding.UTF8.GetString( array, offset, titleSize );
				offset += titleSize;
				return array.Skip( offset ).Take( hotKeySize ).ToArray()[0];

			} catch( Exception ) {
				return 0x00;
			}
		}
		private Bitmap binaryToBitmap(byte[] binary) {
			if( binary.Length == 0 ) {
				return null;
			} else {
				return (Bitmap)new ImageConverter().ConvertFrom( binary );
			}
		}
	}
}
