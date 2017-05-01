using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GyMaster
{
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(n));
            }
        }
    }

    /// <summary>
    /// Singleton tervezésű ViewModel az MVVM pattern implementálásához.
    /// </summary>
    public class ViewModel : Bindable
    {
        Logic bl;

        // ----------------------------BINDING----------------------------------- //

        private ATHLETE selectedAthlete;

        public ATHLETE SelectedAthlete
        {
            get { return selectedAthlete; }
            set { selectedAthlete = value; OPC(); }
        }
        public ATHLETE loggedInAthlete { get; set; }
        public ObservableCollection<ATHLETE> Athletes { get; set; }
        public ObservableCollection<GYM> Gyms { get; set; }
        public ObservableCollection<TRAINING_PLAN> TrainingPlans { get; set; }
        public ObservableCollection<EXERCISE> Exercises { get; set; }
        public ObservableCollection<RESULT> Results { get; set; }

        // ----------------------------END BINDING----------------------------------- //

        public Logic BL
        {
            get { return bl; }
            private set { bl = value; }
        }

        private static ViewModel _peldany;

        /// <summary>
        /// Visszaadja az egyetlen ViewModel példányt, ha nem létezik, létrehozza.
        /// </summary>
        /// <returns>A ViewModel.</returns>
        public static ViewModel Get()
        {
            if (_peldany == null)
                _peldany = new ViewModel();
            return _peldany;
        }

        private ViewModel()
        {
            bl = new Logic();
            Athletes = bl.ToObservableCollection(bl.GetAthleteRepository().GetAll());
            Gyms = bl.ToObservableCollection(bl.GetGymRepository().GetAll());
            TrainingPlans = bl.ToObservableCollection(bl.GetTrainingPlanRepository().GetAll());
            Exercises = bl.ToObservableCollection(bl.GetExerciseRepository().GetAll());
            Results = bl.ToObservableCollection(bl.GetResultRepository().GetAll());
        }
    }
}