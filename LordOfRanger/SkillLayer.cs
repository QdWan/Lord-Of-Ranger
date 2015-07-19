namespace LordOfRanger {
	/// <summary>
	/// スキルアイコン表示用フォーム
	/// </summary>
	internal partial class SkillLayer : LayeredWindowSurface {
		internal SkillLayer() {
			InitializeComponent();
		}

		/// <summary>
		/// このメソッドを呼ぶことでアイコンが一番手前に表示される
		/// </summary>
		internal void ToTop() {
			TopMost = true;
			TopMost = false;
		}
	}
}
