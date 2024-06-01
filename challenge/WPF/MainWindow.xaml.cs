using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace _NET8
{
    #region DefaultImplementation
    /*
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private WriteableBitmap _bitmap;
        private double _time;
        private int _frameCount;
        private Stopwatch _stopwatch;

        public MainWindow()
        {
            InitializeComponent();
            _bitmap = new WriteableBitmap(800, 600, 96, 96, PixelFormats.Bgr32, null);
            MyImage.Source = _bitmap;

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16)
            };
            _timer.Tick += TimerTick;
            _timer.Start();

            _stopwatch = Stopwatch.StartNew();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _time += 0.0003 * 1;
            Draw();
            CalculateFPS();
        }

        private void Draw()
        {
            int width = _bitmap.PixelWidth;
            int height = _bitmap.PixelHeight;
            int stride = width * 4; 

            byte[] pixels = new byte[height * stride];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * stride + x * 4;
                    pixels[index + 2] = (byte)Math.Max(0, pixels[index + 2] - 6); 
                    pixels[index + 1] = (byte)Math.Max(0, pixels[index + 1] - 6); 
                    pixels[index] = (byte)Math.Max(0, pixels[index] - 6); 
                }
            }

            Random rand = new Random(1);

            for (int i = 0; i < 100; i++)
            {
                double x = rand.NextDouble();
                double y = rand.NextDouble();
                byte[] newPixel = BitConverter.GetBytes(rand.Next(0x00FFFFFF + 1));

                for (int j = 0; j < 2000; j++)
                {
                    x = Frac(_time + x + Math.Cos(y * 2.2 + x * 0.1));
                    y = Frac(_time * 0.3 + y + Math.Sin(x * 1.5));

                    int px = (int)(x * width);
                    int py = (int)(y * height);

                    if (px >= 0 && px < width && py >= 0 && py < height)
                    {
                        int index = py * stride + px * 4;

                        pixels[index + 2] = (byte)Math.Min(255, pixels[index + 2] + newPixel[2] / 2); 
                        pixels[index + 1] = (byte)Math.Min(255, pixels[index + 1] + newPixel[1] / 2); 
                        pixels[index] = (byte)Math.Min(255, pixels[index] + newPixel[0]); 
                    }
                }
            }

            _bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
        }

        private double Frac(double value)
        {
            return value - Math.Floor(value);
        }

        private void CalculateFPS()
        {
            _frameCount++;
            if (_stopwatch.ElapsedMilliseconds >= 1000)
            {
                int fps = (int)(_frameCount / (_stopwatch.ElapsedMilliseconds / 1000.0));
                FPSCounter.Text = $"FPS: {fps}";
                _frameCount = 0;
                _stopwatch.Restart();
            }
        }
    }
    */
    #endregion

    #region FixTimerImplementation
    /*
    public partial class MainWindow : Window
    {
        private WriteableBitmap _bitmap;
        private double _time;
        private int _frameCount;
        private Stopwatch _stopwatch;

        public MainWindow()
        {
            InitializeComponent();
            _bitmap = new WriteableBitmap(800, 600, 96, 96, PixelFormats.Bgr32, null);
            MyImage.Source = _bitmap;

            CompositionTarget.Rendering += OnRendering;

            _stopwatch = Stopwatch.StartNew();
        }

        private void OnRendering(object sender, EventArgs e)
        {
            _time += 0.0003 * 1;
            Draw();
            CalculateFPS();
        }

        private void Draw()
        {
            int width = _bitmap.PixelWidth;
            int height = _bitmap.PixelHeight;
            int stride = width * 4;

            byte[] pixels = new byte[height * stride];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * stride + x * 4;
                    pixels[index + 2] = (byte)Math.Max(0, pixels[index + 2] - 6); 
                    pixels[index + 1] = (byte)Math.Max(0, pixels[index + 1] - 6); 
                    pixels[index] = (byte)Math.Max(0, pixels[index] - 6); 
                }
            }

            Random rand = new Random(1);

            for (int i = 0; i < 100; i++)
            {
                double x = rand.NextDouble();
                double y = rand.NextDouble();
                byte[] newPixel = BitConverter.GetBytes(rand.Next(0x00FFFFFF + 1));

                for (int j = 0; j < 2000; j++)
                {
                    x = Frac(_time + x + Math.Cos(y * 2.2 + x * 0.1));
                    y = Frac(_time * 0.3 + y + Math.Sin(x * 1.5));

                    int px = (int)(x * width);
                    int py = (int)(y * height);

                    if (px >= 0 && px < width && py >= 0 && py < height)
                    {
                        int index = py * stride + px * 4;

                        pixels[index + 2] = (byte)Math.Min(255, pixels[index + 2] + newPixel[2] / 2); 
                        pixels[index + 1] = (byte)Math.Min(255, pixels[index + 1] + newPixel[1] / 2); 
                        pixels[index] = (byte)Math.Min(255, pixels[index] + newPixel[0]); 
                    }
                }
            }

            _bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
        }

        private double Frac(double value)
        {
            return value - Math.Floor(value);
        }

        private void CalculateFPS()
        {
            _frameCount++;
            if (_stopwatch.ElapsedMilliseconds >= 1000)
            {
                int fps = (int)(_frameCount / (_stopwatch.ElapsedMilliseconds / 1000.0));
                FPSCounter.Text = $"FPS: {fps}";
                _frameCount = 0;
                _stopwatch.Restart();
            }
        }
    }
    */
    #endregion 

    #region ParallelForImplementation

    public partial class MainWindow : Window
    {
        private WriteableBitmap _bitmap;
        private double _time;
        private int _frameCount;
        private Stopwatch _stopwatch;

        public MainWindow()
        {
            InitializeComponent();
            _bitmap = new WriteableBitmap(800, 600, 96, 96, PixelFormats.Bgr32, null);
            MyImage.Source = _bitmap;

            CompositionTarget.Rendering += OnRendering;

            _stopwatch = Stopwatch.StartNew();
        }

        private async void OnRendering(object sender, EventArgs e)
        {
            _time += 0.0003 * 1;
            await DrawAsync();
            CalculateFPS();
        }

        private async Task DrawAsync()
        {
            int width = _bitmap.PixelWidth;
            int height = _bitmap.PixelHeight;
            int stride = width * 4;

            byte[] pixels = new byte[height * stride];

            Parallel.For(0, height, y =>
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * stride + x * 4;
                    pixels[index + 2] = (byte)Math.Max(0, pixels[index + 2] - 6); // R
                    pixels[index + 1] = (byte)Math.Max(0, pixels[index + 1] - 6); // G
                    pixels[index] = (byte)Math.Max(0, pixels[index] - 6); // B
                }
            });

            Random rand = new Random(1);

            await Task.Run(() =>
            {
                Parallel.For(0, 100, i =>
                {
                    double x = rand.NextDouble();
                    double y = rand.NextDouble();
                    byte[] newPixel = BitConverter.GetBytes(rand.Next(0x00FFFFFF + 1));

                    for (int j = 0; j < 2000; j++)
                    {
                        x = Frac(_time + x + Math.Cos(y * 2.2 + x * 0.1));
                        y = Frac(_time * 0.3 + y + Math.Sin(x * 1.5));

                        int px = (int)(x * width);
                        int py = (int)(y * height);

                        if (px >= 0 && px < width && py >= 0 && py < height)
                        {
                            int index = py * stride + px * 4;

                            pixels[index + 2] = (byte)Math.Min(255, pixels[index + 2] + newPixel[2] / 2);
                            pixels[index + 1] = (byte)Math.Min(255, pixels[index + 1] + newPixel[1] / 2);
                            pixels[index] = (byte)Math.Min(255, pixels[index] + newPixel[0]);
                        }
                    }
                });
            });

            _bitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
        }

        private double Frac(double value)
        {
            return value - Math.Floor(value);
        }

        private void CalculateFPS()
        {
            _frameCount++;
            if (_stopwatch.ElapsedMilliseconds >= 1000)
            {
                int fps = (int)(_frameCount / (_stopwatch.ElapsedMilliseconds / 1000.0));
                FPSCounter.Text = $"FPS: {fps}";
                _frameCount = 0;
                _stopwatch.Restart();
            }
        }
    }

    #endregion 
}