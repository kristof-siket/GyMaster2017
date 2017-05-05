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
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        /// <summary>
        /// Viewmodel példány
        /// </summary>
        private ViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationWindow"/> class.
        /// RegistrationWindow konstruktora
        /// </summary>
        /// <param name="mod">ha módosítunk egy sportolót true ha hozzáadunk false</param>
        public RegistrationWindow(bool mod = false)
        {
            this.vm = ViewModel.Get();
            this.DataContext = this.vm;
            this.InitializeComponent();
            if (mod)
            {
                if (this.vm.LoggedInAthlete is TRAINER)
                {
                    this.chkEdzo.IsChecked = true;
                }
                else
                {
                    this.chkEdzo.IsChecked = false;
                }

                this.chkEdzo.IsEnabled = false;
                this.vm = ViewModel.Get();
                this.DataContext = this.vm.LoggedInAthlete;
            }
            else
            {
                this.UjSportolo = new ATHLETE();
                this.DataContext = this.UjSportolo;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether edzoE property
        /// </summary>
        public bool EdzoE { get; set; }

        /// <summary>
        /// Gets regisztráció során létrejött új sportoló
        /// </summary>
        public ATHLETE UjSportolo { get; private set; }

        /// <summary>
        /// Csak szöveget írhat
        /// </summary>
        /// <param name="sender">Küldő</param>
        /// <param name="e">eventargsparam>
        private void SzovegPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char ch in e.Text)
            {
                if (!char.IsLetter(ch))
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Csak számot írhat
        /// </summary>
        /// <param name="sender">Küldő</param>
        /// <param name="e">eventargs</param>
        private void SzamPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char ch in e.Text)
            {
                if (!char.IsNumber(ch))
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Módosítás ill. regisztráció esetén ment
        /// </summary>
        /// <param name="sender">küldő</param>
        /// <param name="e">eventargs</param>
        private void Mentes_Click(object sender, RoutedEventArgs e)
        {
            this.txtMagassag.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.txtNev.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.txtSuly.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.txtSzulHely.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.dpSzulDatum.GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();
            if (this.chkEdzo.IsChecked == true)
            {
                this.EdzoE = true;
            }

            this.DialogResult = true;
        }

        /// <summary>
        /// Jelszó változás esetén megadja az új jelszót
        /// </summary>
        /// <param name="sender">Küldő</param>
        /// <param name="e">eventargs</param>
        private void Password_changed(object sender, RoutedEventArgs e)
        {
            if (this.UjSportolo != null)
            {
                this.UjSportolo.PASSWORD = this.psdJelszo.Password;
            }
            else
            {
                this.vm.LoggedInAthlete.PASSWORD = this.psdJelszo.Password;
            }
        }
    }
}
