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

namespace RenderingWithShapes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum SelectedShape { Circle, Rectangle, Line }
        private SelectedShape _currentShape;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void canvasDrawingArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Shape shapeToRender = null;

            switch (_currentShape)
            {
                case SelectedShape.Circle:
                    shapeToRender = new Ellipse() { Width = 35, Height = 35 };
                    RadialGradientBrush brush = new RadialGradientBrush();
                    brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF77F177"), 0));
                    brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF11E611"), 1));
                    brush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FF5A8E5A"), 0.545));
                    shapeToRender.Fill = brush;
                    break;

                case SelectedShape.Rectangle:
                    shapeToRender = new Rectangle() { Fill = Brushes.Red, Width = 35, Height = 35, RadiusX = 10, RadiusY = 10 };
                    break;

                case SelectedShape.Line:
                    shapeToRender = new Line()
                    {
                        Stroke = Brushes.Blue,
                        StrokeThickness = 10,
                        X1 = 0,
                        Y1 = 0,
                        X2 = 50,
                        Y2 = 50,
                        StrokeStartLineCap = PenLineCap.Triangle,
                        StrokeEndLineCap = PenLineCap.Round
                    };
                    break;
                default:
                    return;
            }

            if (flipCanvas.IsChecked == true)
            {
                RotateTransform rotate = new RotateTransform(-180);
                shapeToRender.RenderTransform = rotate;
            }

            Canvas.SetLeft(shapeToRender, e.GetPosition(canvasDrawingArea).X);
            Canvas.SetTop(shapeToRender, e.GetPosition(canvasDrawingArea).Y);
            canvasDrawingArea.Children.Add(shapeToRender);
        }

        private void circleOption_Click(object sender, RoutedEventArgs e)
        {
            _currentShape = SelectedShape.Circle;
        }

        private void rectOption_Click(object sender, RoutedEventArgs e)
        {
            _currentShape = SelectedShape.Rectangle;
        }

        private void lineOption_Click(object sender, RoutedEventArgs e)
        {
            _currentShape = SelectedShape.Line;
        }

        private void canvasDrawingArea_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition((Canvas)sender);
            HitTestResult result = VisualTreeHelper.HitTest(canvasDrawingArea, pt);
            if (result != null)
            {
                canvasDrawingArea.Children.Remove(result.VisualHit as Shape);
            }
        }

        private void flipCanvas_Click(object sender, RoutedEventArgs e)
        {
            if (flipCanvas.IsChecked == true)
            {
                RotateTransform rotate = new RotateTransform(-180);
                canvasDrawingArea.LayoutTransform = rotate;
            }
            else
            {
                canvasDrawingArea.LayoutTransform = null;
            }
        }
    }
}
