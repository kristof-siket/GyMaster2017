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
    /// Singleton tervezésű ViewModel az MVVM pattern implementálásához.
    /// </summary>
    public class ViewModel : Bindable
    {
        /// <summary>
        /// Egyetlen viewmodel példány
        /// </summary>
        private static ViewModel peldany;

        /// <summary>
        /// Logic példány
        /// </summary>
        private Logic bl;

        // ----------------------------BINDING----------------------------------- //
        private ATHLETE selectedAthlete;
        private List<string> selectedathleteExercises;
        private List<string> loggedinathleteExercises;
        private List<string> exercises;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// Viewmodel konstruktor
        /// </summary>
        private ViewModel()
        {
            this.bl = new Logic();
            this.Athletes = this.bl.ToObservableCollection(this.bl.GetAthleteRepository().GetAll());
            this.Gyms = this.bl.ToObservableCollection(this.bl.GetGymRepository().GetAll());
            this.TrainingPlans = this.bl.ToObservableCollection(this.bl.GetTrainingPlanRepository().GetAll());
            this.Exercises = this.bl.ToObservableCollection(this.bl.GetExerciseRepository().GetAll());
            this.Results = this.bl.ToObservableCollection(this.bl.GetResultRepository().GetAll());
        }

        /// <summary>
        /// Gets kiválasztott atléta gyakorlatait visszaadó lista
        /// </summary>
        public List<string> SelectedAthleteExercises
        {
            get
            {
                this.selectedathleteExercises = new List<string>();
                {
                    foreach (RESULT res in this.selectedAthlete.RESULT)
                    {
                        if (!this.selectedathleteExercises.Contains(res.EXERCISE.NAME))
                        {
                            this.selectedathleteExercises.Add(res.EXERCISE.NAME);
                        }
                    }

                    return this.selectedathleteExercises;
                }
            }
        }

        /// <summary>
        /// Gets bejelentkezett felhasználó gyakorlatait tartalmazó lista
        /// </summary>
        public List<string> LoggedInAthleteExercises
        {
            get
            {
                this.loggedinathleteExercises = new List<string>();
                {
                    foreach (RESULT res in this.LoggedInAthlete.RESULT)
                    {
                        if (!this.loggedinathleteExercises.Contains(res.EXERCISE.NAME))
                        {
                            this.loggedinathleteExercises.Add(res.EXERCISE.NAME);
                        }
                    }

                    return this.loggedinathleteExercises;
                }
            }
        }

        /// <summary>
        /// Gets or sets kiválasztott atléta property
        /// </summary>
        public ATHLETE SelectedAthlete
        {
            get
            {
                return this.selectedAthlete;
            }

            set
            {
                this.selectedAthlete = value;
                this.OPC();
            }
        }

        /// <summary>
        /// Gets exercise lista
        /// </summary>
        public List<string> ExercisesList
        {
            get
            {
                this.exercises = new List<string>();
                foreach (EXERCISE ex in this.bl.GetExerciseRepository().GetAll())
                {
                    this.exercises.Add(ex.NAME);
                }

                return this.exercises;
            }
        }

        /// <summary>
        /// Gets or sets aktuálisan elkészített edzéslista.
        /// </summary>
        public ObservableCollection<Training> TrainingList { get; set; }

        /// <summary>
        /// Gets or sets bejelentkezett sportoló
        /// </summary>
        public ATHLETE LoggedInAthlete { get; set; }

        /// <summary>
        /// Gets or sets sportolókat tartalmazó ObservableCollection
        /// </summary>
        public ObservableCollection<ATHLETE> Athletes { get; set; }

        /// <summary>
        /// Gets or sets konditermeket tartalmazó ObservableCollection
        /// </summary>
        public ObservableCollection<GYM> Gyms { get; set; }

        /// <summary>
        /// Gets or sets edzésterveket tartalmazó ObservableCollection
        /// </summary>
        public ObservableCollection<TRAINING_PLAN> TrainingPlans { get; set; }

        /// <summary>
        /// Gets or sets gyakorlatokat tartalmazó ObservableCollection
        /// </summary>
        public ObservableCollection<EXERCISE> Exercises { get; set; }

        /// <summary>
        /// Gets or sets eredményeket tartalmazó ObservableCollection
        /// </summary>
        public ObservableCollection<RESULT> Results { get; set; }

        // ----------------------------END BINDING----------------------------------- //

        /// <summary>
        /// Gets logic
        /// </summary>
        public Logic BL
        {
            get { return this.bl; }
            private set { this.bl = value; }
        }

        /// <summary>
        /// Visszaadja az egyetlen ViewModel példányt, ha nem létezik, létrehozza.
        /// </summary>
        /// <returns>A ViewModel.</returns>
        public static ViewModel Get()
        {
            if (peldany == null)
            {
                peldany = new ViewModel();
            }

            return peldany;
        }
    }
}