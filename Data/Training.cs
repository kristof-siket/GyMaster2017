namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Edzést megvalósító osztály
    /// </summary>
    public class Training
    {
        /// <summary>
        /// Gets or sets edzés neve property
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets edzés fő gyakorlata
        /// </summary>
        public EXERCISE FoGyakorlat { get; set; }

        /// <summary>
        /// Gets or sets edzéshez tartozó leírás
        /// </summary>
        public string Leiras { get; set; }
    }
}
