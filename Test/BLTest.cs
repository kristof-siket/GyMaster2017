namespace Test
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLogic;
    using Data;
    using Data.Interfaces;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class BLTest
    {
        private Mock<IRepository<ATHLETE>> m;
        private Logic l;
        private ObservableCollection<ATHLETE> oc;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.m = new Mock<IRepository<ATHLETE>>();
            this.l = new Logic();
            List<ATHLETE> athletes = new List<ATHLETE>()
            {
                new ATHLETE() { NAME = "Valaki", HEIGHT = 160, WEIGHT = 150, ID = 1, PASSWORD = "asd", BORN_DATE = new DateTime(2010, 12, 18) },
                new ATHLETE() { NAME = "Valaki2", HEIGHT = 161, WEIGHT = 140, ID = 2, PASSWORD = "asd", BORN_DATE = new DateTime(2000, 10, 18) },
                new ATHLETE() { NAME = "Valaki3", HEIGHT = 162, WEIGHT = 130, ID = 3, PASSWORD = "asd", BORN_DATE = new DateTime(1995, 12, 10) },
                new ATHLETE() { NAME = "Valaki4", HEIGHT = 163, WEIGHT = 120, ID = 4, PASSWORD = "asd", BORN_DATE = new DateTime(2002, 10, 01) },
                new ATHLETE() { NAME = "Valaki5", HEIGHT = 164, WEIGHT = 110, ID = 5, PASSWORD = "asd", BORN_DATE = new DateTime(2010, 12, 17) }
            };
            this.m.Setup(x => x.GetAll()).Returns(athletes.AsQueryable());
            this.oc = new ObservableCollection<ATHLETE>();
        }

        [Test]
        public void LoginEllenorzesTest()
        {
            bool result = this.l.LoginEllenorzes(this.m.Object, "Valaki", "asd");
            bool result2 = this.l.LoginEllenorzes(this.m.Object, "Val", "asd");
            Assert.That(result, Is.EqualTo(true));
            Assert.That(result2, Is.EqualTo(false));
        }

        [Test]
        public void RegistrationCheckTest()
        {
            ATHLETE a1 = new ATHLETE() { NAME = "Valaki", ID = 10, BORN_DATE = new DateTime(2010, 12, 18) };
            ATHLETE a2 = new ATHLETE() { NAME = "Valak", ID = 10, BORN_DATE = new DateTime(2010, 12, 17) };
            Assert.That(this.l.RegistrationCheck(a1, this.m.Object), Is.EqualTo(false));
            Assert.That(this.l.RegistrationCheck(a2, this.m.Object), Is.EqualTo(true));
        }

        [Test]
        public void ToObservableCollectionTest()
        {
            Assert.That(this.l.ToObservableCollection(this.m.Object.GetAll()), Is.TypeOf(this.oc.GetType()));
        }
    }
}
