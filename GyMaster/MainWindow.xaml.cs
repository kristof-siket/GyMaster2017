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
using Data;
using BusinessLogic;
using System.Diagnostics;

namespace GyMaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Viewmodel példány
        /// </summary>
        ViewModel VM;

        /// <summary>
        /// MainWindow konstruktora
        /// </summary>
        public MainWindow()
        {
            VM = ViewModel.Get();
            this.DataContext = VM;
            InitializeComponent();
        }
       
        /// <summary>
        /// Regisztrálás
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Regisztracio_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow rw = new RegistrationWindow();
            if(rw.ShowDialog()==true)
            {
                if (VM.BL.RegistrationCheck(rw.UjSportolo,VM.BL.GetAthleteRepository()))
                {
                    if (rw.edzoE)
                    {
                        TRAINER newTrainer = new TRAINER
                        {
                            NAME = rw.UjSportolo.NAME,
                            BORN_IN = rw.UjSportolo.BORN_IN,
                            BORN_DATE = rw.UjSportolo.BORN_DATE,
                            PASSWORD = rw.UjSportolo.PASSWORD,
                            WEIGHT = rw.UjSportolo.WEIGHT,
                            HEIGHT = rw.UjSportolo.HEIGHT
                        };
                        newTrainer.IS_PUNISHED = false;
                        newTrainer.MEMBER_FROM = DateTime.Today.Date;                       
                        VM.BL.addNewMember(newTrainer, VM.BL.GetAthleteRepository());
                    }
                    else
                    {
                        rw.UjSportolo.IS_PUNISHED = false;
                        rw.UjSportolo.MEMBER_FROM = DateTime.Today.Date;
                        VM.BL.addNewMember(rw.UjSportolo, VM.BL.GetAthleteRepository());
                    }
                    VM.Athletes = VM.BL.ToObservableCollection(VM.BL.GetAthleteRepository().GetAll());
                }
                else
                {
                    MessageBox.Show("Már egyszer regisztráltál!");
                }
            }
        }

        /// <summary>
        /// Belépés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Belepes_Click(object sender, RoutedEventArgs e)
        {
            if (VM.BL.LoginEllenorzes(VM.BL.GetAthleteRepository(),txtFelhnev.Text, pwbJelszo.Password))
            {
                MessageBox.Show("Sikeres belépés!");
                var res = from x in VM.BL.GetAthleteRepository().GetAll()
                                     where x.NAME == txtFelhnev.Text
                                     select x;
                VM.loggedInAthlete = res.First();
                LoggedInWindow lg = new LoggedInWindow(VM.loggedInAthlete);
                lg.ShowDialog();
            }

            else
                MessageBox.Show("Nincs ilyen tagunk....");        
        }
    }
}
