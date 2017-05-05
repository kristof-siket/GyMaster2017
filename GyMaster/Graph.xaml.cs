namespace GyMaster
{
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
    using Data;

    /// <summary>
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph : Window
    {
        private const double MARGIN = 30;
        private const double STEP = 30;
        private const int ELLIPSESIZE = 10;

        /// <summary>
        /// Viewmodel példány
        /// </summary>
        private ViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph"/> class.
        /// Graph ablak konstruktora
        /// </summary>
        public Graph()
        {
            this.vm = ViewModel.Get();
            this.DataContext = this.vm;
            this.InitializeComponent();
        }

        /// <summary>
        /// Windows loaded esemény
        /// </summary>
        /// <param name="sender">küldő objektum</param>
        /// <param name="e">esemény paraméterek</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double xmin = MARGIN;

            // double xmax = this.canGraph.Width - MARGIN;
            double ymin = MARGIN;
            double ymax = this.canGraph.Height - MARGIN;

            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(new Point(0, ymax), new Point(this.canGraph.Width, ymax)));
            for (double x = xmin + STEP;  x <= this.canGraph.Width; x += STEP)
            {
                xaxis_geom.Children.Add(new LineGeometry(new Point(x, ymax - (MARGIN / 2)), new Point(x, ymax + (MARGIN / 2))));
            }

            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;
            this.canGraph.Children.Add(xaxis_path);

            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xmin, 0), new Point(xmin, this.canGraph.Height)));
            for (double y = STEP; y <= this.canGraph.Height - STEP; y += STEP)
            {
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(ymin - (MARGIN / 2), y),
                    new Point(ymin + (MARGIN / 2), y)));
            }

            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;

            this.canGraph.Children.Add(yaxis_path);
        }

        /// <summary>
        /// Kirajzol "click" eseménykezelője
        /// </summary>
        /// <param name="sender">küldő objektum</param>
        /// <param name="e">esemény paraméterek</param>
        private void Kirajzol_Click(object sender, RoutedEventArgs e)
        {
            List<UIElement> itemstoremove = new List<UIElement>();
            foreach (UIElement ui in this.canGraph.Children)
            {
                if (ui is Polyline)
                {
                    itemstoremove.Add(ui);
                }
            }

            foreach (UIElement ui in itemstoremove)
            {
                this.canGraph.Children.Remove(ui);
            }

            if (this.cmb_Gyakorlat.SelectedItem != null)
            {
                List<int> selectedRes = new List<int>();
                List<int> loggedRes = new List<int>();
                var at1 = from x in this.vm.BL.GetAthleteRepository().GetAll()
                          where x.NAME == this.vm.SelectedAthlete.NAME
                          select x;
                var at2 = from x in this.vm.BL.GetAthleteRepository().GetAll()
                          where x.NAME == this.vm.LoggedInAthlete.NAME
                          select x;

                ATHLETE selected = at1.First();
                ATHLETE logged = at2.First();
                foreach (RESULT res in selected.RESULT)
                {
                    if (res.EXERCISE.NAME == this.cmb_Gyakorlat.SelectedItem.ToString())
                    {
                        selectedRes.Add((int)res.RES_KG);
                    }
                }

                foreach (RESULT res in logged.RESULT)
                {
                    if (res.EXERCISE.NAME == this.cmb_Gyakorlat.SelectedItem.ToString())
                    {
                        loggedRes.Add((int)res.RES_KG);
                    }
                }

                PointCollection points = new PointCollection();
                for (int i = 0; i < selectedRes.Count; i++)
                {
                    Point p = new Point((i * 30) + MARGIN, this.canGraph.Height - MARGIN - selectedRes.ElementAt(i));
                    points.Add(p); // Fentről nézve van az y=0 ezért megkellett fordítani
                    selectedRes.ElementAt(i);
                    Ellipse ge = new Ellipse();
                    ge.SetValue(Canvas.LeftProperty, p.X - (ELLIPSESIZE / 2));
                    ge.SetValue(Canvas.TopProperty, p.Y - (ELLIPSESIZE / 2));
                    ge.Width = ELLIPSESIZE;
                    ge.Height = ELLIPSESIZE;
                    ge.SetValue(Ellipse.FillProperty, Brushes.Red);
                    this.canGraph.Children.Add(ge);
                }

                Polyline polylyine = new Polyline();
                polylyine.StrokeThickness = 1;
                polylyine.Stroke = Brushes.Red;
                polylyine.Points = points;
                this.canGraph.Children.Add(polylyine);

                PointCollection points2 = new PointCollection();

                for (int i = 0; i < loggedRes.Count; i++)
                {
                    Point p = new Point((i * 30) + MARGIN, this.canGraph.Height - MARGIN - loggedRes.ElementAt(i));
                    points2.Add(p);
                    loggedRes.ElementAt(i);
                    Ellipse ge = new Ellipse();
                    ge.SetValue(Canvas.LeftProperty, p.X - (ELLIPSESIZE / 2));
                    ge.SetValue(Canvas.TopProperty, p.Y - (ELLIPSESIZE / 2));
                    ge.Width = ELLIPSESIZE;
                    ge.Height = ELLIPSESIZE;
                    ge.SetValue(Ellipse.FillProperty, Brushes.Blue);
                    this.canGraph.Children.Add(ge);
                }

                Polyline polylyine2 = new Polyline();
                polylyine2.StrokeThickness = 1;
                polylyine2.Stroke = Brushes.Blue;
                polylyine2.Points = points2;
                this.canGraph.Children.Add(polylyine2);
            }
            else
            {
                MessageBox.Show("Válaszd ki milyen gyakorlatokat hasonlítsunk össze!");
            }
        }
    }
}
