namespace GyMaster
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using BusinessLogic;
    using Data;

    /// <summary>
    /// Az 'adatköthető' entitásokat reprezentáló absztrakt osztály.
    /// </summary>
    public abstract class Bindable : INotifyPropertyChanged
    {
        /// <summary>
        /// A tulajdonságváltozást jelző esemény.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// PropertyChanged esemény "elsütése".
        /// </summary>
        /// <param name="n">A hívó property.</param>
        protected void OPC([CallerMemberName]string n = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(n));
            }
        }
    }
}