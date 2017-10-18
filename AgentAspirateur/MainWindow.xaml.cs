using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Windows.Threading;
using System.Timers;
using System.Collections.Generic;

namespace AgentAspirateur
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thread environmentThread;
        private Thread agentThread;
        static public Environment environment;
        static private Agent.Agent agent;
        private readonly System.Timers.Timer _timer;

        public MainWindow()
        {
            InitializeComponent();
            environment = new Environment();


            ThreadStart environmentThreadRef = new ThreadStart(environment.start);
            Console.WriteLine("In Main: Creating the Environment thread");
            this.environmentThread = new Thread(environmentThreadRef);
            environmentThread.Start();

            agent = new Agent.Agent(environment);
            ThreadStart agentThreadRef = new ThreadStart(agent.Start);
            Console.WriteLine("In Main: Creating the Agent thread");
            this.agentThread = new Thread(agentThreadRef);
            agentThread.Start();

            _timer = new System.Timers.Timer(250); //Updates every 2 sec
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer.Start();


        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {

            this.Dispatcher.Invoke(DispatcherPriority.Normal,
            new TimerDispatcherDelegate(Update));

        }

        private delegate void TimerDispatcherDelegate();

        private void drawRoom(Room r, DrawingGroup dg)
        {
            int x = r.getCoordinate().x * 64;
            int y = r.getCoordinate().y * 64;
            dg.Children.Add(new ImageDrawing(
                new BitmapImage(
                new Uri(@"Ressources\sol_parquet_with_border.png", UriKind.Relative)),
                new Rect(x,y, 64, 64)
                ));

            if (r.getHasDiamond())
            {
                dg.Children.Add(new ImageDrawing(
                new BitmapImage(
                new Uri(@"Ressources\diamond.png", UriKind.Relative)),
                new Rect(x, y, 64, 64)
                ));
            }
            if (r.getHasDust())
            {
                dg.Children.Add(new ImageDrawing(
                new BitmapImage(
                new Uri(@"Ressources\dust.png", UriKind.Relative)),
                new Rect(x, y, 64, 64)
                ));
            }
        }

        private ImageDrawing drawRobot(Position robot)
        {
            ImageDrawing robotImage = new ImageDrawing();
            robotImage.Rect = new Rect(robot.x * 64, robot.y * 64, 64, 64);
            robotImage.ImageSource = new BitmapImage(
                new Uri(@"Ressources\robot.png", UriKind.Relative));
            return robotImage;
        }
        private void Update()
        {
            Room[][] map = environment.getMap();
            DrawingGroup imageDrawings = new DrawingGroup();
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    
                    drawRoom(map[i][j], imageDrawings);
                }
            }
            imageDrawings.Children.Add(drawRobot(environment.robot));

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
