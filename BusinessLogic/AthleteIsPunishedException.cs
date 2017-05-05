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

    /// <summary>
    /// Sérült sportoló kivétel
    /// </summary>
    public class AthleteIsPunishedException : Exception
    {
    }
}
