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
using Microsoft.Win32;

namespace TitleGenerator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Public Constructors

        private const int height = 1920;
        private const int width = 1080;
        private bool bw = true;
        private int fontsize = 40;
        private Bitmap image;
        private string text;

        private int textOriginX;
        private int textOriginY;

        public MainWindow()
        {
            InitializeComponent();

            TB_Text.TextWrapping = TextWrapping.Wrap;
            TB_Text.AcceptsReturn = true;

            textOriginX = height / 2;
            textOriginY = width / 2;

            image = CreateFilledRectangle(height, width);

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

                memory.Close();

                return bitmapimage;
            }
        }

        private Bitmap CreateFilledRectangle(int x, int y)
        {
            Bitmap bmp = new Bitmap(x, y);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                System.Drawing.Rectangle ImageSize = new System.Drawing.Rectangle(0, 0, x, y);
                if (bw)
                {
                    graph.FillRectangle(System.Drawing.Brushes.Black, ImageSize);
                }
                else
                {
                    graph.FillRectangle(System.Drawing.Brushes.White, ImageSize);
                }
            }
            return bmp;
        }

        #endregion Private Methods

        private void Button_Down(object sender, RoutedEventArgs e)
        {
            textOriginY += 5;
            GenerateImage();
        }

        private void Button_Generate(object sender, RoutedEventArgs e)
        {
            GenerateImage();

            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.FileName = "ExampleTitle";
            saveDialog.DefaultExt = "jpg";
            saveDialog.Filter = "JPG images (*.jpg)|*.jpg";

            if (saveDialog.ShowDialog() == true)
            {
                var fileName = saveDialog.FileName;
                if (!System.IO.Path.HasExtension(fileName) || System.IO.Path.GetExtension(fileName) != "jpg")
                    fileName = fileName + ".jpg";

                image.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void Button_Invert(object sender, RoutedEventArgs e)
        {
            bw = !bw;
            GenerateImage();
        }

        private void Button_Left(object sender, RoutedEventArgs e)
        {
            textOriginX -= 5;
            GenerateImage();
        }

        private void Button_Right(object sender, RoutedEventArgs e)
        {
            textOriginX += 5;
            GenerateImage();
        }

        private void Button_Up(object sender, RoutedEventArgs e)
        {
            textOriginY -= 5;
            GenerateImage();
        }

        private void GenerateImage()
        {
            image = CreateFilledRectangle(height, width);
            var g = Graphics.FromImage(image);
            if (bw)
            {
                g.DrawString(text, new Font("Tahoma", fontsize), System.Drawing.Brushes.White, new PointF(textOriginX, textOriginY));
            }
            else
            {
                g.DrawString(text, new Font("Tahoma", fontsize), System.Drawing.Brushes.Black, new PointF(textOriginX, textOriginY));
            }
            if (IMG_Preview != null)
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
            GenerateImage();
        }

        private void TB_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            text = TB_Text.Text;
            GenerateImage();
        }
    }
}