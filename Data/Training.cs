using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Edzést megvalósító osztály
    /// </summary>
    public class Training
    {
        /// <summary>
        /// Edzés neve property
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Edzés fő gyakorlata
        /// </summary>
        public EXERCISE FoGyakorlat { get; set; }

        /// <summary>
        /// Edzéshez tartozó leírás
        /// </summary>
        public string Leiras { get; set; }
    }
}
