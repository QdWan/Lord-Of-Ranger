using System.Drawing;

namespace LordOfRanger.Setting {
	/// <summary>
	/// このクラスのインスタンス1つがユーザーがメインウィンドウのDataGridViewで設定したファイルの1行分にあたる。
	/// ロードオブレンジャーやバルムンクでの使用を想定
	/// キーボードでpushキーを押下すると、sendListのキーが順に送信されていく。
	/// パラメータの詳細はAct参照
	/// 細かい実装についてはJobクラスを参照
	/// </summary>
	internal class Command :Act {
		
		internal byte[] sendList = new byte[0];
		internal Command() {
			this.type = InstanceType.COMMAND;
		}
	}
}
