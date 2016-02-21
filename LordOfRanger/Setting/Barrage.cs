using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LordOfRanger.Setting {
	/// <summary>
	/// このクラスのインスタンス1つがユーザーがメインウィンドウのDataGridViewで設定したファイルの1行分にあたる。
	/// 通常攻撃の連打などに使う。
	/// キーボードでpushキーを押下すると、sendキーが送信される。
	/// パラメータの詳細はDataAb参照
	/// 細かい実装についてはJobクラスを参照
	/// </summary>
	internal class Barrage :DataAb {

		internal byte send;

		internal override byte[] Push {
			get;
			set;
		}

		internal override int Id {
			get;
			set;
		}

		internal override InstanceType Type {
			get {
				return InstanceType.BARRAGE;
			}
		}

		internal override int Priority {
			get;
			set;
		}

		internal override Bitmap SkillIcon {
			get;
			set;
		}

		internal override Bitmap DisableSkillIcon {
			get;
			set;
		}

		internal override bool Enable {
			get;
			set;
		} = true;

		internal override bool KeyboardCancel {
			get;
			set;
		}
	}
	internal class BarrageList {
		private List<Barrage> _value = new List<Barrage>();
		internal IEnumerable<Barrage> Value {
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

		internal void Add( Barrage instance ) {
			if( instance.Type == DataAb.InstanceType.BARRAGE ) {
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
