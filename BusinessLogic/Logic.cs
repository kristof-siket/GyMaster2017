﻿using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Logic
    {
        GyMasterDatabaseEntities gde;
        public static GyMasterRepository repo { get; set; }
        public Logic()
        {
            gde = new GyMasterDatabaseEntities();
            repo = new GyMasterRepository(gde);
        }

        public static void addNewMember(ATHLETE at,GyMasterRepository repo)
        {
            repo.Insert(at);
        }
    }
}