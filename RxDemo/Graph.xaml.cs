using System.Windows.Controls;

namespace RxDemo
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph : UserControl
    {
        public static readonly DependencyProperty LinePointsProperty = DependencyProperty.Register("LinePoints", typeof(ImmutableSortedDictionary<double, double>), typeof(Graph), new PropertyMetadata(default(ImmutableSortedDictionary<double, double>), LinePoinstChanged));

        private static void LinePoinstChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            Graph graph = (Graph)dependencyObject;
            graph.Redraw();
        }

        private void Redraw()
        {
            Canvas.Children.Clear();
            if (LinePoints != null && LinePoints.Count > 1)
            {
                var lastPoint = LinePoints.First();
                var minX = LinePoints.Keys.Min();
                var diffX = Math.Max(1, LinePoints.Keys.Max() - minX);
                var minY = LinePoints.Values.Min();
                var diffY = Math.Max(1, LinePoints.Values.Max() - minY);
                foreach (var linePoint in LinePoints)
                {
                    Canvas.Children.Add(
                        new Line()
                            {
                                X1 = (lastPoint.Key - minX) / diffX * ActualWidth, 
                                Y1 = (lastPoint.Value - minY) / diffY * ActualHeight, 
                                X2 = (linePoint.Key - minX) / diffX * ActualWidth, 
                                Y2 = (linePoint.Value - minY) / diffY * ActualHeight,
                                StrokeThickness = 2,
                                Stroke = Brushes.Black
                            });
                    lastPoint = linePoint;
                }
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Redraw();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Redraw();
        }

        public ImmutableSortedDictionary<double, double> LinePoints
        {
            get
            {
                return (ImmutableSortedDictionary<double, double>)GetValue(LinePointsProperty);
            }
            set
            {
                SetValue(LinePointsProperty, value);
            }
        }

        public Graph()
        {
            InitializeComponent();
        }
    }
}
