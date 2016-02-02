using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;



namespace LordOfRanger.Setting.Version {
	internal class V2 :IF {
		private Mass _mass;
		private const int VERSION = 2;

		public V2( Mass instance ) {
			this._mass = instance;
		}

		struct ArdHeader {
			internal int id;
			internal DataAb.InstanceType instanceType;
			internal int priority;
			internal int skillIconSize;
			internal int disableSkillIconSize;
			internal int pushDataSize;
			internal int sendDataSize;
		}

		public void Load( string filename ) {
			this._mass.Init();
			this._mass.name = filename;

			FileStream fs = new FileStream( Mass.SETTING_PATH + this._mass.name + Mass.EXTENSION, FileMode.Open, FileAccess.Read );
			byte[] array = new byte[fs.Length];

			fs.Read( array, 0, (int)fs.Length );
			fs.Close();

			int offset = 0;
			int version = BitConverter.ToInt32( array, offset );
			offset += 4;

			if( version != VERSION ) {
				return;
			}

			int hotKeySize = BitConverter.ToInt32( array, offset );
			offset += 4;
			int headerSize = BitConverter.ToInt32( array, offset );
			offset += 4;
			this._mass.Sequence = BitConverter.ToInt32( array, offset );
			offset += 4;
			this._mass.hotKey = array.Skip( offset ).Take( hotKeySize ).ToArray()[0];
			offset += hotKeySize;
			int headerCount = headerSize / 28;
			List<ArdHeader> headers = new List<ArdHeader>();
			for( int i = 0; i < headerCount; i++ ) {
				ArdHeader ardHeader = new ArdHeader();
				ardHeader.id = BitConverter.ToInt32( array, offset );
				offset += 4;
				ardHeader.instanceType = (DataAb.InstanceType)BitConverter.ToInt32( array, offset );
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
				switch( ardHeader.instanceType ) {
					case DataAb.InstanceType.COMMAND:
						Command c = new Command();
						c.Id = ardHeader.id;
						c.Priority = ardHeader.priority;
						c.SkillIcon = BinaryToBitmap( array.Skip( offset ).Take( ardHeader.skillIconSize ).ToArray() );
						offset += ardHeader.skillIconSize;
						c.DisableSkillIcon = BinaryToBitmap( array.Skip( offset ).Take( ardHeader.disableSkillIconSize ).ToArray() );
						offset += ardHeader.disableSkillIconSize;
						c.Push = array.Skip( offset ).Take( ardHeader.pushDataSize ).ToArray();
						offset += ardHeader.pushDataSize;
						c.sendList = array.Skip( offset ).Take( ardHeader.sendDataSize ).ToArray();
						offset += ardHeader.sendDataSize;
						this._mass.Add( c );
						break;
					case DataAb.InstanceType.BARRAGE:
						Barrage b = new Barrage();
						b.Id = ardHeader.id;
						b.Priority = ardHeader.priority;
						b.SkillIcon = BinaryToBitmap( array.Skip( offset ).Take( ardHeader.skillIconSize ).ToArray() );
						offset += ardHeader.skillIconSize;
						b.DisableSkillIcon = BinaryToBitmap( array.Skip( offset ).Take( ardHeader.disableSkillIconSize ).ToArray() );
						offset += ardHeader.disableSkillIconSize;
						b.Push = array.Skip( offset ).Take( ardHeader.pushDataSize ).ToArray();
						offset += ardHeader.pushDataSize;
						b.send = array.Skip( offset ).Take( ardHeader.sendDataSize ).ToArray()[0];
						offset += ardHeader.sendDataSize;
						this._mass.Add( b );
						break;
					case DataAb.InstanceType.TOGGLE:
						Toggle t = new Toggle();
						t.Id = ardHeader.id;
						t.Priority = ardHeader.priority;
						t.SkillIcon = BinaryToBitmap( array.Skip( offset ).Take( ardHeader.skillIconSize ).ToArray() );
						offset += ardHeader.skillIconSize;
						t.DisableSkillIcon = BinaryToBitmap( array.Skip( offset ).Take( ardHeader.disableSkillIconSize ).ToArray() );
						offset += ardHeader.disableSkillIconSize;
						t.Push = array.Skip( offset ).Take( ardHeader.pushDataSize ).ToArray();
						offset += ardHeader.pushDataSize;
						t.send = array.Skip( offset ).Take( ardHeader.sendDataSize ).ToArray()[0];
						offset += ardHeader.sendDataSize;
						this._mass.Add( t );
						break;
				}
			}
		}

		public void Save() {
			if( !Directory.Exists( Mass.SETTING_PATH ) ) {
				Directory.CreateDirectory( Mass.SETTING_PATH );
				Thread.Sleep( 300 );
			}
			/*
				version 32bit
				This is setting version, not program version.
			*/
			byte[] version = BitConverter.GetBytes( VERSION );
			/*
				hotKeySize 32bit
				8 bit only.
			*/

			/*
				headerSize 32bit
			*/
			byte[] headerSize;

			/* 
				sequence
			*/
			byte[] sequence = BitConverter.GetBytes( this._mass.Sequence );
			/*
				hotKey 8bit
			*/
			byte hotKey = this._mass.hotKey;
			/*
				id 32bit
				Type 32bit
				priority 32bit
				skillIconSize 32bit
				disableSkillIconSize 32bit
				pushDataSize 32bit
				sendDataSize 32bit
			*/


			/*
				headerSize 32bit
			*/
			List<byte> header = new List<byte>();

			/*
				skillIcon variable
				disableSkillIcon variable
				push 8bit
				(sendList variable || send 8bit)
			*/
			List<byte> data = new List<byte>();
			foreach( DataAb da in this._mass.DataList ) {
				var skillIcon = (byte[])new ImageConverter().ConvertTo( da.SkillIcon, typeof( byte[] ) ) ?? new byte[0];

				var disableSkillIcon = (byte[])new ImageConverter().ConvertTo( da.DisableSkillIcon, typeof( byte[] ) ) ?? new byte[0];
				//id
				header.AddRange( BitConverter.GetBytes( da.Id ) );
				//Type
				header.AddRange( BitConverter.GetBytes( (int)da.Type ) );
				//priority
				header.AddRange( BitConverter.GetBytes( da.Priority ) );
				//skillIconSize
				header.AddRange( BitConverter.GetBytes( skillIcon.Length ) );
				//disableSkillIconSize
				header.AddRange( BitConverter.GetBytes( disableSkillIcon.Length ) );


				data.AddRange( skillIcon );
				data.AddRange( disableSkillIcon );
				switch( da.Type ) {
					case DataAb.InstanceType.COMMAND:
						//pushDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//push
						data.Add( ( (Command)da ).Push[0] );
						//sendDataSize
						header.AddRange( BitConverter.GetBytes( ( ( (Command)da ).sendList.Length ) ) );
						//sendList
						data.AddRange( ( (Command)da ).sendList );
						break;
					case DataAb.InstanceType.BARRAGE:
						//pushDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//push
						data.Add( ( (Barrage)da ).Push[0] );
						//sendDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//send
						data.Add( ( (Barrage)da ).send );
						break;
					case DataAb.InstanceType.TOGGLE:
						//pushDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//push
						data.Add( ( (Toggle)da ).Push[0] );
						//sendDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//send
						data.Add( ( (Toggle)da ).send );
						break;
				}

			}
			var hotKeySize = BitConverter.GetBytes( 1 );
			headerSize = BitConverter.GetBytes( header.Count );

			List<byte> settingBinary = new List<byte>();
			settingBinary.AddRange( version );
			settingBinary.AddRange( hotKeySize );
			settingBinary.AddRange( headerSize );
			settingBinary.AddRange( sequence );
			settingBinary.Add( hotKey );
			settingBinary.AddRange( header );
			settingBinary.AddRange( data );
			FileStream fs = new FileStream( Mass.SETTING_PATH + this._mass.name + Mass.EXTENSION, FileMode.Create, FileAccess.Write );
			fs.Write( settingBinary.ToArray(), 0, settingBinary.Count );
			fs.Close();
		}
		public static byte GetHotKey( string filename ) {
			try {
				FileStream fs = new FileStream( Mass.SETTING_PATH + filename + Mass.EXTENSION, FileMode.Open, FileAccess.Read );
				byte[] array = new byte[fs.Length];

				fs.Read( array, 0, (int)fs.Length );
				fs.Close();

				var offset = 0;
				//	var version = BitConverter.ToInt32( array, offset );
				offset += 4;
				var hotKeySize = BitConverter.ToInt32( array, offset );
				offset += 4;
				//	var headerSize = BitConverter.ToInt32( array, offset );
				offset += 4;
				//	sequence = BitConverter.ToInt32( array, offset );
				offset += 4;
				return array.Skip( offset ).Take( hotKeySize ).ToArray()[0];
			} catch( Exception ) {
				return 0x00;
			}
		}

		private Bitmap BinaryToBitmap( byte[] binary ) {
			if( binary.Length == 0 ) {
				return null;
			}
			return (Bitmap)new ImageConverter().ConvertFrom( binary );
		}

	}
}
