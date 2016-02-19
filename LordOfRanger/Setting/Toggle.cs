using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LordOfRanger.Setting {
	/// <summary>
	/// このクラスのインスタンス1つがユーザーがメインウィンドウのDataGridViewで設定したファイルの1行分にあたる。
	/// 主にメカニックのコロナや、バトルメイジのチェイサーのように常に連打し続けるようなもののために作成
	/// sendキーの連打、停止をpushキーで切り替える
	/// パラメータの詳細はDataAb参照
	/// 細かい実装についてはJobクラスを参照
	/// </summary>
	internal class Toggle :DataAb {

		internal byte send = 0x00;

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
				return InstanceType.TOGGLE;
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
		}

	}

	internal class ToggleList {
		private List<Toggle> _value = new List<Toggle>();
		internal IEnumerable<Toggle> Value {
			get {
				return this._value;
			}
		}
		private byte[] _cancelList = { };
		internal IEnumerable<byte> CancelList {
			get {
				return this._cancelList;
			}
		}

		internal void Add( Toggle instance ) {
			if( instance.Type == DataAb.InstanceType.TOGGLE ) {
				this._value.Add( instance );
				CancelListReBuild();
			}
		}
		internal void RemoveAt( int sequence ) {
			for( var j = 0; j < this._value.Count; j++ ) {
				if( this._value[j].Id == sequence ) {
					this._value.RemoveAt( j );
					CancelListReBuild();
					return;
				}
			}
		}
		private void CancelListReBuild() {
			if( !Options.Options.options.keyboardCancelToggle ) {
				this._cancelList = new byte[0];
				return;
			}
			var tmp = new List<byte>();
			foreach( var val in Value ) {
				tmp.AddRange( val.Push );
			}
			this._cancelList = tmp.Distinct().ToArray();
		}
	}
}
