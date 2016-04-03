using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using LordOfRanger.Arad;
using LordOfRanger.Behavior.Action;

// ReSharper disable JoinDeclarationAndInitializer
// ReSharper disable UseObjectOrCollectionInitializer

namespace LordOfRanger.Behavior {
	internal static class Current {

		private const int VERSION = 6;

		internal static Mass Load( string filename ) {
			var mass = new Mass();
			mass.Init();
			mass.name = filename;

			var fs = new FileStream( Mass.SETTING_PATH + mass.name + Mass.EXTENSION, FileMode.Open, FileAccess.Read );
			var array = new byte[fs.Length];

			fs.Read( array, 0, (int)fs.Length );
			fs.Close();

			var offset = 0;
			var version = BitConverter.ToInt32( array, offset );
			offset += 4;

			if( version != VERSION ) {
				throw new InvalidDataException();
			}

			var hotKeySize = BitConverter.ToInt32( array, offset );
			offset += 4;
			var headerSize = BitConverter.ToInt32( array, offset );
			offset += 4;
			mass.Sequence = BitConverter.ToInt32( array, offset );
			offset += 4;
			mass.hotKey = array.Skip( offset ).Take( hotKeySize ).ToArray()[0];
			offset += hotKeySize;
			mass.SwitchPosition = BitConverter.ToInt32( array, offset );
			offset += 4;
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
						c.KeyboardCancel = BitConverter.ToBoolean( array, offset );
						offset += 1;
						mass.Add( c );
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
						b.KeyboardCancel = BitConverter.ToBoolean( array, offset );
						offset += 1;
						mass.Add( b );
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
						t.KeyboardCancel = BitConverter.ToBoolean( array, offset );
						offset += 1;
						mass.Add( t );
						break;
					case Act.InstanceType.MOUSE:
						var m = new Action.Mouse();
						m.Id = ardHeader.id;
						m.Priority = ardHeader.priority;
						m.SkillIcon = BinaryToBitmap( array.Skip( offset ).Take( ardHeader.skillIconSize ).ToArray() );
						offset += ardHeader.skillIconSize;
						m.DisableSkillIcon = BinaryToBitmap( array.Skip( offset ).Take( ardHeader.disableSkillIconSize ).ToArray() );
						offset += ardHeader.disableSkillIconSize;
						m.Push = array.Skip( offset ).Take( ardHeader.pushDataSize ).ToArray();
						offset += ardHeader.pushDataSize;
						m.mouseData.SwitchState = (SwitchingStyle)BitConverter.ToInt32( array, offset );
						offset += 4;
						var tmpOffset = offset;
						while( tmpOffset < offset + ardHeader.sendDataSize - 4 ) {

							var op = (Mouse.Operation)BitConverter.ToInt32( array, tmpOffset );
							tmpOffset += 4;
							var x = BitConverter.ToInt32( array, tmpOffset );
							tmpOffset += 4;
							var y = BitConverter.ToInt32( array, tmpOffset );
							tmpOffset += 4;
							var sleepBetween = BitConverter.ToInt32( array, tmpOffset );
							tmpOffset += 4;
							var sleepAfter = BitConverter.ToInt32( array, tmpOffset );
							tmpOffset += 4;
							m.mouseData.Value.Add( new Mouse.ActionPattern( op, x, y, sleepBetween, sleepAfter ) );

						}
						offset = tmpOffset;
						m.KeyboardCancel = BitConverter.ToBoolean( array, offset );
						offset += 1;
						mass.Add( m );
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			return mass;
		}

		public static void Save( Mass mass ) {
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
			var sequence = BitConverter.GetBytes( mass.Sequence );
			/*
				hotKey 8bit
			*/
			var hotKey = mass.hotKey;

			/*
				switchPosition 32bit
			*/
			var switchPosition = BitConverter.GetBytes( mass.SwitchPosition );

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
			var header = new List<byte>();

			/*
				skillIcon variable
				disableSkillIcon variable
				push 8bit
				(sendList variable || send 8bit)
			*/
			var data = new List<byte>();
			foreach( var da in mass.Value ) {
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

				//pushDataSize
				header.AddRange( BitConverter.GetBytes( da.Push.Length ) );

				data.AddRange( skillIcon );
				data.AddRange( disableSkillIcon );

				//push
				data.AddRange( da.Push );
				switch( da.Type ) {
					case Act.InstanceType.COMMAND:

						//sendDataSize
						header.AddRange( BitConverter.GetBytes( ( ( (Command)da ).sendList.Length ) ) );

						//sendList
						data.AddRange( ( (Command)da ).sendList );
						break;
					case Act.InstanceType.BARRAGE:

						//sendDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );

						//send
						data.Add( ( (Barrage)da ).send );
						break;
					case Act.InstanceType.TOGGLE:

						//sendDataSize
						header.AddRange( BitConverter.GetBytes( 1 ) );

						//send
						data.Add( ( (Toggle)da ).send );
						break;
					case Act.InstanceType.MOUSE:

						//sendDataSize
						header.AddRange( BitConverter.GetBytes( ( (Action.Mouse)da ).mouseData.Value.Count * 4 * 5 + 4 ) );

						//switchState
						data.AddRange( BitConverter.GetBytes( (int)( (Action.Mouse)da ).mouseData.SwitchState ) );

						//sendList
						foreach( var sl in ( (Action.Mouse)da ).mouseData.Value ) {
							data.AddRange( BitConverter.GetBytes( (int)sl.op ) );
							data.AddRange( BitConverter.GetBytes( sl.x ) );
							data.AddRange( BitConverter.GetBytes( sl.y ) );
							data.AddRange( BitConverter.GetBytes( sl.sleepBetween ) );
							data.AddRange( BitConverter.GetBytes( sl.sleepAfter ) );
						}
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				data.AddRange( BitConverter.GetBytes( da.KeyboardCancel ) );
			}
			var hotKeySize = BitConverter.GetBytes( 1 );
			headerSize = BitConverter.GetBytes( header.Count );

			var settingBinary = new List<byte>();
			settingBinary.AddRange( version );
			settingBinary.AddRange( hotKeySize );
			settingBinary.AddRange( headerSize );
			settingBinary.AddRange( sequence );
			settingBinary.Add( hotKey );
			settingBinary.AddRange( switchPosition );
			settingBinary.AddRange( header );
			settingBinary.AddRange( data );
			var fs = new FileStream( Mass.SETTING_PATH + mass.name + Mass.EXTENSION, FileMode.Create, FileAccess.Write );
			fs.Write( settingBinary.ToArray(), 0, settingBinary.Count );
			fs.Close();
		}

		private static Bitmap BinaryToBitmap( IReadOnlyCollection<byte> binary ) {
			if( binary.Count == 0 ) {
				return null;
			}
			return (Bitmap)new ImageConverter().ConvertFrom( binary );
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

	}
}