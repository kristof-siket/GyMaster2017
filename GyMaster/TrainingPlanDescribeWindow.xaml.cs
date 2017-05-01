using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GyMaster
{
    /// <summary>
    /// Interaction logic for TrainingPlanDescribeWindow.xaml
    /// </summary>
    public partial class TrainingPlanDescribeWindow : Window
    {
        ViewModel VM;
        public TrainingPlanDescribeWindow(ATHLETE selectedAthlete)
        {
            VM = ViewModel.Get();
            this.DataContext = VM;
            InitializeComponent();
            VM.BL.BuildTrainingPlanGUI(selectedAthlete, this.Content as Grid);
        }
    }
}
