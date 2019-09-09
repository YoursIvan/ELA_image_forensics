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
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
            quality_slider.Value = MainWindow.quality;
            threshold_slider.Minimum = 10;
            threshold_slider.Value = MainWindow.threshold;
            blocksize_slider.Minimum = 4;
            blocksize_slider.Value = MainWindow.blocksize;
            Location_comboBox.SelectedIndex = MainWindow.loc_type;
            Denoise_comboBox.SelectedIndex = MainWindow.denmethod;
        }
        private void button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.quality = Convert.ToInt32(quality_slider.Value);
            MainWindow.threshold = Convert.ToInt32(threshold_slider.Value);
            MainWindow.blocksize = Convert.ToInt32(blocksize_slider.Value);
            MainWindow.loc_type = Location_comboBox.SelectedIndex;
            MainWindow.denmethod = Denoise_comboBox.SelectedIndex;
            this.Close();
        }
        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void quality_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            label_Quality.Content = "Quality: " + Convert.ToInt32(e.NewValue).ToString();
        }
        private void threshold_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            label_Threshold.Content = "Threshold: " + (e.NewValue / 10).ToString("f1");
        }
        private void blocksize_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            label_Blocksize.Content = "Blocksize: " + Convert.ToInt32(e.NewValue).ToString();
        }
        private void Location_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void Denoise_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
