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
        /// <summary>
        /// Viewmodel példány
        /// </summary>
        ViewModel VM;

        /// <summary>
        /// edzoE property
        /// </summary>
        public bool edzoE { get; set; }

        /// <summary>
        /// Regisztráció során létrejött új sportoló
        /// </summary>
        public ATHLETE UjSportolo { get; private set; }
                    
        /// <summary>
        /// RegistrationWindow konstruktora
        /// </summary>
        /// <param name="mod">ha módosítunk egy sportolót true ha hozzáadunk false</param>
        public RegistrationWindow(bool mod=false)
        {
            VM = ViewModel.Get();
            this.DataContext = VM;
            InitializeComponent();
            if(mod)
            {
                if((VM.loggedInAthlete is TRAINER))
                {
                    chkEdzo.IsChecked = true;
                }
                else
                {
                    chkEdzo.IsChecked = false;
                }
                chkEdzo.IsEnabled = false;
                VM = ViewModel.Get();                
                DataContext = VM.loggedInAthlete;
            }
            else
            {
                UjSportolo = new ATHLETE();
                DataContext = UjSportolo;
            }
            
        }

        /// <summary>
        /// Csak szöveget írhat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Csak számot írhat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Módosítás ill. regisztráció esetén ment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mentes_Click(object sender, RoutedEventArgs e)
        {
            txtMagassag.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtNev.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtSuly.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtSzulHely.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            dpSzulDatum.GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();
            if(chkEdzo.IsChecked==true)
            {
                edzoE = true;
            }                                           
            this.DialogResult = true;
            
        }


        /// <summary>
        /// Jelszó változás esetén megadja az új jelszót
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Password_changed(object sender, RoutedEventArgs e)
        {
            if (UjSportolo != null)
                UjSportolo.PASSWORD = psdJelszo.Password;
            else
                VM.loggedInAthlete.PASSWORD = psdJelszo.Password;
        }
    }
}
