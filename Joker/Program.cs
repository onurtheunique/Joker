using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Joker
{
    public class FullScreenForm : Form
    {
        public FullScreenForm(Image image)
        {
            BackgroundImage = image;
            BackgroundImageLayout = ImageLayout.Stretch;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
        }


        [STAThread]
        public static void Main()
        {
            int sure = 10000;
            Ekranlar.Main(sure+5);
            Keyboard.Main(sure);
            
            
        }
    }
}
