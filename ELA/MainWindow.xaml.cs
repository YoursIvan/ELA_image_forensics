using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;
using LibSVMsharp;
using LibSVMsharp.Helpers;
using System.Windows.Data;
using System.Globalization;

public struct ELA_return
{
    public Image<Bgr, byte> ela_color;
    public Image<Gray, byte> ela_grey;
    public Image<Bgr, byte> ela_mat;
};

namespace ELA
{
    /**********************************************/

    /*界面*/

    /**********************************************/

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeParameter();
        }

        /* 全局变量声明*/
        string[] Location_type = {"Tamper Region Location(Dots)",
                                  "Tamper Region Location(Windows)",
                                  "ELA Overlay Image",
                                  "Test Image" };

        string[] Denoise_method = {"ColorScale ELA Image",
                                   "GrayScale ELA Image",
                                   "ELA Image Denoised by Square Mean" ,
                                   "ELA Image Denoised by Median Filter" ,
                                   "ELA Image Denoised by Gaussian Filter", };
        ELA_return ela_set;
        System.Windows.Point s;
        System.Windows.Point p;
        bool screen_begin = false;
        bool issaved = true;
        public static string curFile = String.Empty;
        public static string Filebuffer = String.Empty;

        public static int quality = 95;
        public static double threshold = 8.0;
        public static int blocksize = 20;
        public static int denmethod = 0;
        public static int loc_type = 0;
        public static bool screenshot = false;
        double vh = 1.5;
        double vl = 0.5;

        /*界面*/
        /**********************************************/

        
        /*初始化*/
        ELA ELA_Evaluation = new ELA();
        private void InitializeParameter()
        {
            Parameter_setting.IsEnabled = false;
            Button_R.IsEnabled = false;
            quality_slider.Value = 94;
            threshold_slider.Minimum = 1;
            threshold_slider.Maximum = 10;
            threshold_slider.SmallChange = 0.1;
            threshold_slider.LargeChange = 0.1;
            threshold_slider.Value = 7.9;
            blocksize_slider.Minimum = 4;
            blocksize_slider.Value = 19;
        }
        private void commonmode_binding()
        {
            //binding quality_slider
            System.Windows.Data.Binding bind1 = new System.Windows.Data.Binding();
            bind1.Source = quality_slider;
            bind1.Path = new PropertyPath("Value");
            //binding combobox denmethod
            System.Windows.Data.Binding bind5 = new System.Windows.Data.Binding();
            bind5.Source = Denoise_comboBox;
            bind5.Path = new PropertyPath("SelectedIndex");
            //binding screenshot
            System.Windows.Data.Binding screenshot_bind = new System.Windows.Data.Binding();
            screenshot_bind.Source = ScreenShot_button;
            screenshot_bind.Path = new PropertyPath("IsChecked");
            MultiBinding mb1 = new MultiBinding() { Mode = BindingMode.OneWay };
            mb1.Bindings.Add(bind1);
            mb1.Bindings.Add(bind5);
            mb1.Bindings.Add(screenshot_bind);
            mb1.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            mb1.Converter = new MultiParameterToImageConverter();
            ///将Binding和MultyBinding关联  
            image_ela.SetBinding(System.Windows.Controls.Image.SourceProperty, mb1);

            System.Windows.Data.Binding bind6 = new System.Windows.Data.Binding();
            bind6.Source = quality_slider;
            bind6.Path = new PropertyPath("Value");

            //binding threshold_slider
            System.Windows.Data.Binding bind2 = new System.Windows.Data.Binding();
            bind2.Source = threshold_slider;
            bind2.Path = new PropertyPath("Value");
            //binding blocksize_slider
            System.Windows.Data.Binding bind3 = new System.Windows.Data.Binding();
            bind3.Source = blocksize_slider;
            bind3.Path = new PropertyPath("Value");
            //binding combobox loctype
            System.Windows.Data.Binding bind4 = new System.Windows.Data.Binding();
            bind4.Source = Location_comboBox;
            bind4.Path = new PropertyPath("SelectedIndex");
            MultiBinding mb2 = new MultiBinding() { Mode = BindingMode.OneWay };
            mb2.Bindings.Add(bind6);
            mb2.Bindings.Add(bind2);
            mb2.Bindings.Add(bind3);
            mb2.Bindings.Add(bind4);
            mb2.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            mb2.Converter = new MultiBindingConverter();
            ///将Binding和MultyBinding关联  
            image_org.SetBinding(System.Windows.Controls.Image.SourceProperty, mb2);
        }
        private void screenshotmode_binding()
        {
            //binding quality_slider
            System.Windows.Data.Binding bind1 = new System.Windows.Data.Binding();
            bind1.Source = quality_slider;
            bind1.Path = new PropertyPath("Value");
            //binding combobox denmethod
            System.Windows.Data.Binding bind5 = new System.Windows.Data.Binding();
            bind5.Source = Denoise_comboBox;
            bind5.Path = new PropertyPath("SelectedIndex");
            //binding screenshot
            System.Windows.Data.Binding screenshot_bind = new System.Windows.Data.Binding();
            screenshot_bind.Source = ScreenShot_button;
            screenshot_bind.Path = new PropertyPath("IsChecked");
            MultiBinding mb1 = new MultiBinding() { Mode = BindingMode.OneWay };
            mb1.Bindings.Add(bind1);
            mb1.Bindings.Add(bind5);
            mb1.Bindings.Add(screenshot_bind);
            mb1.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            mb1.Converter = new MultiParameterToImageConverter();
            ///将Binding和MultyBinding关联  
            image_ela.SetBinding(System.Windows.Controls.Image.SourceProperty, mb1);

            System.Windows.Data.Binding bind6 = new System.Windows.Data.Binding();
            bind6.Source = quality_slider;
            bind6.Path = new PropertyPath("Value");
        }
        private void fopenfile(String filename)
        {
            if (curFile != String.Empty)
            {
               // BinaryReader binReader = new BinaryReader(File.Open(filename, FileMode.Open));
                //FileInfo fileInfo = new FileInfo(filename);
                //byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
                //binReader.Close();

                // Init bitmap
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                Stream ms = new MemoryStream(File.ReadAllBytes(filename));
                bitmap.StreamSource = ms;
                
                bitmap.EndInit();
                image_org.Source = bitmap;
                
                Button_R.IsEnabled = true;
                quality_slider.IsEnabled = false;
                threshold_slider.IsEnabled = false;
                blocksize_slider.IsEnabled = false;
                Parameter_setting.IsEnabled = false;
                this.Title = "Error Level Analysis - " + curFile;//将窗口名称改为现在窗口的路径
                if (Convert.ToBoolean(ScreenShot_button.IsChecked))
                    Box_org.Header = "ScreenShot Mode";
                else
                    Box_org.Header = "Test Image";
                if (Parameter_setting.IsEnabled)
                {
                    Tamper_poss.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    Tamper_poss.Content = "Tamper Possibility:";
                }
                image_ela.Visibility = Visibility.Hidden;
                //silder初始化
                Button_R.IsEnabled = true;
                run_Toolbar.IsEnabled = true;
                run_Toolbar_img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Icon/start256.ico"));
                button_save.IsEnabled = false;
                save_Toolbar.IsEnabled = false;
                save_Toolbar_img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Icon/save_grey.ico"));
            }
        }
        private void savefile()
        {
            if (System.Windows.MessageBox.Show("Do you want to Save Tamper-Location Image?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SaveFileDialog savefile_loc = new SaveFileDialog();
                savefile_loc.Title = "Save Location Image";
                savefile_loc.Filter = "JPEG Files|*.jpg|TIFF Files|*.tif|BMP Files|*.bmp|PNG Files|*.png|All Files|*.*";
                savefile_loc.RestoreDirectory = true;
                savefile_loc.FilterIndex = 1;
                savefile_loc.FileName = "Untitled1.jpg";
                savefile_loc.ShowDialog();
                ela_set.ela_mat.Save(savefile_loc.FileName);
                issaved = true;
            }

            if (System.Windows.MessageBox.Show("Do you want to Save ELA Image?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SaveFileDialog savefile_ela = new SaveFileDialog();
                savefile_ela.Title = "Save ELA Image";
                savefile_ela.Filter = "JPEG Files|*.jpg|TIFF Files|*.tif|BMP Files|*.bmp|PNG Files|*.png|All Files|*.*";
                savefile_ela.RestoreDirectory = true;
                savefile_ela.FilterIndex = 1;
                savefile_ela.FileName = "Untitled2.jpg";
                savefile_ela.ShowDialog();
                if (denmethod == 0)
                    ela_set.ela_color.Save(savefile_ela.FileName);
                else
                    ela_set.ela_grey.Save(savefile_ela.FileName);
            }
        }

        /*按键*/
        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Title = "Open Image File";
            openfile.Filter = "All Files|*.*|JPEG Files|*.jpg|TIFF Files|*.tif|BMP Files|*.bmp|PNG Files|*.png";
            openfile.RestoreDirectory = true;
            openfile.FilterIndex = 1;
            openfile.ShowDialog();
            Filebuffer = curFile = openfile.FileName;
            //System.Windows.MessageBox.Show(" " + curFile, "About ELA", MessageBoxButton.OK, MessageBoxImage.Information);
            fopenfile(openfile.FileName);
        }
        private void Button_R_Click(object sender, RoutedEventArgs e)
        {
            image_ela.Visibility = System.Windows.Visibility.Visible;
            if (Convert.ToBoolean(ScreenShot_button.IsChecked))
            {
                screenshotmode_binding();
                Box_org.Header = "ScreenShot Mode";
                threshold_slider.IsEnabled = false;
                blocksize_slider.IsEnabled = false;
                Location_comboBox.IsEnabled = false;
            }
            else
            {
                image_org.ClearValue(System.Windows.Controls.Image.SourceProperty);
                commonmode_binding();
                quality_slider.Value += 1;
                threshold_slider.Value += 0.1;
                blocksize_slider.Value += 1;
                Box_org.Header = Location_type[loc_type];
                quality_slider.IsEnabled = true;
                threshold_slider.IsEnabled = true;
                blocksize_slider.IsEnabled = true;
            }


            double[] a = ELA_Evaluation.libsvmpredict(curFile, quality);
            //ELA_and_display(quality, threshold, blocksize, loc_type, denmethod);
            if (a[0] > 0)
            {
                if (a[0] >= vh)
                {
                    System.Windows.MessageBox.Show("Error Level Analysis Success!\nTamper Possibility: 0.01%! ", "ELA Done!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Tamper_poss.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    Tamper_poss.Content = "Tamper Possibility: 0.01%";
                }
                else if (a[0] <= vl)
                {
                    System.Windows.MessageBox.Show("Error Level Analysis Success!\nTamper Possibility: 49.99%! ", "ELA Done!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Tamper_poss.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    Tamper_poss.Content = "Tamper Possibility: 49.99%";
                }
                else
                {
                    double ratio = (0.5 - ((a[0] - vl) / (vh - vl) * 0.5)) * 100;
                    System.Windows.MessageBox.Show("Error Level Analysis Success!\nTamper Possibility: " + ratio.ToString("f2") + "%!", "ELA Done!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Tamper_poss.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    Tamper_poss.Content = "Tamper Possibility: " + ratio.ToString("f2") + "%";
                }
            }
            else
            {
                if (a[0] <= -vh)
                {
                    System.Windows.MessageBox.Show("Error Level Analysis Success!\nTamper Possibility: 99.99%! ", "WARNING!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Tamper_poss.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
                    Tamper_poss.Content = "Tamper Possibility: 99.99%";
                }
                else if (a[0] >= -vl)
                {
                    System.Windows.MessageBox.Show("Error Level Analysis Success!\nTamper Possibility: 50.01%! ", "WARNING!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Tamper_poss.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
                    Tamper_poss.Content = "Tamper Possibility: 50.01%";
                }
                else
                {
                    double ratio = (0.5 + ((-vl - a[0]) / (vh - vl)) * 0.5) * 100;
                    System.Windows.MessageBox.Show("Error Level Analysis Success!\nTamper Possibility: " + ratio.ToString("f2") + "%!", "WARNING!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Tamper_poss.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
                    Tamper_poss.Content = "Tamper Possibility: " + ratio.ToString("f2") + "%";
                }
            }
            Box_ela.Header = Denoise_method[denmethod];
            qualityBox.Text = quality.ToString();
            thresholdBox.Text = threshold.ToString();
            blocksizeBox.Text = blocksize.ToString();
            Parameter_setting.IsEnabled = true;
            issaved = false;
            Button_R.IsEnabled = false;
            run_Toolbar.IsEnabled = false;
            run_Toolbar_img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Icon/start256_grey.ico"));
            button_save.IsEnabled = true;
            save_Toolbar.IsEnabled = true;
            save_Toolbar_img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Icon/save.ico"));
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            savefile();
        }
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Programing By Yifan\nError Level Analysis is the analysis of compression artifacts in digital data with lossy compression such as JPEG.\n ", "About ELA", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Do you want to quit ELA?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow subwin = new SettingWindow();
            subwin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            subwin.Owner = this;
            subwin.ShowDialog();
            quality_slider.Value = quality;
            threshold_slider.Value = threshold;
            blocksize_slider.Value = blocksize;
            Location_comboBox.SelectedIndex = loc_type;
            Denoise_comboBox.SelectedIndex = denmethod;
        }
        private void Box_org_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
                e.Effects = System.Windows.DragDropEffects.Link;                            //WinForm中为e.Effect = DragDropEffects.Link
            else e.Effects = System.Windows.DragDropEffects.None;
            string fileName = ((System.Array)e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop)).GetValue(0).ToString();
            Filebuffer = curFile = fileName;
            fopenfile(fileName);                    //WinFrom中为e.Effect = DragDropEffects.None
        }

        /*图像处理*/
        private BitmapSource GetPartImage(string ImgUri, int XCoordinate, int YCoordinate, int Width, int Height)
        {
            return new CroppedBitmap(BitmapFrame.Create(new Uri(ImgUri, UriKind.Relative)), new Int32Rect(XCoordinate, YCoordinate, Width, Height));
        }
        private void DispRGBImage(Image<Bgr, byte> img, System.Windows.Controls.Image display) 
        {
            Bitmap bitmap = img.ToBitmap();
            IntPtr ip = bitmap.GetHbitmap();//从GDI+ Bitmap创建GDI位图对象
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, Int32Rect.Empty,
            System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            display.Source = bitmapSource;
        }

        /*截屏模式*/
        private void image_org_MouseDown(object sender, MouseButtonEventArgs e)
        {
            screen_begin = true;
            s = e.GetPosition(image_org);
        }
        private void image_org_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (screen_begin)
            {
                Image<Bgr, byte> img = new Image<Bgr, byte>(Filebuffer);
                int i;
                double width = img.Width;
                double height = img.Height;
                double xscale = width / image_org.Width;
                double yscale = height / image_org.Height;

                p = e.GetPosition(image_org);
                if (p != s)
                {
                    int sx = Convert.ToInt32(Math.Floor(s.X * xscale));
                    int sy = Convert.ToInt32(Math.Floor(s.Y * yscale));
                    int px = Convert.ToInt32(Math.Floor(p.X * xscale));
                    int py = Convert.ToInt32(Math.Floor(p.Y * yscale));
                    if (px >= sx && py >= sy)
                    {
                        for (i = sx; i < px; i++)
                        {
                            img.Data[sy, i, 0] = 0; img.Data[sy, i, 1] = 0; img.Data[sy, i, 2] = 255;
                            img.Data[py, i, 0] = 0; img.Data[py, i, 1] = 0; img.Data[py, i, 2] = 255;
                        }
                        for (i = sy; i < py; i++)
                        {
                            img.Data[i, sx, 0] = 0; img.Data[i, sx, 1] = 0; img.Data[i, sx, 2] = 255;
                            img.Data[i, px, 0] = 0; img.Data[i, px, 1] = 0; img.Data[i, px, 2] = 255;
                        }
                    }
                    else if (px >= sx && py < sy)
                    {
                        for (i = sx; i < px; i++)
                        {
                            img.Data[sy, i, 0] = 0; img.Data[sy, i, 1] = 0; img.Data[sy, i, 2] = 255;
                            img.Data[py, i, 0] = 0; img.Data[py, i, 1] = 0; img.Data[py, i, 2] = 255;
                        }
                        for (i = py; i < sy; i++)
                        {
                            img.Data[i, sx, 0] = 0; img.Data[i, sx, 1] = 0; img.Data[i, sx, 2] = 255;
                            img.Data[i, px, 0] = 0; img.Data[i, px, 1] = 0; img.Data[i, px, 2] = 255;
                        }
                    }
                    else if (px < sx && py >= sy)
                    {
                        for (i = px; i < sx; i++)
                        {
                            img.Data[sy, i, 0] = 0; img.Data[sy, i, 1] = 0; img.Data[sy, i, 2] = 255;
                            img.Data[py, i, 0] = 0; img.Data[py, i, 1] = 0; img.Data[py, i, 2] = 255;
                        }
                        for (i = sy; i < py; i++)
                        {
                            img.Data[i, sx, 0] = 0; img.Data[i, sx, 1] = 0; img.Data[i, sx, 2] = 255;
                            img.Data[i, px, 0] = 0; img.Data[i, px, 1] = 0; img.Data[i, px, 2] = 255;
                        }
                    }
                    else
                    {
                        for (i = px; i < sx; i++)
                        {
                            img.Data[sy, i, 0] = 0; img.Data[sy, i, 1] = 0; img.Data[sy, i, 2] = 255;
                            img.Data[py, i, 0] = 0; img.Data[py, i, 1] = 0; img.Data[py, i, 2] = 255;
                        }
                        for (i = py; i < sy; i++)
                        {
                            img.Data[i, sx, 0] = 0; img.Data[i, sx, 1] = 0; img.Data[i, sx, 2] = 255;
                            img.Data[i, px, 0] = 0; img.Data[i, px, 1] = 0; img.Data[i, px, 2] = 255;
                        }
                    }
                    DispRGBImage(img, image_org);
                }
            }
        }
        private void image_org_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (screen_begin)
            {
                screen_begin = false;
                Image<Bgr, byte> img = new Image<Bgr, byte>(Filebuffer);
                double width = img.Width;
                double height = img.Height;
                double xscale = width / image_org.Width;
                double yscale = height / image_org.Height;

                p = e.GetPosition(image_org);
                if (s != p)
                {
                    int sx = Convert.ToInt32(Math.Floor(s.X * xscale));
                    int sy = Convert.ToInt32(Math.Floor(s.Y * yscale));
                    int px = Convert.ToInt32(Math.Floor(p.X * xscale));
                    int py = Convert.ToInt32(Math.Floor(p.Y * yscale));
                    int W, H;
                    BitmapSource src;
                    W = Convert.ToInt32(Math.Abs(sx - px));
                    H = Convert.ToInt32(Math.Abs(sy - py));
                    if (px >= sx && py >= sy)
                        src = GetPartImage(Filebuffer, sx, sy, W, H);
                    else if (px >= sx && py < sy)
                        src = GetPartImage(Filebuffer, sx, sy - H, W, H);
                    else if (px < sx && py >= sy)
                        src = GetPartImage(Filebuffer, sx - W, sy, W, H);
                    else
                        src = GetPartImage(Filebuffer, px, py, W, H);

                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)src));
                    encoder.Save(ms);
                    curFile = System.Environment.CurrentDirectory + "/Temp/temp.jpg";
                    Bitmap bp = new Bitmap(ms);
                    bp.Save(curFile);
                    ms.Close();
                    run_Toolbar.IsEnabled = true;
                    run_Toolbar_img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Icon/start256.ico"));
                }
            }
        }
        private void ScreenShotButton_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(ScreenShot_button.IsChecked))
            {
                //System.Windows.MessageBox.Show("Enter the ScreenShot Mode!.", "Mode Changed", MessageBoxButton.OK, MessageBoxImage.Information);
                image_org.ClearValue(System.Windows.Controls.Image.SourceProperty);
                image_org.Cursor = System.Windows.Input.Cursors.Cross;
                image_org.IsEnabled = true;
                Box_org.Header = "ScreenShot Mode";
                image_ela.Visibility = Visibility.Hidden;

                Parameter_setting.IsEnabled = false;
                screenshot_Menu.IsChecked = true;
                Tamper_poss.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(109, 109, 109));
                Tamper_poss.Content = "Tamper Possibility:";
                if (Filebuffer != string.Empty)
                {
                    Image<Bgr, byte> img = new Image<Bgr, byte>(Filebuffer);
                    DispRGBImage(img, image_org);
                }
            }
            else
            {
                // System.Windows.MessageBox.Show("Quit the ScreenShot Mode!.", "Mode Changed", MessageBoxButton.OK, MessageBoxImage.Information);
                image_org.ClearValue(System.Windows.Controls.Image.SourceProperty);
                image_org.Cursor = System.Windows.Input.Cursors.None;
                image_org.IsEnabled = false;
                image_ela.Visibility = Visibility.Hidden;
                threshold_slider.IsEnabled = true;
                blocksize_slider.IsEnabled = true;
                Location_comboBox.IsEnabled = true;
                Parameter_setting.IsEnabled = false;
                screenshot_Menu.IsChecked = false;
                Box_org.Header = "Test Image";
                Box_ela.Header = "ELA Image";
                Tamper_poss.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(109, 109, 109));
                Tamper_poss.Content = "Tamper Possibility:";
                if (Filebuffer != string.Empty)
                {
                    Image<Bgr, byte> img = new Image<Bgr, byte>(Filebuffer);
                    DispRGBImage(img, image_org);
                    Button_R.IsEnabled = true;
                    run_Toolbar.IsEnabled = true;
                    run_Toolbar_img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Icon/start256.ico"));
                }
            }
        }
        private void screenshot_Menu_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(screenshot_Menu.IsChecked))
            {
                System.Windows.MessageBox.Show("Enter the ScreenShot Mode!.", "Mode Changed", MessageBoxButton.OK, MessageBoxImage.Information);
                image_org.Cursor = System.Windows.Input.Cursors.Cross;
                image_org.IsEnabled = true;
                image_ela.Visibility = Visibility.Hidden;
                threshold_slider.IsEnabled = false;
                blocksize_slider.IsEnabled = false;
                Location_comboBox.IsEnabled = false;
                ScreenShot_button.IsChecked = true;
                if (Filebuffer != string.Empty)
                {
                    Image<Bgr, byte> img = new Image<Bgr, byte>(Filebuffer);
                    DispRGBImage(img, image_org);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Quit the ScreenShot Mode!.", "Mode Changed", MessageBoxButton.OK, MessageBoxImage.Information);
                image_org.Cursor = System.Windows.Input.Cursors.None;
                image_org.IsEnabled = false;
                image_ela.Visibility = Visibility.Hidden;
                threshold_slider.IsEnabled = true;
                blocksize_slider.IsEnabled = true;
                Location_comboBox.IsEnabled = true;
                ScreenShot_button.IsChecked = false;
                if (Filebuffer != string.Empty)
                {
                    Image<Bgr, byte> img = new Image<Bgr, byte>(Filebuffer);
                    DispRGBImage(img, image_org);
                    Button_R.IsEnabled = true;
                    run_Toolbar.IsEnabled = true;
                    run_Toolbar_img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Icon/start256.ico"));

                }
            }
        }  
    }

    /**********************************************/

    /*核心算法 ELA类*/

    /**********************************************/

    public class ELA 
    {
        public ELA_return Error_Level_Analysis(int quality, double threshold, int dark_block_size, int func_type, int denoise_method,bool screenshot)
        {
            ELA_return ela_set = new ELA_return();
            if (!screenshot)
                MainWindow.curFile = MainWindow.Filebuffer;
            Image<Bgr, byte> src_img = new Image<Bgr, byte>(MainWindow.curFile);
            Image<Bgr, byte> org_img = src_img.Clone();
            int height = src_img.Rows;
            int width = src_img.Cols;
            int i, j, iRow, iCol;
            int pixelnum = height * width;
            Emgu.CV.Util.VectorOfByte buff = new Emgu.CV.Util.VectorOfByte();

            KeyValuePair<ImwriteFlags, int> keypair = new KeyValuePair<ImwriteFlags, int>(ImwriteFlags.JpegQuality, quality);
            CvInvoke.Imencode(".jpg", src_img, buff, keypair);

            Mat resrc = new Mat();
            CvInvoke.Imdecode(buff, ImreadModes.Color, resrc); //resrc存储重压缩后的图像
            Image<Bgr, byte> resrc_img = resrc.ToImage<Bgr, byte>();

            Image<Bgr, byte> diff_img = src_img.AbsDiff(resrc_img)*20;//计算两图像差值
            /*在待测图像中用粉红色标记对应的ELA图高亮区域*/
            //计算单通道ELA图，并计算像素均值
            int block = 4;
            Image<Gray, byte> ela_grey = new Image<Gray, byte>(width, height);
            for (iRow = 0; iRow < height; iRow++)
            {
                for (iCol = 0; iCol < width; iCol++)
                {
                    ela_grey.Data[iRow, iCol, 0] = Convert.ToByte((diff_img.Data[iRow, iCol, 0] + diff_img.Data[iRow, iCol, 1] + diff_img.Data[iRow, iCol, 2]) / 3);
                }
            }
            //cvtColor(diff,ela_grey,CV_BGR2GRAY);

            //补齐图像块并进行分块操作
            int iHeight_buf = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(height) / block) * block);
            int iWidth_buf = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(width) / block) * block);
            Image<Gray, byte> location;
            if (iHeight_buf == height && iWidth_buf == width)
                location = ela_grey;
            else
            {
                location = new Image<Gray, byte>(iWidth_buf, iHeight_buf);
                for (iRow = 0; iRow < height; iRow++)
                {
                    for (iCol = 0; iCol < width; iCol++)
                    {
                        location.Data[iRow, iCol, 0] = ela_grey.Data[iRow, iCol, 0];
                    }
                }
            }

            Image<Gray, byte> ELA_Denoise = location;
            switch (denoise_method)
            {
                case (0): ela_set.ela_color = diff_img; break;
                case (1): ela_set.ela_grey = ELA_Denoise = location; break;
                case (2): ela_set.ela_grey = ELA_Denoise = average_square(ELA_Denoise, iHeight_buf, iWidth_buf, 4); break;//平方均值滤波
                case (3): ela_set.ela_grey = ELA_Denoise = Median_filter(ela_grey); break;
                case (4): ela_set.ela_grey = ELA_Denoise = Gaussian_filter(ela_grey); break;
            }

            double img_mean_value = img_mean(ELA_Denoise, iHeight_buf, iWidth_buf);
            int dark_num = dark_pixelnum(ELA_Denoise, iHeight_buf, iWidth_buf);
            double quality_dark = Convert.ToDouble(dark_num) / pixelnum;

            /*在待测图像中用粉红色标记对应的ELA图高亮区域*/
            for (iRow = 0; iRow < iHeight_buf / block - 1; iRow++)
            {
                for (iCol = 0; iCol < iWidth_buf / block - 1; iCol++)
                {
                    double block_mean_value = block_mean(ELA_Denoise, iRow, iCol, block);//分块并定位亮度大于threshold*img_mean_value的区域。
                    if (block_mean_value > threshold * img_mean_value)
                    {
                        for (i = 0; i < block; i++)
                        {
                            for (j = 0; j < block; j++)
                            {
                                src_img.Data[iRow * block + i, iCol * block + j, 0] = 156;
                                src_img.Data[iRow * block + i, iCol * block + j, 1] = 0;
                                src_img.Data[iRow * block + i, iCol * block + j, 2] = 255;
                            }
                        }
                    }
                }
            }

            //对于质量较高的图像，进行异常暗区域定位
            if (quality_dark < 0.4)
            {
                for (iRow = 0; iRow < iHeight_buf / dark_block_size - 1; iRow++)
                {
                    for (iCol = 0; iCol < iWidth_buf / dark_block_size - 1; iCol++)
                    {
                        double block_max_value = block_max(ELA_Denoise, iRow, iCol, dark_block_size);
                        if (block_max_value == 0)
                        {
                            for (i = 0; i < dark_block_size; i++)
                            {
                                for (j = 0; j < dark_block_size; j++)
                                {
                                    src_img.Data[iRow * dark_block_size + i, iCol * dark_block_size + j, 0] = 255;
                                    src_img.Data[iRow * dark_block_size + i, iCol * dark_block_size + j, 1] = 0;
                                    src_img.Data[iRow * dark_block_size + i, iCol * dark_block_size + j, 2] = 0;
                                }
                            }
                        }
                    }
                }
            }

            switch (func_type)
            {
                case (0): ela_set.ela_mat = src_img; break;
                case (1): ela_set.ela_mat = window_label(org_img, src_img, MainWindow.blocksize * 2); break;
                case (2): ela_set.ela_mat = overlayELA(org_img, diff_img); break;
                case (3): ela_set.ela_mat = org_img; break;
                default: ela_set.ela_mat = src_img;break;
            }
            return ela_set;
        }
        private Image<Bgr, byte> window_label(Image<Bgr, byte> org, Image<Bgr, byte> src_img, int windowsize)
        {
            //64*64方框定位
            //补齐图像块并进行分块操作
            int iRow, iCol, i, j;
            int iHeight_win = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(src_img.Height) / windowsize) * windowsize);
            int iWidth_win = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(src_img.Width) / windowsize) * windowsize);
            Image<Bgr, byte> frame_location;
            if (iHeight_win == src_img.Height && iWidth_win == src_img.Width)
                frame_location = src_img;
            else
            {
                frame_location = new Image<Bgr, byte>(iWidth_win, iHeight_win);
                for (iRow = 0; iRow < src_img.Height; iRow++)
                {
                    for (iCol = 0; iCol < src_img.Width; iCol++)
                    {
                        frame_location.Data[iRow, iCol, 0] = src_img.Data[iRow, iCol, 0];
                        frame_location.Data[iRow, iCol, 1] = src_img.Data[iRow, iCol, 1];
                        frame_location.Data[iRow, iCol, 2] = src_img.Data[iRow, iCol, 2];
                    }
                }
            }

            for (iRow = 0; iRow < iHeight_win / windowsize - 1; iRow++)
            {
                for (iCol = 0; iCol < iWidth_win / windowsize - 1; iCol++)
                {
                    if (Convert.ToDouble(light_pixelnum(frame_location, iRow, iCol, windowsize)) / (windowsize * windowsize) > 0.5)
                    {
                        for (i = 0; i < windowsize; i++)
                        {
                            for (j = 0; j < windowsize; j++)
                            {
                                org.Data[iRow * windowsize + i, iCol * windowsize, 0] = 156;
                                org.Data[iRow * windowsize + i, iCol * windowsize, 1] = 0;
                                org.Data[iRow * windowsize + i, iCol * windowsize, 2] = 255;

                                org.Data[iRow * windowsize + i, iCol * windowsize + windowsize - 1, 0] = 156;
                                org.Data[iRow * windowsize + i, iCol * windowsize + windowsize - 1, 1] = 0;
                                org.Data[iRow * windowsize + i, iCol * windowsize + windowsize - 1, 2] = 255;

                                org.Data[iRow * windowsize, iCol * windowsize + j, 0] = 156;
                                org.Data[iRow * windowsize, iCol * windowsize + j, 1] = 0;
                                org.Data[iRow * windowsize, iCol * windowsize + j, 2] = 255;

                                org.Data[iRow * windowsize + windowsize - 1, iCol * windowsize + j, 0] = 156;
                                org.Data[iRow * windowsize + windowsize - 1, iCol * windowsize + j, 1] = 0;
                                org.Data[iRow * windowsize + windowsize - 1, iCol * windowsize + j, 2] = 255;
                            }
                        }
                    }
                }
            }

            windowsize *= 2;
            iHeight_win = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(src_img.Height) / windowsize) * windowsize);
            iWidth_win = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(src_img.Width) / windowsize) * windowsize);
            for (iRow = 0; iRow < iHeight_win / windowsize - 1; iRow++)
            {
                for (iCol = 0; iCol < iWidth_win / windowsize - 1; iCol++)
                {
                    if (Convert.ToDouble(blue_pixelnum(frame_location, iRow, iCol, windowsize)) / (windowsize * windowsize) > 0.2)
                    {
                        for (i = 0; i < windowsize; i++)
                        {
                            for (j = 0; j < windowsize; j++)
                            {
                                org.Data[iRow * windowsize + i, iCol * windowsize, 0] = 255;
                                org.Data[iRow * windowsize + i, iCol * windowsize, 1] = 0;
                                org.Data[iRow * windowsize + i, iCol * windowsize, 2] = 0;

                                org.Data[iRow * windowsize + i, iCol * windowsize + windowsize - 1, 0] = 255;
                                org.Data[iRow * windowsize + i, iCol * windowsize + windowsize - 1, 1] = 0;
                                org.Data[iRow * windowsize + i, iCol * windowsize + windowsize - 1, 2] = 0;

                                org.Data[iRow * windowsize, iCol * windowsize + j, 0] = 255;
                                org.Data[iRow * windowsize, iCol * windowsize + j, 1] = 0;
                                org.Data[iRow * windowsize, iCol * windowsize + j, 2] = 0;

                                org.Data[iRow * windowsize + windowsize - 1, iCol * windowsize + j, 0] = 255;
                                org.Data[iRow * windowsize + windowsize - 1, iCol * windowsize + j, 1] = 0;
                                org.Data[iRow * windowsize + windowsize - 1, iCol * windowsize + j, 2] = 0;
                            }
                        }
                    }
                }
            }
            return org;
        }
        /* 平方均值去噪 突出ELA图高亮区域 */
        private Image<Gray, byte> average_square(Image<Gray, byte> buffer, int iRow, int iCol, int block_size)
        {
            Image<Gray, byte> fimg = (buffer) / 255;

            Image<Gray, byte> temp = new Image<Gray, byte>(iCol, iRow);
            int row, col, i, j;
            for (row = 0; row < iRow - block_size + 1; row++)
            {
                for (col = 0; col < iCol - block_size + 1; col++)
                {
                    for (i = 0; i < block_size; i++)
                    {
                        for (j = 0; j < block_size; j++)
                        {
                            temp.Data[row, col, 0] = Convert.ToByte(temp.Data[row, col, 0] + fimg.Data[row + i, col + j, 0] * fimg.Data[row + i, col + j, 0]);
                        }
                    }

                }
            }
            temp = temp * 255 / (block_size * block_size);
            Image<Gray, byte> res = temp;
            return res * 3;
        }
        /* 中值滤波去噪 */
        private Image<Gray, byte> Median_filter(Image<Gray, byte> buffer)
        {
            CvInvoke.MedianBlur(buffer, buffer, 5);
            return buffer;
        }
        /* 高斯滤波去噪 */
        private Image<Gray, byte> Gaussian_filter(Image<Gray, byte> buffer)
        {
            System.Drawing.Size s = new System.Drawing.Size(5, 5);
            CvInvoke.GaussianBlur(buffer, buffer, s, 0);
            return buffer;
        }

        /* 计算块像素最大值 */
        private double block_max(Image<Gray, byte> Buffer0, int iRow, int iCol, int block)
        {
            int max = 0;
            for (int i = 0; i < block; i++)
            {
                for (int j = 0; j < block; j++)
                {
                    if (Buffer0.Data[iRow * block + i, iCol * block + j, 0] > max)
                        max = Buffer0.Data[iRow * block + i, iCol * block + j, 0];
                }
            }
            return max;
        }
        /* 计算块像素均值 */
        private double block_mean(Image<Gray, byte> Buffer0, int iRow, int iCol, int block)
        {
            int sum = 0;
            for (int i = 0; i < block; i++)
            {
                for (int j = 0; j < block; j++)
                {
                    sum = sum + Buffer0.Data[iRow * block + i, iCol * block + j, 0];
                }
            }
            return Convert.ToDouble(sum) / (block * block);
        }
        /* 计算图像像素均值 */
        private double img_mean(Image<Gray, byte> Buffer1, int iRow, int iCol)
        {
            int sum = 0;
            for (int i = 0; i < iRow; i++)
            {
                for (int j = 0; j < iCol; j++)
                {
                    sum += Buffer1.Data[i, j, 0];
                }
            }
            return Convert.ToDouble(sum) / (iRow * iCol);
        }
        /* 计算图像黑色像素数量 */
        private int dark_pixelnum(Image<Gray, byte> Buffer1, int iRow, int iCol)
        {
            int num = 0;
            for (int i = 0; i < iRow; i++)
            {
                for (int j = 0; j < iCol; j++)
                {
                    if (Buffer1.Data[i, j, 0] == 0)
                        num++;
                }
            }
            return num;
        }
        /*计算图像粉红像素数量*/
        private int light_pixelnum(Image<Bgr, byte> Buffer1, int iRow, int iCol, int windowsize)
        {
            int num = 0;
            for (int i = 0; i < windowsize; i++)
            {
                for (int j = 0; j < windowsize; j++)
                {
                    if (Buffer1.Data[iRow * windowsize + i, iCol * windowsize + j, 0] == 156 && Buffer1.Data[iRow * windowsize + i, iCol * windowsize + j, 1] == 0 && Buffer1.Data[iRow * windowsize + i, iCol * windowsize + j, 2] == 255)
                        num++;
                }
            }
            return num;
        }
        /*计算图像蓝色像素数量*/
        private int blue_pixelnum(Image<Bgr, byte> Buffer1, int iRow, int iCol, int windowsize)
        {
            int num = 0;
            for (int i = 0; i < windowsize; i++)
            {
                for (int j = 0; j < windowsize; j++)
                {
                    if (Buffer1.Data[iRow * windowsize + i, iCol * windowsize + j, 0] == 255 && Buffer1.Data[iRow * windowsize + i, iCol * windowsize + j, 1] == 0 && Buffer1.Data[iRow * windowsize + i, iCol * windowsize + j, 2] == 0)
                        num++;
                }
            }
            return num;
        }
        /*计算ELA Overlay 图像*/
        Image<Bgr, byte> overlayELA(Image<Bgr, byte> org, Image<Bgr, byte> ELA)
        {
            return (org + ELA) / 2;
        }

        /*特征提取*/
        public static Matrix<double> feature_extraction(Image<Bgr, byte> src_img, int quality)
        {
            Matrix<double> feature_vector = new Matrix<double>(1, 3);
            int height = src_img.Rows;
            int width = src_img.Cols;
            int pixelnum = height * width;
            Emgu.CV.Util.VectorOfByte buff = new Emgu.CV.Util.VectorOfByte();
            KeyValuePair<ImwriteFlags, int> keypair = new KeyValuePair<ImwriteFlags, int>(ImwriteFlags.JpegQuality, quality);
            CvInvoke.Imencode(".jpg", src_img, buff, keypair);

            Mat resrc = new Mat();
            CvInvoke.Imdecode(buff, ImreadModes.Color, resrc); //resrc存储重压缩后的图像
            Image<Bgr, byte> resrc_img = resrc.ToImage<Bgr, byte>();

            Image<Bgr, byte> diff_img = src_img.AbsDiff(resrc_img);//计算两图像差值
            Image<Ycc, byte> diff_ycbcr = new Image<Ycc, byte>(diff_img.Width, diff_img.Height);
            CvInvoke.CvtColor(diff_img, diff_ycbcr, ColorConversion.Bgr2YCrCb);

            feature_vector[0, 0] = Highest(diff_ycbcr);
            feature_vector[0, 1] = Mean64(diff_ycbcr);
            feature_vector[0, 2] = Blockmax(diff_ycbcr);
            return feature_vector;
        }
        /*特征1*/
        public static double Highest(Image<Ycc, byte> img)
        {
            double H_luminance = 0;
            for (int i = 0; i < img.Rows; i++)
            {
                for (int j = 0; j < img.Cols; j++)
                {
                    if (img.Data[i, j, 0] > H_luminance)
                        H_luminance = img.Data[i, j, 0];
                }
            }
            return H_luminance;
        }
        /*特征2*/
        public static double Mean64(Image<Ycc, byte> img)
        {
            List<double> pixel = new List<double>();
            double mean_64 = 0;
            for (int i = 0; i < img.Rows; i++)
            {
                for (int j = 0; j < img.Cols; j++)
                {
                    pixel.Add(img.Data[i, j, 0]);
                }
            }
            pixel.Sort();
            pixel.Reverse();
            double sum = 0;
            for (int i = 0; i < 64; i++)
                sum += pixel[i];
            mean_64 = sum / 64;
            return mean_64;
        }
        /*特征3*/
        public static double Blockmax(Image<Ycc, byte> img)
        {
            List<double> pixel = new List<double>();
            //补齐图像块并进行分块操作
            int block = 8;
            int iRow, iCol;
            int iHeight_buf = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(img.Height) / block) * block);
            int iWidth_buf = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(img.Width) / block) * block);
            Image<Ycc, byte> src;
            if (iHeight_buf == img.Height && iWidth_buf == img.Width)
                src = img;
            else
            {
                src = new Image<Ycc, byte>(iWidth_buf, iHeight_buf);
                for (iRow = 0; iRow < img.Height; iRow++)
                {
                    for (iCol = 0; iCol < img.Width; iCol++)
                    {
                        src.Data[iRow, iCol, 0] = img.Data[iRow, iCol, 0];
                    }
                }
            }

            for (iRow = 0; iRow < iHeight_buf / block - 1; iRow++)
            {
                for (iCol = 0; iCol < iWidth_buf / block - 1; iCol++)
                {
                    double block_mean_value = Y_block_mean(src, iRow, iCol, block);//分块并定位亮度大于threshold*img_mean_value的区域。
                    pixel.Add(block_mean_value);
                }
            }
            return pixel.Max();
        }
        /*块均值函数*/
        private static double Y_block_mean(Image<Ycc, byte> Buffer0, int iRow, int iCol, int block)
        {
            int sum = 0;
            for (int i = 0; i < block; i++)
            {
                for (int j = 0; j < block; j++)
                {
                    sum = sum + Buffer0.Data[iRow * block + i, iCol * block + j, 0];
                }
            }
            return Convert.ToDouble(sum) / (block * block);
        }
        /*libsvm预测*/

        public double[] libsvmpredict(string file, int quality)
        {
            Image<Bgr, byte> src_img = new Image<Bgr, byte>(file);
            Matrix<double> test = feature_extraction(src_img, quality);

            FileStream fs = new FileStream(@"temp\\temp.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write("1 1:" + test[0, 0] + " 2:" + test[0, 1] + " 3:" + test[0, 2] + "\n");
            sw.Close();
            fs.Close();

            SVMProblem testdata = SVMProblemHelper.Load(@"temp\\temp.txt");

            SVMParameter parameter = new SVMParameter();
            parameter.Type = SVMType.C_SVC;
            parameter.Kernel = SVMKernelType.RBF;
            parameter.C = 1;
            parameter.Gamma = 1;

            SVMModel model = SVM.LoadModel(@"Model\\Model.txt");
            double predict = SVM.Predict(model, testdata.X[0]);
            double[] a;
            double prob = SVM.PredictValues(model, testdata.X[0], out a);
            return a;
        }
     }
    
    //绑定转换器
    public class EnabledToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameters, System.Globalization.CultureInfo culture)
        {
            if (System.Convert.ToBoolean(value))
                return "Visible";
            else
                return "Hidden";
        }

        public object ConvertBack(object value, Type targetType, object parameters, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
         }
    }
    public class MultiBindingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (MainWindow.Filebuffer != string.Empty && MainWindow.curFile != string.Empty)
            {
                //File.Delete(Path.GetFullPath("Temp/temp2.jpg"));
                MainWindow.quality = System.Convert.ToInt32(values[0]);
                MainWindow.threshold = System.Convert.ToDouble(values[1]);
                MainWindow.blocksize = System.Convert.ToInt32(values[2]);
                MainWindow.loc_type = System.Convert.ToInt32(values[3]);
                ELA ELA1 = new ELA();
                ELA_return ela_set = ELA1.Error_Level_Analysis(MainWindow.quality, MainWindow.threshold, MainWindow.blocksize, MainWindow.loc_type, MainWindow.denmethod, MainWindow.screenshot);
                Image<Bgr, byte> res = ela_set.ela_mat;
                Bitmap bitmap = res.ToBitmap();
                IntPtr ip = bitmap.GetHbitmap();//从GDI+ Bitmap创建GDI位图对象
                BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                return bitmapSource;
            }
            else { return ""; }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class MultiParameterToImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameters, CultureInfo culture)
        {
            if (MainWindow.Filebuffer != string.Empty && MainWindow.curFile != string.Empty)
            {
                ELA ELA1 = new ELA();
                MainWindow.quality = System.Convert.ToInt32(values[0]);
                MainWindow.denmethod = System.Convert.ToInt32(values[1]);
                MainWindow.screenshot = System.Convert.ToBoolean(values[2]);
                ELA_return ela_set = ELA1.Error_Level_Analysis(MainWindow.quality, MainWindow.threshold, MainWindow.blocksize,MainWindow.loc_type, MainWindow.denmethod, MainWindow.screenshot);
                Bitmap bitmap;
                if (System.Convert.ToInt32(values[1]) == 0)
                    bitmap = ela_set.ela_color.ToBitmap();
                else
                    bitmap = ela_set.ela_grey.ToBitmap();

                IntPtr ip = bitmap.GetHbitmap();//从GDI+ Bitmap创建GDI位图对象
                BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                return bitmapSource;
            }
            else { return ""; }
        }

        //Bitmap bitmap = res1.ToBitmap();
        //IntPtr ip = bitmap.GetHbitmap();//从GDI+ Bitmap创建GDI位图对象
        //BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, Int32Rect.Empty,System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
        //String p = System.IO.Path.GetFullPath("/Temp/temp.jpg");


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}