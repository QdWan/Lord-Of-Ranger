using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LordOfRanger.Setting {
	internal abstract class DataAb {

		internal enum InstanceType {
			COMMAND,
			BARRAGE,
			TOGGLE,
			MOUSE
		}

		internal abstract byte[] Push {
			get;
			set;
		}

		/// <summary>
		/// 自動で振られる連番
		/// これによって操作するインスタンスを識別する
		/// </summary>
		internal abstract int Id {
			get;
			set;
		}

		/// <summary>
		/// インスタンスのタイプ
		/// COMMAND,BARRAGE,TOGGLE,MOUSEの4パターンある
		/// </summary>
		internal abstract InstanceType Type {
			get;
		}

		/// <summary>
		/// 優先度
		/// (未実装)
		/// </summary>
		internal abstract int Priority {
			get;
			set;
		}

		/// <summary>
		/// レイヤウィンドウに表示するためのスキルアイコン
		/// </summary>
		internal abstract Bitmap SkillIcon {
			get;
			set;
		}

		/// <summary>
		/// レイヤウィンドウに表示するためのスキルアイコン(無効用)
		/// </summary>
		internal abstract Bitmap DisableSkillIcon {
			get;
			set;
		}

		/// <summary>
		/// このインスタンスの設定が現在有効か無効かのフラグ
		/// </summary>
		internal abstract bool Enable {
			get;
			set;
		}

		internal abstract bool KeyboardCancel {
			get;
			set;
		}
	}


	internal class DataAbList<TDataAb> where TDataAb : DataAb {
		private List<TDataAb> _value = new List<TDataAb>();
		internal IEnumerable<TDataAb> Value {
			get {
				return this._value;
			}
		}
		private byte[] _cancelList = { };
		internal IEnumerable<byte> CancelList {
			get {
				CancelListReBuild();
				return this._cancelList;
			}
		}

		internal void Add( TDataAb instance ) {
			if( instance.GetType() == typeof(TDataAb) ) {
				this._value.Add( instance );
			}
		}
		internal void RemoveAt( int sequence ) {
			for( var j = 0; j < this._value.Count; j++ ) {
				if( this._value[j].Id == sequence ) {
					this._value.RemoveAt( j );
					return;
				}
			}
		}
		private void CancelListReBuild() {
			var tmp = new List<byte>();
			foreach( var val in Value.Where( x => x.KeyboardCancel ) ) {
				tmp.AddRange( val.Push );
			}
			this._cancelList = tmp.Distinct().ToArray();
		}
	}
}
