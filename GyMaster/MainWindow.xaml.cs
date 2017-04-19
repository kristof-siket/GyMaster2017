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
        public MainWindow()
        {
            InitializeComponent();
            Logic l = new Logic();
            foreach (ATHLETE item in Logic.repo.GetAll())
            {
                Debug.WriteLine(item.NAME);
            }
            //GyMasterDatabaseEntities ge = new GyMasterDatabaseEntities();
            //Console.WriteLine(ge.GYM.First().ADDRESS);
        }
       

        private void Regisztracio_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow rw = new RegistrationWindow();
            rw.ShowDialog();
        }

        private void Belepes_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
