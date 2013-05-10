using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Kinect;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensor sensor;
        private byte[] dataPixels;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region 視窗停止

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            sensor.Stop();
        }

        #endregion 視窗停止

        #region 視窗載入

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sensor = KinectSensor.KinectSensors[0];
            sensor.ColorStream.Enable();
            sensor.ColorFrameReady += VideoFrameReady;
            sensor.Start();
        }

        #endregion 視窗載入

        #region 持續讀入影像

        private void VideoFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            bool data = false;
            using (ColorImageFrame frame = e.OpenColorImageFrame())
            {
                if (frame == null)
                {
                }
                else
                {
                    dataPixels = new byte[frame.PixelDataLength];
                    frame.CopyPixelDataTo(dataPixels);
                    data = true;
                }
            }
            if (data)
            {
                BitmapSource source = BitmapSource.Create(640, 480, 96, 96, PixelFormats.Bgr32, null, dataPixels, 640 * 4);
                videoImage.Source = source;
            }
        }

        #endregion 持續讀入影像
    }
}