using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataGenerator {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private DataBuilder DataBuilder;
        private GenConstrains constrains;
        private GenAnalizer Analizer;
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        public bool IsBusy { get; set; }

        public MainWindow() {
            InitializeComponent();
            IsBusy = false;
            this.progressBar.Visibility = Visibility.Hidden;
            DataBuilder = new DataBuilder( backgroundWorker );
            this.Analizer = new GenAnalizer();
            this.constrains = new GenConstrains();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        

        private async void GoBtn_Click(object sender, RoutedEventArgs e) {
            this.setUpConstrains();
            progressBar.Value = 0;
            backgroundWorker.CancelAsync();
            backgroundWorker.Dispose();
            DataBuilder.reinit();
            LoadingProcesTogle( true );
            DataBuilder.StartGeneration(constrains);
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            Dispatcher.Invoke( () => {
                progressBar.Value = e.ProgressPercentage;
            } );
            
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
            LoadingProcesTogle(true);
        }
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            LoadingProcesTogle( false );
            ShowAnswer(DataBuilder.response);
        }

        private void setUpConstrains() {
            int max_days = 120;

            constrains.MAX_COMMAND = (int)sliderCommMax.Value;
#region Obsolete
            /*
            int min_comm_per_day = (int) sliderCommMax.Minimum/max_days;
            constrains.MIN_COMMAND_PER_DAY = min_comm_per_day == 0 ? 1 : min_comm_per_day;
            int max_comm_per_day = (int) sliderCommMax.Maximum/max_days;
            constrains.MAX_COMMAND_PER_DAY = (int) max_comm_per_day == 0 ? 1 : max_comm_per_day;
            */
#endregion
            constrains.MAX_LOTS_PER_COMMAND = (int) sliderMaxProdByComm.Value;
            constrains.MIN_LOTS_PER_COMMAND = (int) sliderMaxProdByComm.Minimum;

        }

        private void LoadingProcesTogle(bool state) {
            Visibility LoadingState = state ? Visibility.Visible : Visibility.Hidden;
            Dispatcher.Invoke( () => {
                this.progressBar.Visibility = LoadingState;
                this.GoBtn.IsEnabled = !state;
            } );
        }

        private void ShowAnswer(bool res) {
            Dispatcher.Invoke( () => {
                if (!res) {
                    this.resp_status_lb.Content = "Fail";
                }
                else {
                    this.resp_status_lb.Content = "Success";
                    this.resp_nb_Comms_lb.Content = Analizer.getNbOfCommands(DataBuilder.Commandes);
                    this.resp_nb_Pieces_lb.Content = Analizer.getNbPieces(DataBuilder.Commandes);
                }
            } );
        }

    }
}
