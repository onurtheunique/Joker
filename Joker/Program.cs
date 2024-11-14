
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Reflection;

public class FullScreenImageForm : Form
{
    public FullScreenImageForm()
    {
        // Form ayarlarý: Tam ekran ve kenarlýk yok
        this.FormBorderStyle = FormBorderStyle.None;
        this.WindowState = FormWindowState.Maximized;
        this.StartPosition = FormStartPosition.Manual;

        // Klavye ve fare giriþlerini devre dýþý býrak
        this.KeyPreview = true;
        this.KeyDown += (s, e) => e.SuppressKeyPress = true;
        //this.MouseClick += (s, e) => e.Handled = true;

        // Resmi yükle ve göster
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

        // Tüm ekranlarda formu göster
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
