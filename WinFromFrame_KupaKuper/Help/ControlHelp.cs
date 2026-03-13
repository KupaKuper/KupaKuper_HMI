using System.Drawing.Drawing2D;

namespace WinFromFrame_KupaKuper.Help
{
    public class ControlHelp
    {
        int radius = 20;            // 圆角半径
        float penWidth = 2f;       // 边框宽度（可以是小数）

        // 为任何控件设置圆角的方法
        public void SetControlRoundedRegion(Control control, int radius ,float penWidth=2f)
        {
            this.radius = radius;
            this.penWidth = penWidth;
            // 创建一个GraphicsPath对象来描绘圆角路径
            using (GraphicsPath path = new GraphicsPath())
            {
                // 添加四个圆弧来构成一个圆角矩形
                // 左上角圆弧
                path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
                // 右上角圆弧
                path.AddArc(new Rectangle(control.Width - radius, 0, radius, radius), 270, 90);
                // 右下角圆弧
                path.AddArc(new Rectangle(control.Width - radius, control.Height - radius, radius, radius), 0, 90);
                // 左下角圆弧
                path.AddArc(new Rectangle(0, control.Height - radius, radius, radius), 90, 90);
                path.CloseFigure(); // 闭合路径

                // 将控件的区域设置为这个圆角路径
                control.Region = new Region(path);
                control.Paint += SetControlRoundedRegion_Paint;
            }
        }

        /// <summary>
        /// 替换组件原边框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetControlRoundedRegion_Paint(object? sender, PaintEventArgs? e)
        {
            var panel = sender as Panel;
            if (panel == null) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 计算收缩后的矩形：向内偏移 penWidth/2
            float offset = penWidth / 2f;
            RectangleF rect = new RectangleF(
                offset,
                offset,
                panel.Width - penWidth,   // 注意不是 panel.Width - 2*offset，因为宽度要减去整个笔宽
                panel.Height - penWidth
            );

            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                // 基于收缩后的矩形添加圆角弧线
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();

                using (var pen = new Pen(Color.Black, penWidth))
                {
                    // 可以设置笔的对齐方式为 Inset（如果支持），但调整矩形更可靠
                    // pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset; // 可能不精确
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}
