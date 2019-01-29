﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mesen.GUI.Controls
{
	class MyListView : ListView
	{
		private bool _preventCheck = false;

		public MyListView()
		{
			this.DoubleBuffered = true;
		}

		protected override void OnItemCheck(ItemCheckEventArgs e)
		{
			if(this._preventCheck || Control.ModifierKeys.HasFlag(Keys.Control) || Control.ModifierKeys.HasFlag(Keys.Shift)) {
				e.NewValue = e.CurrentValue;
				this._preventCheck = false;
			} else {
				base.OnItemCheck(e);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left && e.Clicks > 1) {
				this._preventCheck = true;
			} else {
				this._preventCheck = false;
			}
			base.OnMouseDown(e);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			this._preventCheck = false;
			base.OnKeyDown(e);
		}
	}
	
	public class DoubleBufferedListView : ListView
	{
		public DoubleBufferedListView()
		{
			this.DoubleBuffered = true;
		}

		protected override void OnVirtualItemsSelectionRangeChanged(ListViewVirtualItemsSelectionRangeChangedEventArgs e)
		{
			base.OnVirtualItemsSelectionRangeChanged(e);
			base.OnSelectedIndexChanged(e);
		}
	}
}
