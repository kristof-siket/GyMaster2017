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
        static GyMasterDBEntities gde;
        private static AthleteRepository athleteRepo;

        public  AthleteRepository GetAthleteRepository()
        {
            if (athleteRepo == null)
                athleteRepo = new AthleteRepository();
            return athleteRepo;
        }

        public static GyMasterDBEntities GetDbEntities()
        {
            if (gde == null)
                gde = new GyMasterDBEntities();
            return gde;
        }

        public Logic()
        {
            gde = new GyMasterDBEntities() ;
            athleteRepo = new AthleteRepository();
            
        }

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
        /// <returns></returns>
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

        public static ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> enumeration)
        {
            return new ObservableCollection<T>(enumeration);
        }
    }
}
