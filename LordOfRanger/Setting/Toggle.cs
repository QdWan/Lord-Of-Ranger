using System.Drawing;

namespace LordOfRanger.Setting {
	/// <summary>
	/// このクラスのインスタンス1つがユーザーがメインウィンドウのDataGridViewで設定したファイルの1行分にあたる。
	/// 主にメカニックのコロナや、バトルメイジのチェイサーのように常に連打し続けるようなもののために作成
	/// sendキーの連打、停止をpushキーで切り替える
	/// パラメータの詳細はAct参照
	/// 細かい実装についてはJobクラスを参照
	/// </summary>
	internal class Toggle :Act {

		internal byte send = 0x00;
		internal Toggle() {
			this.type = InstanceType.TOGGLE;
			Enable = false;
		}

	}
}
