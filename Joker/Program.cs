
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Reflection;

public class FullScreenImageForm : Form
{
    public FullScreenImageForm()
    {
        // Form ayarlar�: Tam ekran ve kenarl�k yok
        this.FormBorderStyle = FormBorderStyle.None;
        this.WindowState = FormWindowState.Maximized;
        this.StartPosition = FormStartPosition.Manual;

        // Klavye ve fare giri�lerini devre d��� b�rak
        this.KeyPreview = true;
        this.KeyDown += (s, e) => e.SuppressKeyPress = true;
        //this.MouseClick += (s, e) => e.Handled = true;

        // Resmi y�kle ve g�ster
        PictureBox pictureBox = new PictureBox();
        Image image = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Joker.Deadpool.jpg"));
        pictureBox.Image = image;
        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox.Dock = DockStyle.Fill;
        this.Controls.Add(pictureBox);

        // 15 saniye sonra formu kapat
        this.Shown += async (s, e) => {
            await Task.Delay(15000);
            this.Close();
        };
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // T�m ekranlarda formu g�ster
        foreach (Screen screen in Screen.AllScreens)
        {
            FullScreenImageForm form = new FullScreenImageForm
            {
                Location = screen.Bounds.Location
            };
            form.Show();
        }

        Application.Run();
    }
}
