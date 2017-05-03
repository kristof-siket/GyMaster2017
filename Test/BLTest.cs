using BusinessLogic;
using Data;
using Data.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class BLTest
    {
        Mock<IRepository<ATHLETE>> m;
        Logic l;
        public void SetUp()
        {
            m = new Mock<IRepository<ATHLETE>>();
            l = new Logic();
            List<ATHLETE> ATHLETES = new List<ATHLETE>()
            {
                new ATHLETE(){NAME="Valaki",HEIGHT=160,WEIGHT=150,ID=1,PASSWORD="asd"},
                new ATHLETE(){NAME="Valaki2",HEIGHT=161,WEIGHT=140,ID=2,PASSWORD="asd"},
                new ATHLETE(){NAME="Valaki3",HEIGHT=162,WEIGHT=130,ID=3,PASSWORD="asd"},
                new ATHLETE(){NAME="Valaki4",HEIGHT=163,WEIGHT=120,ID=4,PASSWORD="asd"},
                new ATHLETE(){NAME="Valaki5",HEIGHT=164,WEIGHT=110,ID=5,PASSWORD="asd"}
            };
            m.Setup(x => x.GetAll()).Returns(ATHLETES.AsQueryable());
        }
    }
}
