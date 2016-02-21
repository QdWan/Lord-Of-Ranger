﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace LordOfRanger.Setting {
	internal class Mouse : DataAb {
		
		internal LordOfRanger.Mouse.Set[] sendList = new LordOfRanger.Mouse.Set[0];

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
				return InstanceType.MOUSE;
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
		}

		internal override bool KeyboardCancel {
			get;
			set;
		}

	}

	internal class MouseList {
		private List<Mouse> _value = new List<Mouse>();
		internal IEnumerable<Mouse> Value {
			get {
				return this._value;
			}
		}
		private byte[] _cancelList = { };
		internal IEnumerable<byte> CancelList {
			get {
				CancelListReBuild();
				return this._cancelList;
			}
		}

		internal void Add( Mouse instance ) {
			if( instance.Type == DataAb.InstanceType.MOUSE ) {
				this._value.Add( instance );
			}
		}
		internal void RemoveAt( int sequence ) {
			for( var j = 0; j < this._value.Count; j++ ) {
				if( this._value[j].Id == sequence ) {
					this._value.RemoveAt( j );
					return;
				}
			}
		}
		private void CancelListReBuild() {
			var tmp = new List<byte>();
			foreach( var val in Value.Where( x => x.KeyboardCancel ) ) {
				tmp.AddRange( val.Push );
			}
			this._cancelList = tmp.Distinct().ToArray();
		}
	}
}