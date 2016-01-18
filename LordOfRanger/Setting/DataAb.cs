using System.Drawing;



namespace LordOfRanger.Setting {
	internal abstract class DataAb {

		internal enum InstanceType {
			COMMAND,
			BARRAGE,
			TOGGLE,
			MOUSE
		}

		/// <summary>
		/// 自動で振られる連番
		/// これによって操作するインスタンスを識別する
		/// </summary>
		internal abstract int Id {
			get;
			set;
		}

		/// <summary>
		/// インスタンスのタイプ
		/// COMMAND,BARRAGE,TOGGLE,MOUSEの4パターンある
		/// </summary>
		internal abstract InstanceType Type {
			get;
		}

		/// <summary>
		/// 優先度
		/// (未実装)
		/// </summary>
		internal abstract int Priority {
			get;
			set;
		}

		/// <summary>
		/// レイヤウィンドウに表示するためのスキルアイコン
		/// </summary>
		internal abstract Bitmap SkillIcon {
			get;
			set;
		}

		/// <summary>
		/// レイヤウィンドウに表示するためのスキルアイコン(無効用)
		/// </summary>
		internal abstract Bitmap DisableSkillIcon {
			get;
			set;
		}

		/// <summary>
		/// このインスタンスの設定が現在有効か無効かのフラグ
		/// </summary>
		internal abstract bool Enable {
			get;
			set;
		}
	}
}
