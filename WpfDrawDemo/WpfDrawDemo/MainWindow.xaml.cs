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
        int drawingAreaL = 0;
        int drawingAreaT = 0;
        int drawingAreaR = 300;
        int drawingAreaB = 300;
        Random random = new Random();
        List<Line> lines = new List<Line>();

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
            this.Width = SystemParameters.PrimaryScreenWidth - 200;
            this.Height = SystemParameters.PrimaryScreenHeight - 200;
            drawingAreaR = (int)this.Width;
            drawingAreaB = (int)this.Height;

            //set window position
            this.Top = 0;
            this.Left = 0;

            //init 
            InitShapes();
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

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            //Draw_ClearEveryFrame();
            Draw_ReusingLines();
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
        }

        private void Draw_ReusingLines()
        {
            foreach (var line in lines)
            {
                line.X1 = random.Next(drawingAreaL, drawingAreaR);
                line.X2 = random.Next(drawingAreaL, drawingAreaR);
                line.Y1 = random.Next(drawingAreaT, drawingAreaB);
                line.Y2 = random.Next(drawingAreaT, drawingAreaB);
            }
        }

    }
}
