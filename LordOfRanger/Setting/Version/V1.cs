using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
// ReSharper disable JoinDeclarationAndInitializer



namespace LordOfRanger.Setting.Version {
	internal class V1 :IF {
		private readonly Mass _mass;
		private const int VERSION = 1;

		public V1( Mass instance ) {
			this._mass = instance;
		}

		private struct ArdHeader {
			internal int id;
			internal Act.InstanceType instanceType;
			internal int priority;
			internal int skillIconSize;
			internal int disableSkillIconSize;
			internal int pushDataSize;
			internal int sendDataSize;
		}

		public void Load( string filename ) {
			this._mass.Init();
			this._mass.name = filename;

			var fs = new FileStream( Mass.SETTING_PATH + this._mass.name + Mass.EXTENSION, FileMode.Open, FileAccess.Read );
			var array = new byte[fs.Length];

			fs.Read( array, 0, (int)fs.Length );
			fs.Close();

			var offset = 0;
			var version = BitConverter.ToInt32( array, offset );
			offset += 4;

			if( version != VERSION ) {
				return;
			}

			var titleSize = BitConverter.ToInt32( array, offset );
			offset += 4;
			var hotKeySize = BitConverter.ToInt32( array, offset );
			offset += 4;
			var headerSize = BitConverter.ToInt32( array, offset );
			offset += 4;
			this._mass.Sequence = BitConverter.ToInt32( array, offset );
			offset += 4;
			//	var title = Encoding.UTF8.GetString( array, offset, titleSize );
			offset += titleSize;
			this._mass.hotKey = array.Skip( offset ).Take( hotKeySize ).ToArray()[0];
			offset += hotKeySize;
			var headerCount = headerSize / 28;
			var headers = new List<ArdHeader>();
			for( var i = 0; i < headerCount; i++ ) {
				var ardHeader = new ArdHeader();
				ardHeader.id = BitConverter.ToInt32( array, offset );
				offset += 4;
				ardHeader.instanceType = (Act.InstanceType)BitConverter.ToInt32( array, offset );
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

			foreach( var ardHeader in headers ) {
				// ReSharper disable once SwitchStatementMissingSomeCases
				switch( ardHeader.instanceType ) {
					case Act.InstanceType.COMMAND:
						var c = new Command();
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
					case Act.InstanceType.BARRAGE:
						var b = new Barrage();
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
					case Act.InstanceType.TOGGLE:
						var t = new Toggle();
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
					default:
						throw new ArgumentOutOfRangeException();
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
			var version = BitConverter.GetBytes( VERSION );
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
			var sequence = BitConverter.GetBytes( this._mass.Sequence );
			/*
				headerSize 32bit
			*/
			/*
				title variable
			*/
			var title = Encoding.UTF8.GetBytes( this._mass.name );
			/*
				hotKey 8bit
			*/
			var hotKey = this._mass.hotKey;
			/*
				id 32bit
				Type 32bit
				priority 32bit
				skillIconSize 32bit
				disableSkillIconSize 32bit
				pushDataSize 32bit
				sendDataSize 32bit
			*/
			var header = new List<byte>();

			/*
				skillIcon variable
				disableSkillIcon variable
				push 8bit
				(sendList variable || send 8bit)
			*/
			var data = new List<byte>();
			foreach( var da in this._mass.Value ) {
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
				// ReSharper disable once SwitchStatementMissingSomeCases
				switch( da.Type ) {
					case Act.InstanceType.COMMAND:
						//pushDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//push
						data.Add( ( (Command)da ).Push[0] );
						//sendDataSize
						header.AddRange( BitConverter.GetBytes( ( ( (Command)da ).sendList.Length ) ) );
						//sendList
						data.AddRange( ( (Command)da ).sendList );
						break;
					case Act.InstanceType.BARRAGE:
						//pushDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//push
						data.Add( ( (Barrage)da ).Push[0] );
						//sendDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//send
						data.Add( ( (Barrage)da ).send );
						break;
					case Act.InstanceType.TOGGLE:
						//pushDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//push
						data.Add( ( (Toggle)da ).Push[0] );
						//sendDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );
						//send
						data.Add( ( (Toggle)da ).send );
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

			}
			titleSize = BitConverter.GetBytes( title.Length );
			hotKeySize = BitConverter.GetBytes( 1 );
			headerSize = BitConverter.GetBytes( header.Count );

			var settingBinary = new List<byte>();
			settingBinary.AddRange( version );
			settingBinary.AddRange( titleSize );
			settingBinary.AddRange( hotKeySize );
			settingBinary.AddRange( headerSize );
			settingBinary.AddRange( sequence );
			settingBinary.AddRange( title );
			settingBinary.Add( hotKey );
			settingBinary.AddRange( header );
			settingBinary.AddRange( data );
			var fs = new FileStream( Mass.SETTING_PATH + this._mass.name + Mass.EXTENSION, FileMode.Create, FileAccess.Write );
			fs.Write( settingBinary.ToArray(), 0, settingBinary.Count );
			fs.Close();
		}

		public static byte GetHotKey( string filename ) {
			try {
				var fs = new FileStream( Mass.SETTING_PATH + filename + Mass.EXTENSION, FileMode.Open, FileAccess.Read );
				var array = new byte[fs.Length];

				fs.Read( array, 0, (int)fs.Length );
				fs.Close();

				var offset = 0;
				//	var version = BitConverter.ToInt32( array, offset );
				offset += 4;
				var titleSize = BitConverter.ToInt32( array, offset );
				offset += 4;
				var hotKeySize = BitConverter.ToInt32( array, offset );
				offset += 4;
				//	var headerSize = BitConverter.ToInt32( array, offset );
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
		private static Bitmap BinaryToBitmap( IReadOnlyCollection<byte> binary ) {
			if( binary.Count == 0 ) {
				return null;
			}
			return (Bitmap)new ImageConverter().ConvertFrom( binary );
		}

	}
}
