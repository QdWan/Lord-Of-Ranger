using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LordOfRanger.Setting {
	/// <summary>
	/// このクラスのインスタンス1つがユーザーがメインウィンドウのDataGridViewで設定したファイルの1行分にあたる。
	/// ロードオブレンジャーやバルムンクでの使用を想定
	/// キーボードでpushキーを押下すると、sendListのキーが順に送信されていく。
	/// パラメータの詳細はDataAb参照
	/// 細かい実装についてはJobクラスを参照
	/// </summary>
	internal class Command :DataAb {
		
		internal byte[] sendList = new byte[0];

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
				return InstanceType.COMMAND;
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

	}

	internal class CommandList {
		private List<Command> _value = new List<Command>();
		internal IEnumerable<Command> Value {
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

		internal void Add( Command instance ) {
			if( instance.Type == DataAb.InstanceType.COMMAND ) {
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
			if( !Options.Options.options.keyboardCancelCommand ) {
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
