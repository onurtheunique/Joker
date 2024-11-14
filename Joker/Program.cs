using System;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joker
{
    public class FullScreenForm : Form
    {
        int time = 10000;
         static async void Ekranlar()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Image image = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Joker.Deadpool.jpg"));
            // Her ekran için tam ekran form oluþtur
            var forms = new FullScreenForm[Screen.AllScreens.Length];
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                forms[i] = new FullScreenForm(image)
                {
                    StartPosition = FormStartPosition.Manual,
                    Location = Screen.AllScreens[i].Bounds.Location
                };
                forms[i].Show();
            }
            //Thread.Sleep(5000);
            await Task.Delay(5000);
            // Süre dolduktan sonra kapat
            foreach (var form in forms)
            {
                form.Close();
            }
        }


         static async void Main()
        {
            
            Task Ekran = Ekranlar();           
            Task Inputs = Keyboard.Main(5000);
            Ekran.Start();
            Inputs.Start(5000);
            //Ekranlar.Main(sure+5);
            //Keyboard.Main(sure);

        }
        public FullScreenForm(Image image)
        {
            BackgroundImage = image;
            BackgroundImageLayout = ImageLayout.Stretch;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
        }
       

    }
}
