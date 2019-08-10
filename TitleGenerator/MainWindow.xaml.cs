using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Drawing;
using System.IO;

namespace TitleGenerator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Public Constructors

        private int fontsize;
        private Bitmap image;
        private string text;

        public MainWindow()
        {
            InitializeComponent();
            image = CreateFilledRectangle(1920, 1080);

            IMG_Preview.Source = BitmapToImageSource(image);
        }

        #endregion Public Constructors

        #region Private Methods

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private Bitmap CreateFilledRectangle(int x, int y)
        {
            Bitmap bmp = new Bitmap(x, y);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                System.Drawing.Rectangle ImageSize = new System.Drawing.Rectangle(0, 0, x, y);
                graph.FillRectangle(System.Drawing.Brushes.Black, ImageSize);
            }
            return bmp;
        }

        #endregion Private Methods

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Generate(object sender, RoutedEventArgs e)
        {
            GenerateImage();
        }

        private void GenerateImage()
        {
            image = CreateFilledRectangle(1920, 1080);
            var g = Graphics.FromImage(image);
            g.DrawString(text, new Font("Tahoma", fontsize), System.Drawing.Brushes.White, new PointF(1920 / 2, 1080 / 2));
            IMG_Preview.Source = BitmapToImageSource(image);
        }

        private void TB_FontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            int a = 0;
            try
            {
                a = Int32.Parse(TB_FontSize.Text);
            }
            catch (Exception)
            {
                TB_FontSize.Text = fontsize.ToString();
                return;
            }
            fontsize = a;
        }

        private void TB_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            text = TB_Text.Text;
        }
    }
}