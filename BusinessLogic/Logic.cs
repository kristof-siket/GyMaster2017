using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BusinessLogic
{
    public class Logic
    {
        private GyMasterDBEntities gde;

        private AthleteRepository athleteRepo;
        private GymRepository gymRepo;
        private TrainingPlanRepository trainingPlanRepo;
        private ExerciseRepository exerciseRepo;
        private ResultRepository resultRepo;

        public Logic()
        {
            gde = DatabaseEntityProvider.GetDatabaseEntities();
        }

        //-----------------------------------INNENTŐL JÖNNEK A REPOKAT ELÉRŐ METÓDUSOK-------------------------------//

        /// <summary>
        /// Visszaadja az AthleteRepository-t.
        /// </summary>
        /// <returns>Az egyetlen AthleteRepository.</returns>
        public  AthleteRepository GetAthleteRepository()
        {
            if (athleteRepo == null)
                athleteRepo = new AthleteRepository();
            return athleteRepo;
        }

        /// <summary>
        /// Visszaadja a GymRepository-t.
        /// </summary>
        /// <returns>Az egyetlen GymRepository.</returns>
        public GymRepository GetGymRepository()
        {
            if (gymRepo == null)
                gymRepo = new GymRepository();
            return gymRepo;
        }

        /// <summary>
        /// Visszaadja az TrainingPlanRepository-t.
        /// </summary>
        /// <returns>Az egyetlen TrainingPlanRepository.</returns>
        public TrainingPlanRepository GetTrainingPlanRepository()
        {
            if (trainingPlanRepo == null)
                trainingPlanRepo = new TrainingPlanRepository();
            return trainingPlanRepo;
        }

        /// <summary>
        /// Visszaadja az ExerciseRepository-t.
        /// </summary>
        /// <returns>Az egyetlen ExerciseRepository.</returns>
        public ExerciseRepository GetExerciseRepository()
        {
            if (exerciseRepo == null)
                exerciseRepo = new ExerciseRepository();
            return exerciseRepo;
        }

        /// <summary>
        /// Visszaadja az ResultRepository-t.
        /// </summary>
        /// <returns>Az egyetlen ResultRepository.</returns>
        public ResultRepository GetResultRepository()
        {
            if (resultRepo == null)
                resultRepo = new ResultRepository();
            return resultRepo;
        }

        //---------------------------------------REPO-METÓDUSOK VÉGE-----------------------------------------------//

        public static void addNewMember(ATHLETE at,AthleteRepository ar)
        {
            //ATHLETE at = new ATHLETE { NAME = name, PASSWORD = password };           
           ar.Insert(at);                    
        }

        /// <summary>
        /// Egyszerű, gagyi login validáció.
        /// </summary>
        /// <param name="actUser">Ez lesz majd az inputon beadott név.</param>
        /// <param name="actPasswd">Input jelszó.</param>
        /// <returns>Van, vagy nincs ilyen név-jelszó pár.</returns>
        public bool LoginEllenorzes(string actUser, string actPasswd)
        {
            foreach (ATHLETE a in athleteRepo.GetAll())
                if (a.NAME == actUser && a.PASSWORD == actPasswd)
                    return true;
            return false;
        }

        //public static void GUIBuild(ATHLETE loggedInAthlete, Grid g)
        //{
        //    int i = 1;


        //    for (int j = 0; j < 5; j++)
        //    {
        //        ColumnDefinition cd = new ColumnDefinition();
        //        g.ColumnDefinitions.Add(cd);
        //    }
        //    RowDefinition rdf = new RowDefinition();
        //    g.RowDefinitions.Add(rdf);
        //    Label loggedName = new Label();
        //    loggedName.Content = loggedInAthlete.NAME;
        //    loggedName.FontSize = 30;
        //    Label loggedHeight = new Label();
        //    loggedHeight.Content = loggedInAthlete.HEIGHT;
        //    loggedHeight.FontSize = 30;
        //    Label loggedWeight = new Label();
        //    loggedWeight.Content = loggedInAthlete.WEIGHT;
        //    loggedWeight.FontSize = 30;
        //    g.Children.Add(loggedName);
        //    g.Children.Add(loggedHeight);
        //    g.Children.Add(loggedWeight);
        //    Grid.SetColumn(loggedName, 0);
        //    Grid.SetRow(loggedName, 0);
        //    Grid.SetColumn(loggedHeight, 1);
        //    Grid.SetRow(loggedHeight, 0);
        //    Grid.SetColumn(loggedWeight, 2);
        //    Grid.SetRow(loggedWeight, 0);

        //    foreach (ATHLETE a in athleteRepo.GetAll().ToList())
        //    {
        //        RowDefinition rd = new RowDefinition();
        //        g.RowDefinitions.Add(rd);
        //        Label name = new Label();
        //        Label height = new Label();
        //        Label age = new Label();
        //        Label weight = new Label();
        //        name.Content = a.NAME;
        //        height.Content = a.HEIGHT;
        //        weight.Content = a.WEIGHT;
        //        g.Children.Add(name);
        //        g.Children.Add(height);
        //        g.Children.Add(weight);
        //        Grid.SetColumn(name, 0);
        //        Grid.SetRow(name, i);
        //        Grid.SetColumn(height, 1);
        //        Grid.SetRow(height, i);
        //        Grid.SetColumn(weight, 2);
        //        Grid.SetRow(weight, i);
        //        i++;
        //    }


        //}

        /// <summary>
        /// Gyűjteményt alakít ObservableCollection-ná (GUI megjelenítéshez).
        /// </summary>
        /// <typeparam name="T">A collection típusparamétere.</typeparam>
        /// <param name="enumeration">A bemeneti gyűjtemény.</param>
        /// <returns>A bemeneti gyűjtemény konvertálva ObservableCollection-né.</returns>
        public ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> enumeration) // Kristóf: átírtam ezt is példányszintűre, mivel a ViewModelben úgyis le van példányosítva a BL
        {
            return new ObservableCollection<T>(enumeration);
        }
    }
}
