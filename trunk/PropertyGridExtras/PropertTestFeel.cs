using System;
using System.Drawing;
using System.Windows.Forms;
using VisualHint.SmartPropertyGrid;
namespace ClarionEdge.PropertyGridExtras
{
    internal class PropertyTestFeel : PropertyFeel
    {
		private bool _password;
        public PropertyTestFeel(VisualHint.SmartPropertyGrid.PropertyGrid grid, bool password)
            : base(grid)
		{
			this._password = password;
		}
		public override Control ShowControl(Rectangle valueRect, PropertyEnumerator propEnum)
		{
			PropInPlaceTextbox propInPlaceTextbox;
			if (this.mInPlaceCtrl == null)
			{
				if (this.mPropCtrl.propInPlaceTextBoxType == null)
				{
					propInPlaceTextbox = new PropInPlaceTextbox(this._password);
				}
				else
				{
					propInPlaceTextbox = (PropInPlaceTextbox)Activator.CreateInstance(this.mPropCtrl.propInPlaceTextBoxType, new object[]
					{
						this._password
					});
				}
				propInPlaceTextbox.Visible = false;
				if (this._password)
				{
					propInPlaceTextbox.PasswordChar = propEnum.Property.ParentGrid.DrawManager.PasswordChar;
				}
				propInPlaceTextbox.Parent = this.mParentWnd;
				this.mInPlaceCtrl = propInPlaceTextbox;
			}
			else
			{
				propInPlaceTextbox = (PropInPlaceTextbox)this.mInPlaceCtrl;
			}
			base.NotifyInPlaceControlCreated(propEnum);
			propInPlaceTextbox.OwnerPropertyEnumerator = propEnum;
			if (this._password)
			{
				propInPlaceTextbox.Font = new Font("Microsoft Sans Serif", propEnum.Property.ParentGrid.Font.Size);
			}
			else
			{
				propInPlaceTextbox.Font = propEnum.Property.Value.Font;
			}
			this.MoveControl(valueRect, propEnum);
			return base.ShowControl(valueRect, propEnum);
		}
		public override void MoveControl(Rectangle valueRect, PropertyEnumerator propEnum)
		{
			if (this.mInPlaceCtrl == null)
			{
				return;
			}
			Graphics graphics = this.mParentWnd.CreateGraphics();
			Rectangle stringValueRect = propEnum.Property.Value.GetStringValueRect(graphics, valueRect, Point.Empty);
			int globalTextMargin = propEnum.Property.ParentGrid.GlobalTextMargin;
			stringValueRect.X -= ((propEnum.Property.Value.EditboxLeftMargin == -1) ? globalTextMargin : propEnum.Property.Value.EditboxLeftMargin);
			stringValueRect.Width = valueRect.Right - stringValueRect.Left;
			Rectangle rectangle = stringValueRect;
			rectangle.Inflate(0, -1);
			Win32Calls.TEXTMETRIC tEXTMETRIC = default(Win32Calls.TEXTMETRIC);
			Win32Calls.GetTextMetrics(graphics, this.mInPlaceCtrl.Font, ref tEXTMETRIC);
			graphics.Dispose();
			int num = rectangle.Height - tEXTMETRIC.tmHeight;
			rectangle.Y += num / 2;
			rectangle.Height -= num;
			rectangle = this.mPropCtrl.TransformRectIfRTL(rectangle);
			if (this.mInPlaceCtrl.Bounds != rectangle)
			{
				this.mInPlaceCtrl.Bounds = rectangle;
			}
		}
    }
}
