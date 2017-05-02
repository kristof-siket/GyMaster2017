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
        ViewModel VM;
        public LoggedInWindow(ATHLETE loggedIn)
        {
            VM = ViewModel.Get();
            InitializeComponent();
            this.DataContext = VM;
        }

        private void Adatmodositas_Click(object sender, RoutedEventArgs e)
        {
            (new RegistrationWindow(true)).ShowDialog();
        }


        //TODO már jó de valamiér csak egy legörgetés után látszódik
        private void Megbuntet_Click(object sender, RoutedEventArgs e)
        {
            if (VM.loggedInAthlete is TRAINER)
            {
                VM.SelectedAthlete.IS_PUNISHED = true;                        
            }

            else
                MessageBox.Show("Nem büntetheted meg mivel, Te nem vagy edző!");
        }

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
                EXERCISE ex = VM.BL.GetExerciseByName(cmb_gyakorlat.SelectedItem.ToString());
                VM.BL.GetResultRepository().Insert(new RESULT { ATHLETE = VM.loggedInAthlete, RES_KG = int.Parse(txt_teljesitmeny.Text), EXERCISE = ex, EX_ID = ex.ID, ATHLETE_ID = VM.loggedInAthlete.ID });
            }
        }
    }
}
