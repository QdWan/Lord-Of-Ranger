namespace LordOfRanger.Setting {
	/// <summary>
	/// このクラスのインスタンス1つがユーザーがメインウィンドウのDataGridViewで設定したファイルの1行分にあたる。
	/// 通常攻撃の連打などに使う。
	/// キーボードでpushキーを押下すると、sendキーが送信される。
	/// パラメータの詳細はAct参照
	/// 細かい実装についてはJobクラスを参照
	/// </summary>
	internal class Barrage :Act {

		internal byte send;
		internal Barrage() {
			this.type = InstanceType.BARRAGE;
		}
	}
}
