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
        ViewModel VM;
        public MainWindow()
        {
            VM = ViewModel.Get();
            this.DataContext = VM;
            InitializeComponent();
        }
       

        private void Regisztracio_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow rw = new RegistrationWindow();
            rw.ShowDialog();
        }

        private void Belepes_Click(object sender, RoutedEventArgs e)
        {
            if (VM.BL.LoginEllenorzes(txtFelhnev.Text, pwbJelszo.Password))
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
