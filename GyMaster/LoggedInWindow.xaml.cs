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
            RegistrationWindow rw = new RegistrationWindow(true);
            rw.ShowDialog();
        }

        private void Megbuntet_Click(object sender, RoutedEventArgs e)
        {
            if (VM.loggedInAthlete is TRAINER)
            {
                VM.selectedAthlete.IS_PUNISHED = true;              
            }

            else
                MessageBox.Show("Nem büntetheted meg mivel, Te nem vagy edző!");
            
        }
    }
}
