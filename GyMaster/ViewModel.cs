using BusinessLogic;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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
        /// <summary>
        /// Logic példány
        /// </summary>
        Logic bl;

        // ----------------------------BINDING----------------------------------- //

        private ATHLETE selectedAthlete;
        private List<string> selectedathleteExercises;
        private List<string> loggedinathleteExercises;
        private List<string> exercises;

        public ObservableCollection<Training> TrainingList { get; set; }

        /// <summary>
        /// Exercise listát visszaadó property
        /// </summary>
        public List<string> ExercisesList
        {
            get
            {
                exercises = new List<string>();
                foreach(EXERCISE ex in bl.GetExerciseRepository().GetAll())
                {
                    exercises.Add(ex.NAME);
                }
                return exercises;
            }
        }

        /// <summary>
        /// Kiválasztott atléta gyakorlatait visszaadó lista
        /// </summary>
        public List<string> SelectedAthleteExercises
        {
            get
            {
                selectedathleteExercises = new List<string>();               
                {
                    foreach (RESULT res in selectedAthlete.RESULT)
                    {
                        if (!selectedathleteExercises.Contains(res.EXERCISE.NAME))
                            selectedathleteExercises.Add(res.EXERCISE.NAME);
                    }
                    return selectedathleteExercises;
                }
               
            }
        }

        /// <summary>
        /// Bejelentkezett felhasználó gyakorlatait visszaadó lista
        /// </summary>
        public List<string> LoggedInAthleteExercises
        {
            get
            {
                loggedinathleteExercises = new List<string>();
                {
                    foreach (RESULT res in loggedInAthlete.RESULT)
                    {
                        if (!loggedinathleteExercises.Contains(res.EXERCISE.NAME))
                            loggedinathleteExercises.Add(res.EXERCISE.NAME);
                    }
                    return loggedinathleteExercises;
                }

            }
        }

        /// <summary>
        /// Kiválasztott atléta property
        /// </summary>
        public ATHLETE SelectedAthlete
        {
            get { return selectedAthlete; }
            set { selectedAthlete = value; OPC(); }
        }
       
        /// <summary>
        /// Bejelentkezett sportolót visszaadó property
        /// </summary>
        public ATHLETE loggedInAthlete { get; set; }

        /// <summary>
        /// Sportolókat tartalmazó ObservableCollectiont visszaadó property
        /// </summary>
        public ObservableCollection<ATHLETE> Athletes { get; set; }

        /// <summary>
        /// Konditermeket tartalmazó ObservableCollectiont visszaadó property
        /// </summary>
        public ObservableCollection<GYM> Gyms { get; set; }

        /// <summary>
        /// Edzésterveket tartalmazó ObservableCollectiont visszaadó property
        /// </summary>
        public ObservableCollection<TRAINING_PLAN> TrainingPlans { get; set; }

        /// <summary>
        /// Gyakorlatokat tartalmazó ObservableCollectiont visszaadó property
        /// </summary>
        public ObservableCollection<EXERCISE> Exercises { get; set; }

        /// <summary>
        /// Eredményeket tartalmazó ObservableCollectiont visszaadó property
        /// </summary>
        public ObservableCollection<RESULT> Results { get; set; }

        // ----------------------------END BINDING----------------------------------- //

        
        /// <summary>
        /// Logic konstruktora
        /// </summary>
        public Logic BL
        {
            get { return bl; }
            private set { bl = value; }
        }

        /// <summary>
        /// Egyetlen viewmodel példány
        /// </summary>
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

        /// <summary>
        /// Viewmodel konstruktor
        /// </summary>
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