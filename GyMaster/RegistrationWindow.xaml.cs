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
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        ViewModel VM;
        public RegistrationWindow()
        {
            VM = ViewModel.Get();
            this.DataContext = VM;
            InitializeComponent();
            
        }

        private void szovegPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char ch in e.Text)
            {
                if (!Char.IsLetter(ch))
                {
                    e.Handled = true;
                }
            }
        }

        private void szamPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char ch in e.Text)
            {
                if (!Char.IsNumber(ch))
                {
                    e.Handled = true;
                }
            }
        }

        private void Mentes_Click(object sender, RoutedEventArgs e)
        {
            Logic.addNewMember(new ATHLETE
            {
                BORN_DATE = null,
                NAME = txtNev.Text,
                HEIGHT = int.Parse(txtMagassag.Text),
                WEIGHT = int.Parse(txtSuly.Text),
                ID = VM.BL.GetAthleteRepository().GetAll().Count() + 1,
            },
              VM.BL.GetAthleteRepository());
        }
    }
}
