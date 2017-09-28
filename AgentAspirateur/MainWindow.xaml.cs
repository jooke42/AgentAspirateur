using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Windows.Threading;
using System.Timers;

namespace AgentAspirateur
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thread environmentThread;
        private Environment environment;
        private readonly System.Timers.Timer _timer;

        public MainWindow()
        {
            InitializeComponent();
            environment = new Environment();
            ThreadStart environmentThreadRef = new ThreadStart(environment.start);
            Console.WriteLine("In Main: Creating the Environment thread");
            this.environmentThread = new Thread(environmentThreadRef);
            environmentThread.Start();

            _timer = new System.Timers.Timer(2000); //Updates every 2 sec
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer.Start();
            
        
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {

            this.Dispatcher.Invoke(DispatcherPriority.Normal,
            new TimerDispatcherDelegate(Update));
            
        }
        private delegate void TimerDispatcherDelegate();
        
        private void Update()
        {
            int x, y;
            Tile[,] map = environment.getMap();
            DrawingGroup imageDrawings = new DrawingGroup();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    x = i * 64;
                    y = j * 64;
                    ImageDrawing tile = new ImageDrawing();
                    tile.Rect = new Rect(x, y, 64, 64);
                    tile.ImageSource = new BitmapImage(
                        new Uri(@"Ressources\sol_parquet_with_border.png", UriKind.Relative));
                    imageDrawings.Children.Add(tile);

                    tile = new ImageDrawing();
                    tile.Rect = new Rect(x, y, 64, 64);
                    switch (map[i, j])
                    {
                        case Tile.DIRTY_FLOOR:
                            Console.WriteLine("drawing dirty floor at ("+i+";"+j+")");
                            tile.ImageSource = new BitmapImage(
                        new Uri(@"Ressources\dust.png", UriKind.Relative));
                            break;
                        case Tile.JEWELRY_FLOOR:
                            tile.ImageSource = new BitmapImage(
                        new Uri(@"Ressources\diamond.png", UriKind.Relative));
                            break;
                    }
                    
                    imageDrawings.Children.Add(tile);
                    
                }
            }
            //
            // Use a DrawingImage and an Image control to
            // display the drawings.
            //
            DrawingImage drawingImageSource = new DrawingImage(imageDrawings);

            // Freeze the DrawingImage for performance benefits.
            drawingImageSource.Freeze();

            mapImage.BeginInit();
            mapImage.Source = null;
            mapImage.EndInit();
            mapImage.BeginInit();
            mapImage.Stretch = Stretch.None;
            mapImage.Source = drawingImageSource;
            
            mapImage.EndInit();

        }
    }
    
}
