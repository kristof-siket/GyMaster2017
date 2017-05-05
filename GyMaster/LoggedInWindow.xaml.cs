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
    using BusinessLogic;
    using Data;

    /// <summary>
    /// Interaction logic for LoggedInWindow.xaml
    /// </summary>
    public partial class LoggedInWindow : Window
    {
        /// <summary>
        /// Viewmodel példány
        /// </summary>
        private ViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggedInWindow"/> class.
        /// LoggedInWindow ablak konstruktora
        /// </summary>
        public LoggedInWindow()
        {
            this.vm = ViewModel.Get();
            this.InitializeComponent();
            this.DataContext = this.vm;
        }

        /// <summary>
        /// Adatmódosítás gomb "click" eseménykezelője
        /// </summary>
        /// <param name="sender">küldő objektum</param>
        /// <param name="e">esemény paraméterek</param>
        private void Adatmodositas_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow(true).ShowDialog();
        }

        /// <summary>
        /// Megbüntet gomb "click" eseménykezelője
        /// </summary>
        /// <param name="sender">küldő objektum</param>
        /// <param name="e">esemény paraméterek</param>
        private void Megbuntet_Click(object sender, RoutedEventArgs e)
        {
            if (this.vm.LoggedInAthlete is TRAINER)
            {
                this.vm.SelectedAthlete.IS_PUNISHED = true;
            }
            else
            {
                MessageBox.Show("Nem büntetheted meg mivel, Te nem vagy edző!");
            }
        }

        /// <summary>
        /// Edzésterv gomb "click" eseménykezelője
        /// </summary>
        /// <param name="sender">küldő objektum</param>
        /// <param name="e">esemény paraméterek</param>
        private void Edzesterv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.vm.LoggedInAthlete is TRAINER)
                {
                    TrainingPlanDescribeWindow tpd = new TrainingPlanDescribeWindow(this.vm.SelectedAthlete);
                    tpd.ShowDialog();
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            catch (AthleteIsPunishedException)
            {
                MessageBox.Show("Ez a felhasználó nem kaphat edzéstervet, mert büntetett!");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Nem készíthetsz edzéstervet, mert nem vagy edző, vagy nincs elég adat a kiválasztott sportolóról!");
            }
            catch (Exception)
            {
                MessageBox.Show("A rendszer nem tud edzéstervet generálni ennek a sportolónak!");
            }
        }

        /// <summary>
        /// Összehasonlít gomb "click" eseménykezelője
        /// </summary>
        /// <param name="sender">küldő objektum</param>
        /// <param name="e">esemény paraméterek</param>
        private void Összehasonlít_Click(object sender, RoutedEventArgs e)
        {
            if (this.vm.SelectedAthlete != null)
            {
                Graph g = new Graph();
                g.Show();
            }
            else
            {
                MessageBox.Show("Válasszál ki valakit hogy össze tudjunk hasonlítani vele!");
            }
        }

        /// <summary>
        /// Hozzáad gomb "click" eseménykezelője
        /// </summary>
        /// <param name="sender">küldő objektum</param>
        /// <param name="e">esemény paraméterek</param>
        private void Hozzaad_Cick(object sender, RoutedEventArgs e)
        {
            if (this.cmb_gyakorlat.SelectedItem == null)
            {
                EXERCISE ex = new EXERCISE { NAME = this.txt_Gyakorlat.Text, ID = 100 + this.vm.BL.GetExerciseRepository().GetAll().Count() + 1 };
                this.vm.BL.GetExerciseRepository().Insert(ex);
                this.vm.BL.GetResultRepository().Insert(new RESULT { ATHLETE = this.vm.LoggedInAthlete, RES_KG = int.Parse(this.txt_teljesitmeny.Text), EXERCISE = ex, EX_ID = ex.ID, ATHLETE_ID = this.vm.LoggedInAthlete.ID });
            }
            else
            {
                string selectedExercise = this.cmb_gyakorlat.SelectedValue.ToString();
                EXERCISE ex = this.vm.BL.GetExerciseRepository().GetExerciseByName(selectedExercise);
                this.vm.BL.GetResultRepository().Insert(new RESULT { ATHLETE = this.vm.LoggedInAthlete, RES_KG = int.Parse(this.txt_teljesitmeny.Text), EXERCISE = ex, EX_ID = ex.ID, ATHLETE_ID = this.vm.LoggedInAthlete.ID });
            }
        }
    }
}