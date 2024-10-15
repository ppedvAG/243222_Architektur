using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloTemplatePattern
{
    internal class MyButton : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {

            pevent.Graphics.FillRectangle(new SolidBrush( Parent.BackColor), 0, 0, Width, Height);
            pevent.Graphics.FillEllipse(Brushes.Red, 0, 0, Width, Height);
            pevent.Graphics.DrawString(Text, Font, Brushes.White, 0, 0);
            //base.OnPaint(pevent);
            //Text=$"MyButton: {count++}";
        }
        int count = 0;
    }
}
