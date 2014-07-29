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

namespace WpfDrawDemo
{
    public partial class MainWindow : Window
    {
        SolidColorBrush lineBrush = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
        SolidColorBrush circleBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
        SolidColorBrush circleBrush2 = new SolidColorBrush(Color.FromArgb(128, 128, 128, 128));
        SolidColorBrush textBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

        int drawingAreaL = 0;
        int drawingAreaT = 0;
        int drawingAreaR = 300;
        int drawingAreaB = 300;

        int diameterMin = 10;
        int diameterMax = 100;

        Random random = new Random();

        List<Line> lines = new List<Line>();
        List<Ellipse> circles = new List<Ellipse>();
        List<TextBlock> textblocks = new List<TextBlock>();

        public MainWindow()
        {
            InitializeComponent();

            //events
            this.KeyDown += MainWindow_KeyDown;
            CompositionTarget.Rendering += CompositionTarget_Rendering;

            //set the window borderless
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.AllowsTransparency = true;

            //set window size
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
            drawingAreaR = (int)this.Width;
            drawingAreaB = (int)this.Height;

            //set window position
            this.Top = 0;
            this.Left = 0;

            //init 
            InitShapes();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                this.Top -= 50;
            }
            else if (e.Key == Key.Down && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                this.Top += 50;
            }
            else if (e.Key == Key.Left && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                this.Left -= 50;
            }
            else if (e.Key == Key.Right && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                this.Left += 50;
            }
            else if (e.Key == Key.Up)
            {
                this.Top -= 1;
            }
            else if (e.Key == Key.Down)
            {
                this.Top += 1;
            }
            else if (e.Key == Key.Left)
            {
                this.Left -= 1;
            }
            else if (e.Key == Key.Right)
            {
                this.Left += 1;
            }
            else if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }

        private void InitShapes()
        {
            for (int i = 0; i < 1000; i++)
            {
                Line line = new Line();
                line.StrokeThickness = 1d;
                line.Stroke = lineBrush;
                line.X1 = random.Next(drawingAreaL, drawingAreaR);
                line.X2 = random.Next(drawingAreaL, drawingAreaR);
                line.Y1 = random.Next(drawingAreaT, drawingAreaB);
                line.Y2 = random.Next(drawingAreaT, drawingAreaB);

                canvas.Children.Add(line);
                lines.Add(line);
            }

            for (int i = 0; i < 100; i++)
            {
                Ellipse circle = new Ellipse();
                circle.StrokeThickness = 1d;
                circle.Stroke = circleBrush;
                circle.Fill = circleBrush2;
                int diameter = random.Next(diameterMin, diameterMax);
                circle.Width = diameter;
                circle.Height = diameter;

                canvas.Children.Add(circle);
                int top = random.Next(drawingAreaT, drawingAreaB);
                int left = random.Next(drawingAreaL, drawingAreaR);
                Canvas.SetTop(circle, top);
                Canvas.SetLeft(circle, left);
                circles.Add(circle);
            }

            for (int i = 0; i < 100; i++)
            {
                TextBlock textblock = new TextBlock();
                textblock.Text = "sample";
                textblock.FontSize = 24;
                textblock.Foreground = textBrush;

                canvas.Children.Add(textblock);
                int top = random.Next(drawingAreaT, drawingAreaB);
                int left = random.Next(drawingAreaL, drawingAreaR);
                Canvas.SetTop(textblock, top);
                Canvas.SetLeft(textblock, left);
                textblocks.Add(textblock);
            }
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            /**********************************************
            you can switch the two methods below to see the performance difference 
            **********************************************/

            //Draw_ClearEveryFrame();
            Draw_ByCaching();
        }

        private void Draw_ClearEveryFrame()
        {
            canvas.Children.Clear();

            for (int i = 0; i < 1000; i++)
            {
                Line line = new Line();
                line.StrokeThickness = 1d;
                line.Stroke = lineBrush;
                line.X1 = random.Next(drawingAreaL, drawingAreaR);
                line.X2 = random.Next(drawingAreaL, drawingAreaR);
                line.Y1 = random.Next(drawingAreaT, drawingAreaB);
                line.Y2 = random.Next(drawingAreaT, drawingAreaB);

                canvas.Children.Add(line);
            }

            for (int i = 0; i < 100; i++)
            {
                Ellipse circle = new Ellipse();
                circle.StrokeThickness = 1d;
                circle.Stroke = circleBrush;
                circle.Fill = circleBrush2;
                int diameter = random.Next(diameterMin, diameterMax);
                circle.Width = diameter;
                circle.Height = diameter;

                canvas.Children.Add(circle);
                int top = random.Next(drawingAreaT, drawingAreaB);
                int left = random.Next(drawingAreaL, drawingAreaR);
                Canvas.SetTop(circle, top);
                Canvas.SetLeft(circle, left);
                circles.Add(circle);
            }

            for (int i = 0; i < 100; i++)
            {
                TextBlock textblock = new TextBlock();
                textblock.Text = "sample";
                textblock.FontSize = 24;
                textblock.Foreground = textBrush;

                canvas.Children.Add(textblock);
                int top = random.Next(drawingAreaT, drawingAreaB);
                int left = random.Next(drawingAreaL, drawingAreaR);
                Canvas.SetTop(textblock, top);
                Canvas.SetLeft(textblock, left);
                textblocks.Add(textblock);
            }
        }

        private void Draw_ByCaching()
        {
            foreach (var line in lines)
            {
                line.X1 = random.Next(drawingAreaL, drawingAreaR);
                line.X2 = random.Next(drawingAreaL, drawingAreaR);
                line.Y1 = random.Next(drawingAreaT, drawingAreaB);
                line.Y2 = random.Next(drawingAreaT, drawingAreaB);
            }

            foreach (var circle in circles)
            {
                int diameter = random.Next(diameterMin, diameterMax);
                circle.Width = diameter;
                circle.Height = diameter;
                int top = random.Next(drawingAreaT, drawingAreaB);
                int left = random.Next(drawingAreaL, drawingAreaR);
                Canvas.SetTop(circle, top);
                Canvas.SetLeft(circle, left);
            }

            foreach (var textblock in textblocks)
            {
                int top = random.Next(drawingAreaT, drawingAreaB);
                int left = random.Next(drawingAreaL, drawingAreaR);
                Canvas.SetTop(textblock, top);
                Canvas.SetLeft(textblock, left);
            }
        }

    }
}
