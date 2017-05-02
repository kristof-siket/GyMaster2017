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
    /// Interaction logic for TrainingPlanDescribeWindow.xaml
    /// </summary>
    public partial class TrainingPlanDescribeWindow : Window
    {
        ViewModel VM;
        public TrainingPlanDescribeWindow(ATHLETE selectedAthlete)
        {
            VM = ViewModel.Get();
            this.DataContext = VM;
            InitializeComponent();
            BuildTrainingPlanGUI(selectedAthlete, this.Content as Grid);
        }

        /// <summary>
        /// A legenerált edzésterv pontosításához szükséges felületet összeállító metódus.
        /// </summary>
        /// <param name="selectedAthlete">A listából kiválasztott sportoló.</param>
        /// <param name="g">A Grid, amelyben elhelyezzük ezt a GUI-t.</param>
        private void BuildTrainingPlanGUI(ATHLETE selectedAthlete, Grid g)
        {
            VM.BL.Edzesek = VM.BL.GenerateTrainingArray(selectedAthlete);

            for (int i = 0; i < 4; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                g.ColumnDefinitions.Add(cd);
            }

            for (int i = 0; i < VM.BL.Edzesek.Length; i++)
            {
                RowDefinition rd = new RowDefinition();
                g.RowDefinitions.Add(rd);
            }

            List<TextBox> tbs = new List<TextBox>();
            for (int i = 0; i < g.RowDefinitions.Count; i++)
            {
                Label szamlalo = new Label();
                Label edzesNeve = new Label() { Content = VM.BL.Edzesek[i].Title };
                Label foGyakorlat = new Label() { Content = VM.BL.Edzesek[i].FoGyakorlat.NAME };
                TextBox tb = new TextBox() { TextWrapping = System.Windows.TextWrapping.NoWrap, IsEnabled = true, Visibility = System.Windows.Visibility.Visible, Width = 150, Height = 75, Text = "Adjon leírást az edzéshez...", AcceptsReturn = true };
                tbs.Add(tb);
                g.Children.Add(szamlalo);
                g.Children.Add(edzesNeve);
                g.Children.Add(foGyakorlat);
                g.Children.Add(tbs[i]);
                szamlalo.Content = (i + 1).ToString();
                Grid.SetColumn(szamlalo, 0);
                Grid.SetRow(szamlalo, i);
                Grid.SetColumn(edzesNeve, 1);
                Grid.SetRow(edzesNeve, i);
                Grid.SetColumn(foGyakorlat, 2);
                Grid.SetRow(foGyakorlat, i);
                Grid.SetColumn(tbs[i], 3);
                Grid.SetRow(tbs[i], i);
            }
            g.ShowGridLines = true;
            g.RowDefinitions.Add(new RowDefinition()); // +1 sor a gombnak
            Button btn_Kesz = new Button() { Content = "Kész!", Visibility = System.Windows.Visibility.Visible, Name = "btn_Kesz", FontSize = 40 };
            Grid.SetRow(btn_Kesz, (g.RowDefinitions.Count == 4) ? 3 : 4); // ha összesen 4 sor van, akkor a 4.be, ha 5, akkor az 5.be (de ugye 0-tól indul)
            Grid.SetColumn(btn_Kesz, 1);
            Grid.SetColumnSpan(btn_Kesz, 2);
            btn_Kesz.Click += (o, a) =>
            {
                for (int i = 0; i < VM.BL.Edzesek.Length; i++)
                {
                    VM.BL.Edzesek[i].Leiras = tbs[i].Text;
                }
                MessageBox.Show("Az edzésterv sikeresen elkészült!");
                this.Close();
            };
            g.Children.Add(btn_Kesz);
        }
    }
}
