namespace GyMaster
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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
    using BusinessLogic;
    using Data;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Viewmodel példány
        /// </summary>
        private ViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// MainWindow konstruktora
        /// </summary>
        public MainWindow()
        {
            this.vm = ViewModel.Get();
            this.DataContext = this.vm;
            this.InitializeComponent();
        }

        /// <summary>
        /// Regisztrálás
        /// </summary>
        /// <param name="sender">küldő</param>
        /// <param name="e">eventargs</param>
        private void Regisztracio_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow rw = new RegistrationWindow();
            if (rw.ShowDialog() == true)
            {
                if (this.vm.BL.RegistrationCheck(rw.UjSportolo, this.vm.BL.GetAthleteRepository()))
                {
                    if (rw.EdzoE)
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
                        this.vm.BL.AddNewMember(newTrainer, this.vm.BL.GetAthleteRepository());
                    }
                    else
                    {
                        rw.UjSportolo.IS_PUNISHED = false;
                        rw.UjSportolo.MEMBER_FROM = DateTime.Today.Date;
                        this.vm.BL.AddNewMember(rw.UjSportolo, this.vm.BL.GetAthleteRepository());
                    }

                    this.vm.Athletes = this.vm.BL.ToObservableCollection(this.vm.BL.GetAthleteRepository().GetAll());
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
        /// <param name="sender">Küldő</param>
        /// <param name="e">eventargs</param>
        private void Belepes_Click(object sender, RoutedEventArgs e)
        {
            if (this.vm.BL.LoginEllenorzes(this.vm.BL.GetAthleteRepository(), this.txtFelhnev.Text, this.pwbJelszo.Password))
            {
                MessageBox.Show("Sikeres belépés!");
                var res = from x in this.vm.BL.GetAthleteRepository().GetAll()
                                     where x.NAME == this.txtFelhnev.Text
                                     select x;
                this.vm.LoggedInAthlete = res.First();
                LoggedInWindow lg = new LoggedInWindow();
                lg.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nincs ilyen tagunk....");
            }
        }
    }
}
