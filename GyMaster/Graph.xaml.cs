using Data;
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

namespace GyMaster
{
    /// <summary>
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph : Window
    {
        /// <summary>
        /// Viewmodel példány
        /// </summary>
        ViewModel VM;
        const double margin = 30;
        const double step = 30;

        /// <summary>
        /// Graph ablak konstruktora
        /// </summary>
        public Graph()
        {
            VM = ViewModel.Get();
            this.DataContext = VM;
            InitializeComponent();           
        }

        /// <summary>
        /// Windows loaded esemény
        /// </summary>
        /// <param name="sender">küldő objektum</param>
        /// <param name="e">esemény paraméterek</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            double xmin = margin;
            double xmax = canGraph.Width - margin;
            double ymin = margin;
            double ymax = canGraph.Height - margin;
            
            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(new Point(0, ymax), new Point(canGraph.Width, ymax)));
            for (double x= xmin+step;  x<= canGraph.Width; x+=step)
            {
                xaxis_geom.Children.Add(new LineGeometry(new Point(x, ymax - margin / 2), new Point(x, ymax + margin / 2)));
            }
            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;
            canGraph.Children.Add(xaxis_path);

            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xmin, 0), new Point(xmin, canGraph.Height)));
            for (double y = step; y <= canGraph.Height - step; y += step)
            {
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(ymin - margin / 2, y),
                    new Point(ymin + margin / 2, y)));
            }

            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;

            canGraph.Children.Add(yaxis_path);
        }

        /// <summary>
        /// Kirajzol "click" eseménykezelője
        /// </summary>
        /// <param name="sender">küldő objektum</param>
        /// <param name="e">esemény paraméterek</param>
        private void Kirajzol_Click(object sender, RoutedEventArgs e)
        {

            List<UIElement> itemstoremove = new List<UIElement>();
            foreach (UIElement ui in canGraph.Children)
            {
                if (ui is Polyline)
                    itemstoremove.Add(ui);
            }
            foreach(UIElement ui in itemstoremove)
            {
                canGraph.Children.Remove(ui);
            }

            if (cmb_selected.SelectedItem != null && cmb_loggedin.SelectedItem != null)
            {
                List<int> selectedRes = new List<int>();
                List<int> loggedRes = new List<int>();
                var at1 = from x in VM.BL.GetAthleteRepository().GetAll()
                          where x.NAME == VM.SelectedAthlete.NAME
                          select x;
                var at2 = from x in VM.BL.GetAthleteRepository().GetAll()
                          where x.NAME == VM.loggedInAthlete.NAME
                          select x;

                ATHLETE selected = at1.First();
                ATHLETE logged = at2.First();
                foreach (RESULT res in selected.RESULT)
                {
                    if (res.EXERCISE.NAME == cmb_selected.SelectedItem.ToString())
                    {
                        selectedRes.Add((int)res.RES_KG);
                    }
                }

                foreach (RESULT res in logged.RESULT)
                {
                    if (res.EXERCISE.NAME == cmb_loggedin.SelectedItem.ToString())
                    {
                        loggedRes.Add((int)res.RES_KG);
                    }
                }

                PointCollection points = new PointCollection();
                for (int i = 0; i < selectedRes.Count + 1; i++)
                {
                    points.Add(new Point(i * 30 + margin, canGraph.Height - margin - selectedRes.ElementAt(i))); //Fentről nézve van az y=0 ezért megkellett fordítani
                    selectedRes.RemoveAt(0);
                }
               
                Polyline polylyine = new Polyline();
                polylyine.StrokeThickness = 1;
                polylyine.Stroke = Brushes.Red;
                polylyine.Points = points;
                canGraph.Children.Add(polylyine);

                PointCollection points2 = new PointCollection();
                for (int i = 0; i < loggedRes.Count; i++)
                {
                    points2.Add(new Point(i * 30+margin, canGraph.Height-margin-loggedRes.ElementAt(i)));                   
                }
                
                Polyline polylyine2 = new Polyline();
                polylyine2.StrokeThickness = 1;
                polylyine2.Stroke = Brushes.Blue;
                polylyine2.Points = points2;
                canGraph.Children.Add(polylyine2);
            }
            else
            {
                MessageBox.Show("Válaszd ki milyen gyakorlatokat hasonlítsunk össze!");
            }
        }
    }
}
