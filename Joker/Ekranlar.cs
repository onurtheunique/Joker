using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Joker
{
    public class Ekranlar()
    {
        public static void Main(int sure)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Image image = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Joker.Deadpool.jpg"));
            // Her ekran için tam ekran form oluştur
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
            Thread.Sleep(sure+1);
            // Süre dolduktan sonra kapat
            foreach (var form in forms)
            {
                form.Close();
            }
        }
    }

}
