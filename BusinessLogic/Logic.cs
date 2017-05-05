namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Data;
    using Data.Interfaces;
    using Microsoft.Win32;
    using Excel = Microsoft.Office.Interop.Excel;

    public class Logic
    {
        private GyMasterDBEntities gde;

        /// <summary>
        /// Edzés tömb
        /// </summary>
        private Training[] edzesek;

        /// <summary>
        /// Athlete repository
        /// </summary>
        private AthleteRepository athleteRepo;

        /// <summary>
        /// Gym repository
        /// </summary>
        private GymRepository gymRepo;

        /// <summary>
        /// Edzésterv repository
        /// </summary>
        private TrainingPlanRepository trainingPlanRepo;

        /// <summary>
        /// Gyakorlatok repository
        /// </summary>
        private ExerciseRepository exerciseRepo;

        /// <summary>
        /// Eredmények repository
        /// </summary>
        private ResultRepository resultRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logic"/> class.
        /// Business logic konstruktora
        /// </summary>
        /// <returns>Logic példány.</returns>
        public Logic()
        {
            this.gde = DatabaseEntityProvider.GetDatabaseEntities();
        }

        /// <summary>
        /// Gets or sets edzések tömb propertyje
        /// </summary>
        public Training[] Edzesek { get => this.edzesek; set => this.edzesek = value; }

        //-----------------------------------INNENTŐL JÖNNEK A REPOKAT ELÉRŐ METÓDUSOK-------------------------------//

        /// <summary>
        /// Visszaadja az AthleteRepository-t.
        /// </summary>
        /// <returns>Az egyetlen AthleteRepository.</returns>
        public AthleteRepository GetAthleteRepository()
        {
            if (this.athleteRepo == null)
            {
                this.athleteRepo = new AthleteRepository();
            }

            return this.athleteRepo;
        }

        /// <summary>
        /// Visszaadja a GymRepository-t.
        /// </summary>
        /// <returns>Az egyetlen GymRepository.</returns>
        public GymRepository GetGymRepository()
        {
            if (this.gymRepo == null)
            {
                this.gymRepo = new GymRepository();
            }

            return this.gymRepo;
        }

        /// <summary>
        /// Visszaadja az TrainingPlanRepository-t.
        /// </summary>
        /// <returns>Az egyetlen TrainingPlanRepository.</returns>
        public TrainingPlanRepository GetTrainingPlanRepository()
        {
            if (this.trainingPlanRepo == null)
            {
                this.trainingPlanRepo = new TrainingPlanRepository();
            }

            return this.trainingPlanRepo;
        }

        /// <summary>
        /// Visszaadja az ExerciseRepository-t.
        /// </summary>
        /// <returns>Az egyetlen ExerciseRepository.</returns>
        public ExerciseRepository GetExerciseRepository()
        {
            if (this.exerciseRepo == null)
            {
                this.exerciseRepo = new ExerciseRepository();
            }

            return this.exerciseRepo;
        }

        /// <summary>
        /// Visszaadja az ResultRepository-t.
        /// </summary>
        /// <returns>Az egyetlen ResultRepository.</returns>
        public ResultRepository GetResultRepository()
        {
            if (this.resultRepo == null)
            {
                this.resultRepo = new ResultRepository();
            }

            return this.resultRepo;
        }

        //---------------------------------------REPO-METÓDUSOK VÉGE-----------------------------------------------//

        /// <summary>
        /// Új tag hozzáadása a rendszerhez.
        /// </summary>
        /// <param name="at">A hozzáadandó sportoló.</param>
        /// <param name="ar">Az ATHLETE objektumokhoz való hozzáférést kezelő repository.</param>
        public void AddNewMember(ATHLETE at, IRepository<ATHLETE> ar)
        {
            // ATHLETE at = new ATHLETE { NAME = name, PASSWORD = password };
           ar.Insert(at);
        }

        /// <summary>
        /// Egyszerű, gagyi login validáció.
        /// </summary>
        /// <param name="atr">Athlete repository.</param>
        /// <param name="actUser">Ez lesz majd az inputon beadott név.</param>
        /// <param name="actPasswd">Input jelszó.</param>
        /// <returns>Van, vagy nincs ilyen név-jelszó pár.</returns>
        public bool LoginEllenorzes(IRepository<ATHLETE> atr, string actUser, string actPasswd)
        {
            foreach (ATHLETE a in atr.GetAll())
            {
                if (a.NAME == actUser && a.PASSWORD == actPasswd)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Egy edzéstömböt generál a sportoló tagsági ideje, és a kedvenc, illetve leggyengébb gyakorlatához illeszkedve.
        /// </summary>
        /// <param name="a">A sportoló, akinek a terv készül.</param>
        /// <returns>3 vagy 4 elemű Edzés-ekből álló tömb. (attól függ, hány nap edzés kell)</returns>
        public Training[] GenerateTrainingArray(ATHLETE a)
        {
            int hetiEdzesSzam;
            Training[] edzesTomb;

            if (a.IS_PUNISHED != null && !(bool)a.IS_PUNISHED)
            {
                // először megnézzük, mióta edz (mióta tag), ha 1 év vagy annál több, akkor heti 4 edzés, ha kevesebb, akkor csak 3
                if ((DateTime.Now.Year - a.MEMBER_FROM.Value.Year) <= 3)
                {
                    hetiEdzesSzam = 3;
                }
                else
                {
                    hetiEdzesSzam = 4;
                }

                edzesTomb = new Training[hetiEdzesSzam];
                edzesTomb[0] = new Training() { FoGyakorlat = a.FAVOURITE_EX }; // úgy tervezzük, hogy végezhesse a hét első edzésén a kedvenc gyakorlatát
                switch (edzesTomb[0].FoGyakorlat.NAME)
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

                // a helyzet egyértelmű, minden gyakorlat köré egy edzés épül a héten
                if (hetiEdzesSzam == 3)
                {
                    for (int i = 1; i < edzesTomb.Length; i++)
                    {
                        edzesTomb[i] = new Training();
                    }

                    switch (a.FAVOURITE_EX.NAME)
                    {
                        case "Fekvenyomás": // ha a hét mellnappal indult
                            edzesTomb[1].FoGyakorlat = this.GetExerciseRepository().GetExerciseByName("Guggolás");
                            edzesTomb[1].Title = "Láb edzés";
                            edzesTomb[2].FoGyakorlat = this.GetExerciseRepository().GetExerciseByName("Felhúzás");
                            edzesTomb[2].Title = "Hát edzés";
                            break;
                        case "Guggolás": // ha a hét lábnappal indult
                            edzesTomb[1].FoGyakorlat = this.GetExerciseRepository().GetExerciseByName("Fekvenyomás");
                            edzesTomb[1].Title = "Mell edzés";
                            edzesTomb[2].FoGyakorlat = this.GetExerciseRepository().GetExerciseByName("Felhúzás");
                            edzesTomb[2].Title = "Hát edzés";
                            break;
                        case "Felhúzás": // ha a hét hátnappal indult
                            edzesTomb[1].FoGyakorlat = this.GetExerciseRepository().GetExerciseByName("Fekvenyomás");
                            edzesTomb[1].Title = "Mell edzés";
                            edzesTomb[2].FoGyakorlat = this.GetExerciseRepository().GetExerciseByName("Guggolás");
                            edzesTomb[2].Title = "Láb edzés";
                            break;
                    }
                }

                // valamelyik területre dupla edzés mehet, szóval ki kell választani a leggyengébb területet
                else if (hetiEdzesSzam == 4)
                {
                    var sportolo_eredmenyei = from x in this.GetResultRepository().GetAll().ToList()
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
                    switch (a.FAVOURITE_EX.NAME)
                    {
                        case "Fekvenyomás": // ha a hét mellnappal indult
                            edzesTomb[1].FoGyakorlat = this.GetExerciseRepository().GetExerciseByName("Guggolás");
                            edzesTomb[1].Title = "Láb edzés";
                            edzesTomb[2].FoGyakorlat = this.GetExerciseRepository().GetExerciseByName("Felhúzás");
                            edzesTomb[2].Title = "Hát edzés";
                            break;
                        case "Guggolás": // ha a hét lábnappal indult
                            edzesTomb[1].FoGyakorlat = this.GetExerciseRepository().GetExerciseByName("Fekvenyomás");
                            edzesTomb[1].Title = "Mell edzés";
                            edzesTomb[2].FoGyakorlat = this.GetExerciseRepository().GetExerciseByName("Felhúzás");
                            edzesTomb[2].Title = "Hát edzés";
                            break;
                        case "Felhúzás": // ha a hét hátnappal indult
                            edzesTomb[1].FoGyakorlat = this.GetExerciseRepository().GetExerciseByName("Fekvenyomás");
                            edzesTomb[1].Title = "Mell edzés";
                            edzesTomb[2].FoGyakorlat = this.GetExerciseRepository().GetExerciseByName("Guggolás");
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

                    // megoldjuk, hogy tuti ne legyen 2 egymást követő napon ugyanolyan edzés
                    if (edzesTomb[3].FoGyakorlat.NAME == edzesTomb[2].FoGyakorlat.NAME)
                    {
                        Training atm = edzesTomb[2];
                        edzesTomb[2] = edzesTomb[1];
                        edzesTomb[1] = atm;
                    }
                }

                return edzesTomb;
            }
            else
            {
                throw new AthleteIsPunishedException(); // ezt a hívás helyén kell majd elkapni
            }
        }

        /// <summary>
        /// Edzésterv objektumot készít az eddigi metódusok segítségével.
        /// </summary>
        /// <param name="a">A sportoló, akinek készül.</param>
        /// <returns>Új edzésterv.</returns>
        public TRAINING_PLAN GenerateTrainingPlan(ATHLETE a)
        {
            string filename = this.CreateExcelFromTrainingArray(this.Edzesek);
            return new TRAINING_PLAN() { FILENAME = filename, RELEASE_DATE = DateTime.Now, ATHLETE = a, ATHLETE_ID = a.ID };
        }

        /// <summary>
        /// Ellenőrzi hogy nincs e két ugyan olyan felhasználó
        /// </summary>
        /// <param name="at">regisztrálni kívánt felhasználó</param>
        /// <param name="atr">Athlete repository.</param>
        /// <returns>Van-e már ilyen adatokkal rendelkező user.</returns>
        public bool RegistrationCheck(ATHLETE at, IRepository<ATHLETE> atr)
        {
            var res = from x in atr.GetAll()
                      where x.NAME == at.NAME && x.BORN_DATE == at.BORN_DATE
                      select x;
            if (res.Count() != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

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

        private string CreateExcelFromTrainingArray(Training[] trainings)
        {
            var excelApp = new Excel.Application();
            excelApp.Workbooks.Add();
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
            workSheet.Cells[1, "A"] = "Sorszám";
            workSheet.Cells[1, "B"] = "Edzés neve";
            workSheet.Cells[1, "C"] = "Fő gyakorlat";
            workSheet.Cells[1, "D"] = "Leírás";

            int row = 1;
            foreach (Training t in trainings)
            {
                row++;
                workSheet.Cells[row, "A"] = row - 1;
                workSheet.Cells[row, "B"] = t.Title;
                workSheet.Cells[row, "C"] = t.FoGyakorlat.NAME;
                workSheet.Cells[row, "D"] = t.Leiras;
            }

            workSheet.Columns[1].AutoFit();
            workSheet.Columns[2].AutoFit();
            workSheet.Columns[3].AutoFit();
            workSheet.Columns[4].ColumnWidth = 30;

            workSheet.Rows[1].AutoFit();
            workSheet.Rows[2].AutoFit();
            workSheet.Rows[3].AutoFit();
            workSheet.Rows[4].AutoFit();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"C:\";
            sfd.RestoreDirectory = true;
            sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
            sfd.FilterIndex = 0;
            sfd.Title = "Edzésterv mentése excel táblázatba...";
            sfd.FileName = "edzesterv";
            string path = string.Empty;

            if (sfd.ShowDialog() == true)
            {
                path = sfd.FileName;

                excelApp.Workbooks[1].SaveCopyAs(path);
                excelApp.Workbooks[1].Saved = true;
                excelApp.Workbooks[1].Close(true);
                excelApp.Quit();
            }

            return Path.GetFileName(path);
        }
    }
}
