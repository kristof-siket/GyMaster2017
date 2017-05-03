using BusinessLogic;
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
    /// Interaction logic for LoggedInWindow.xaml
    /// </summary>
    public partial class LoggedInWindow : Window
    {
        /// <summary>
        /// Viewmodel példány
        /// </summary>
        ViewModel VM;

        /// <summary>
        /// LoggedInWindow ablak konstruktora
        /// </summary>
        /// <param name="loggedIn">bejelentkezett sportoló</param>
        public LoggedInWindow(ATHLETE loggedIn)
        {
            VM = ViewModel.Get();
            InitializeComponent();
            this.DataContext = VM;
        }

        /// <summary>
        /// Adatmódosítás gomb "click" eseménykezelője
        /// </summary>
        /// <param name="sender">küldő objektum</param>
        /// <param name="e">esemény paraméterek</param>
        private void Adatmodositas_Click(object sender, RoutedEventArgs e)
        {
            (new RegistrationWindow(true)).ShowDialog();
        }


        //TODO már jó de valamiér csak egy legörgetés után látszódik
        /// <summary>
        /// Megbüntet gomb "click" eseménykezelője
        /// </summary>
        /// <param name="sender">küldő objektum</param>
        /// <param name="e">esemény paraméterek</param>
        private void Megbuntet_Click(object sender, RoutedEventArgs e)
        {
            if (VM.loggedInAthlete is TRAINER)
            {
                VM.SelectedAthlete.IS_PUNISHED = true;                        
            }

            else
                MessageBox.Show("Nem büntetheted meg mivel, Te nem vagy edző!");
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
                TrainingPlanDescribeWindow tpd = new TrainingPlanDescribeWindow(VM.SelectedAthlete);
                tpd.ShowDialog();
            }
            catch (AthleteIsPunishedException)
            {
                MessageBox.Show("Ez a felhasználó nem kaphat edzéstervet, mert büntetett!");
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
            if (VM.SelectedAthlete != null)
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
            if(cmb_gyakorlat.SelectedItem==null)
            {
                EXERCISE ex = new EXERCISE{ NAME = txt_Gyakorlat.Text, ID = 100+VM.BL.GetExerciseRepository().GetAll().Count() + 1 };
                VM.BL.GetExerciseRepository().Insert(ex);
                VM.BL.GetResultRepository().Insert(new RESULT { ATHLETE = VM.loggedInAthlete, RES_KG = int.Parse(txt_teljesitmeny.Text), EXERCISE = ex,EX_ID=ex.ID,ATHLETE_ID=VM.loggedInAthlete.ID});
            }
            else
            {
                EXERCISE ex = VM.BL.GetExerciseRepository().GetExerciseByName(cmb_gyakorlat.SelectedItem.ToString());
                VM.BL.GetResultRepository().Insert(new RESULT {  ATHLETE = VM.loggedInAthlete, RES_KG = int.Parse(txt_teljesitmeny.Text), EXERCISE = ex, EX_ID = ex.ID, ATHLETE_ID = VM.loggedInAthlete.ID });
            }
        }
    }
}