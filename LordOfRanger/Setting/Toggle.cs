using System.Drawing;

namespace LordOfRanger.Setting {
	/// <summary>
	/// このクラスのインスタンス1つがユーザーがメインウィンドウのDataGridViewで設定したファイルの1行分にあたる。
	/// 主にメカニックのコロナや、バトルメイジのチェイサーのように常に連打し続けるようなもののために作成
	/// sendキーの連打、停止をpushキーで切り替える
	/// パラメータの詳細はDataAb参照
	/// 細かい実装についてはJobクラスを参照
	/// </summary>
	internal class Toggle : DataAb {
		private int _id;
		private int _priority;
		private Bitmap _skillIcon;
		private Bitmap _disableSkillIcon;
		private bool _enable = false;
		internal byte push;
		internal byte send;

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
				return Type.TOGGLE;
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
