using System.Drawing;



namespace LordOfRanger.Setting {
	/// <summary>
	/// このクラスのインスタンス1つがユーザーがメインウィンドウのDataGridViewで設定したファイルの1行分にあたる。
	/// 通常攻撃の連打などに使う。
	/// キーボードでpushキーを押下すると、sendキーが送信される。
	/// パラメータの詳細はDataAb参照
	/// 細かい実装についてはJobクラスを参照
	/// </summary>
	internal class Barrage :DataAb {

		internal byte push;
		internal byte send;

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

	}
}
