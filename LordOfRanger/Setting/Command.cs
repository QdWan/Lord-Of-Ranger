using System.Drawing;

namespace LordOfRanger.Setting {
	/// <summary>
	/// このクラスのインスタンス1つがユーザーがメインウィンドウのDataGridViewで設定したファイルの1行分にあたる。
	/// ロードオブレンジャーやバルムンクでの使用を想定
	/// キーボードでpushキーを押下すると、sendListのキーが順に送信されていく。
	/// パラメータの詳細はDataAb参照
	/// 細かい実装についてはJobクラスを参照
	/// </summary>
	internal class Command : DataAb {
		private int _id;
		private int _priority;
		private Bitmap _skillIcon;
		private Bitmap _disableSkillIcon;
		internal byte push;
		internal byte[] sendList = new byte[0];

		internal override int id {
			get {
				return _id;
			}
			set {
				_id = value;
			}
		}

		internal override Type type {
			get {
				return Type.COMMAND;
			}
		}

		internal override int priority {
			get {
				return _priority;
			}
			set {
				_priority = value;
			}
		}

		internal override Bitmap skillIcon {
			get {
				return _skillIcon;
			}
			set {
				_skillIcon = value;
			}
		}

		internal override Bitmap disableSkillIcon {
			get {
				return _disableSkillIcon;
			}
			set {
				_disableSkillIcon = value;
			}
		}

		private bool _enable = true;
		internal override bool enable {
			get {
				return _enable;
			}
			set {
				_enable = value;
			}
		}
	}
}
