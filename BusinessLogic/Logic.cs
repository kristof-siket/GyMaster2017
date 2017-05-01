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

        /// <summary>
        /// Új tag hozzáadása a rendszerhez.
        /// </summary>
        /// <param name="at">A hozzáadandó sportoló.</param>
        /// <param name="ar">Az ATHLETE objektumokhoz való hozzáférést kezelő repository.</param>
        public void addNewMember(ATHLETE at,AthleteRepository ar)
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

        /// <summary>
        /// Egy edzéstömböt generál a sportoló tagsági ideje, és a kedvenc, illetve leggyengébb gyakorlatához illeszkedve.
        /// </summary>
        /// <param name="a">A sportoló, akinek a terv készül.</param>
        /// <returns>3 vagy 4 elemű Edzés-ekből álló tömb. (attól függ, hány nap edzés kell)</returns>
        private Training[] GenerateTrainingArray(ATHLETE a)
        {
            int hetiEdzesSzam;
            Training[] edzesTomb;

            if (a.IS_PUNISHED != null && !(bool)a.IS_PUNISHED)
            {
                // először megnézzük, mióta edz (mióta tag), ha 1 év vagy annál több, akkor heti 4 edzés, ha kevesebb, akkor csak 3
                if ((DateTime.Now.Year - a.MEMBER_FROM.Value.Year) <= 2)
                    hetiEdzesSzam = 3;
                else
                    hetiEdzesSzam = 4;
                edzesTomb = new Training[hetiEdzesSzam];
                edzesTomb[0] = new Training() { FoGyakorlat = a.FAVOURITE_EX };
                switch (edzesTomb[0].FoGyakorlat.NAME) // úgy tervezzük, hogy végezhesse a hét első edzésén a kedvenc gyakorlatát
                {
                    case "Fekvenyomás":
                        edzesTomb[0].Title = "Mell edzés";
                        break;
                    case "Guggolás":
                        edzesTomb[0].Title = "Láb edzés";
                        break;
                    case "Felhúzás":
                        edzesTomb[0].Title = "Hát edzés";
                        break;
                    default:
                        edzesTomb[0].Title = "Nem működik ez a szviccs.";
                        break;
                }

                if (hetiEdzesSzam == 3) // a helyzet egyértelmű, minden gyakorlat köré egy edzés épül a héten
                {
                    for (int i = 1; i < edzesTomb.Length; i++)
                    {
                        edzesTomb[i] = new Training();
                    }

                    switch (a.FAVOURITE_EX.NAME) // elég redundáns, majd lehet refaktorálni
                    {
                        case "Fekvenyomás": // ha a hét mellnappal indult
                            edzesTomb[1].FoGyakorlat = GetExerciseRepository().GetExerciseByName("Guggolás");
                            edzesTomb[1].Title = "Láb edzés";
                            edzesTomb[2].FoGyakorlat = GetExerciseRepository().GetExerciseByName("Felhúzás");
                            edzesTomb[2].Title = "Hát edzés";
                            break;
                        case "Guggolás": // ha a hét lábnappal indult
                            edzesTomb[1].FoGyakorlat = GetExerciseRepository().GetExerciseByName("Fekvenyomás");
                            edzesTomb[1].Title = "Mell edzés";
                            edzesTomb[2].FoGyakorlat = GetExerciseRepository().GetExerciseByName("Felhúzás");
                            edzesTomb[2].Title = "Hát edzés";
                            break;
                        case "Felhúzás": // ha a hét hátnappal indult
                            edzesTomb[1].FoGyakorlat = GetExerciseRepository().GetExerciseByName("Fekvenyomás");
                            edzesTomb[1].Title = "Mell edzés";
                            edzesTomb[2].FoGyakorlat = GetExerciseRepository().GetExerciseByName("Guggolás");
                            edzesTomb[2].Title = "Láb edzés";
                            break;
                    }
                }

                else if (hetiEdzesSzam == 4) // valamelyik területre dupla edzés mehet, szóval ki kell választani a leggyengébb területet
                {
                    var sportolo_eredmenyei = from x in GetResultRepository().GetAll().ToList()
                                              where x.ATHLETE == a
                                              select x;

                    var res_weakest = from x in sportolo_eredmenyei
                                      let xmin = sportolo_eredmenyei.Min(y => y.RES_KG)
                                      where x.RES_KG == xmin
                                      select x.EXERCISE;

                    for (int i = 1; i < edzesTomb.Length; i++)
                    {
                        edzesTomb[i] = new Training();
                    }

                    EXERCISE leggyengebbGyakorlat = res_weakest.ToList().Single();
                    switch (a.FAVOURITE_EX.NAME) // elég redundáns, majd lehet refaktorálni
                    {
                        case "Fekvenyomás": // ha a hét mellnappal indult
                            edzesTomb[1].FoGyakorlat = GetExerciseRepository().GetExerciseByName("Guggolás");
                            edzesTomb[1].Title = "Láb edzés";
                            edzesTomb[2].FoGyakorlat = GetExerciseRepository().GetExerciseByName("Felhúzás");
                            edzesTomb[2].Title = "Hát edzés";
                            break;
                        case "Guggolás": // ha a hét lábnappal indult
                            edzesTomb[1].FoGyakorlat = GetExerciseRepository().GetExerciseByName("Fekvenyomás");
                            edzesTomb[1].Title = "Mell edzés";
                            edzesTomb[2].FoGyakorlat = GetExerciseRepository().GetExerciseByName("Felhúzás");
                            edzesTomb[2].Title = "Hát edzés";
                            break;
                        case "Felhúzás": // ha a hét hátnappal indult
                            edzesTomb[1].FoGyakorlat = GetExerciseRepository().GetExerciseByName("Fekvenyomás");
                            edzesTomb[1].Title = "Mell edzés";
                            edzesTomb[2].FoGyakorlat = GetExerciseRepository().GetExerciseByName("Guggolás");
                            edzesTomb[2].Title = "Láb edzés";
                            break;
                    }
                    edzesTomb[3].FoGyakorlat = leggyengebbGyakorlat; // beállítjuk az utsó edzésre a leggyengébb gyakorlatot
                    switch (leggyengebbGyakorlat.NAME)
                    {
                        case "Fekvenyomás":
                            edzesTomb[3].Title = "Mell edzés";
                            break;
                        case "Guggolás":
                            edzesTomb[3].Title = "Láb edzés";
                            break;
                        case "Felhúzás":
                            edzesTomb[3].Title = "Hát edzés";
                            break;
                    }

                    if (edzesTomb[3].FoGyakorlat.NAME == edzesTomb[2].FoGyakorlat.NAME) // megoldjuk, hogy tuti ne legyen 2 egymást követő napon ugyanolyan edzés
                    {
                        Training atm = edzesTomb[2];
                        edzesTomb[2] = edzesTomb[1];
                        edzesTomb[1] = atm;
                    }
                }
                return edzesTomb;
            }
            else
                throw new AthleteIsPunishedException(); // ezt a hívás helyén kell majd elkapni
        }

        // ezzel csak tesztelem
        public void GenerateTrainingPlan(string athleteName)
        {
            Training[] edzesTomb = GenerateTrainingArray(GetAthleteRepository().GetAthleteByName(athleteName));

            foreach (Training t in edzesTomb)
            {
                Console.WriteLine(t.Title);
            }
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

    public class AthleteIsPunishedException : Exception
    {

    }

    class Training
    {
        public string Title { get; set; }
        public EXERCISE FoGyakorlat { get; set; }
        public string Leiras { get; set; }
    }
}
