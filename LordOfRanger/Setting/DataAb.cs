using System.Drawing;

namespace LordOfRanger.Setting {
	internal abstract class DataAb {

		internal enum Type {
			COMMAND,
			BARRAGE,
			TOGGLE,
		}

		/// <summary>
		/// 自動で振られる連番
		/// これによって操作するインスタンスを識別する
		/// </summary>
		internal abstract int id {
			get;
			set;
		}

		/// <summary>
		/// インスタンスのタイプ
		/// COMMAND,BARRAGE,TOGGLEの3パターンある
		/// </summary>
		internal abstract Type type {
			get;
		}

		/// <summary>
		/// 優先度
		/// (未実装)
		/// </summary>
		internal abstract int priority {
			get;
			set;
		}

		/// <summary>
		/// レイヤウィンドウに表示するためのスキルアイコン
		/// </summary>
		internal abstract Bitmap skillIcon {
			get;
			set;
		}

		/// <summary>
		/// レイヤウィンドウに表示するためのスキルアイコン(無効用)
		/// </summary>
		internal abstract Bitmap disableSkillIcon {
			get;
			set;
		}

		/// <summary>
		/// このインスタンスの設定が現在有効か無効かのフラグ
		/// </summary>
		internal abstract bool enable {
			get;
			set;
		}
	}
}
