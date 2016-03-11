using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using LordOfRanger.Behavior.Action;

// ReSharper disable JoinDeclarationAndInitializer
// ReSharper disable UseObjectOrCollectionInitializer

namespace LordOfRanger.Behavior {
	internal static class V4 {

		private const int VERSION = 4;

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
						var msList = new List<Mouse.ActionPattern>();
						var tmpOffset = offset;
						while( tmpOffset < offset + ardHeader.sendDataSize ) {

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
							msList.Add(new Mouse.ActionPattern( op,x,y,sleepBetween,sleepAfter ));

						}
						offset = tmpOffset;
						m.mouseData.Value = msList;
						mass.Add( m );
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			return mass;
		}

		internal static byte GetHotKey( string filename ) {
			try {
				var fs = new FileStream( Mass.SETTING_PATH + filename + Mass.EXTENSION, FileMode.Open, FileAccess.Read );
				var array = new byte[fs.Length];

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