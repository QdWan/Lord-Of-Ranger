 using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LordOfRanger.Setting.Action {
	internal class Act {

		protected InstanceType type;
		internal enum InstanceType {
			COMMAND,
			BARRAGE,
			TOGGLE,
			MOUSE
		}

		internal byte[] Push {
			get;
			set;
		}

		/// <summary>
		/// 自動で振られる連番
		/// これによって操作するインスタンスを識別する
		/// </summary>
		internal int Id {
			get;
			set;
		}

		/// <summary>
		/// インスタンスのタイプ
		/// COMMAND,BARRAGE,TOGGLE,MOUSEの4パターンある
		/// </summary>
		internal InstanceType Type {
			get {
				return this.type;
			}
		}

		/// <summary>
		/// 優先度
		/// (未実装)
		/// </summary>
		internal int Priority {
			get;
			set;
		}

		/// <summary>
		/// レイヤウィンドウに表示するためのスキルアイコン
		/// </summary>
		internal Bitmap SkillIcon {
			get;
			set;
		}

		/// <summary>
		/// レイヤウィンドウに表示するためのスキルアイコン(無効用)
		/// </summary>
		internal Bitmap DisableSkillIcon {
			get;
			set;
		}

		/// <summary>
		/// このインスタンスの設定が現在有効か無効かのフラグ
		/// </summary>
		internal bool Enable {
			get;
			set;
		} = true;

		internal bool KeyboardCancel {
			get;
			set;
		}
	}


	internal class ActList<TAct> where TAct : Act {
		private readonly List<TAct> _value = new List<TAct>();
		internal IEnumerable<TAct> Value {
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

		internal void Add( TAct instance ) {
			if( instance.GetType() == typeof(TAct) ) {
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
