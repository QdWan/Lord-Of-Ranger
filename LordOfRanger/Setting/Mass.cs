using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
namespace LordOfRanger.Setting {

	internal class Mass {
		internal static string setting_path = Application.StartupPath + "/setting/";
		internal string Name;
		internal byte HotKey = 0x00;
		internal const string EXTENSION = ".ard";

		/*u-nu-n*/
		internal List<DataAb> _dataList;
		internal List<Command> _commandList;
		internal List<Barrage> _barrageList;
		internal List<Toggle> _toggleList;
		internal int sequence;
		/***komatta***/


		internal DataAb[] DataList {
			get {
				return _dataList.ToArray();
			}
		}

		internal Command[] commandList {
			get {
				return _commandList.ToArray();
			}
		}

		internal Barrage[] barrageList {
			get {
				return _barrageList.ToArray();
			}
		}

		internal Toggle[] toggleList {
			get {
				return _toggleList.ToArray();
			}
		}

		internal Mass(int sequence = 0) {
			Name = "new";
			this.sequence = sequence;
			_dataList = new List<DataAb>();
			_commandList = new List<Command>();
			_barrageList = new List<Barrage>();
			_toggleList = new List<Toggle>();
		}

		#region Data Interface

		internal int Add(DataAb instance) {
			if( instance.id == 0 ) {
				instance.id = ++sequence;
			}
			_dataList.Add( instance );
			return instance.id;
		}

		internal bool RemoveAt(int sequence) {
			for( int i = 0; i < _dataList.Count; i++ ) {
				if( _dataList[i].id == sequence ) {
					_dataList.RemoveAt( i );
					return true;
				}
			}
			return false;
		}

		internal bool UpAt(int sequence) {
			for( int i = 0; i < _dataList.Count; i++ ) {
				if( _dataList[i].id == sequence ) {
					DataAb da = DataList[i];
					_dataList.RemoveAt( i );
					_dataList.Insert( i - 1, da );
					return true;
				}
			}
			return false;
		}

		internal bool DownAt(int sequence) {
			for( int i = 0; i < _dataList.Count; i++ ) {
				if( _dataList[i].id == sequence ) {
					DataAb da = DataList[i];
					_dataList.RemoveAt( i );
					_dataList.Insert( i + 1, da );
					return true;
				}
			}
			return false;
		}

		internal bool changeEnable(int sequence, bool enable) {
			for( int i = 0; i < _dataList.Count; i++ ) {
				if( _dataList[i].id == sequence ) {
					_dataList[i].enable = enable;
					return true;
				}
			}
			return false;
		}

		#endregion

		internal void save() {
			Version.V v = new Version.V( this );
			v.Save();
		}

		internal void load(string filename) {
			Version.V v = new Version.V( this );
			v.Load( filename );
		}

		internal static byte getHotKey(string filename) {
			return Version.V.getHotKey( filename );
		}
	}
}
