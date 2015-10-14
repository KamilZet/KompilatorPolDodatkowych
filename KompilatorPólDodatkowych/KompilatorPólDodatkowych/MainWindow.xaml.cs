using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace KompilatorPólDodatkowych
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void ReadyToShowDelegate(object sender, EventArgs args);

        public event ReadyToShowDelegate ReadyToShow;

        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(HandleEsc);

            /*
             * Set splashscreen thread
             * 
             */
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2); // set number of seconds, after which MainWindow will be displayed
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

        }

        void timer_Tick(object sender, EventArgs e)
        {
            // This timer imitates a time-consuming operation (or a whole bunch of
            // such operations).
            // When done, it raises a custom event to let the splash screen know that its time is up.

            timer.Stop();

            if (ReadyToShow != null)
            {
                ReadyToShow(this, null);
            }
        }

        private void HandleEsc(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void validateParams(object sender,EventArgs e)
        {
            if (File.Exists(txMapFile.Text) && Directory.Exists(txOutDir.Text))
            {
                this.btnExecute.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF06"));
                this.btnExecute.IsEnabled = true;
            }
            else
            {
                this.btnExecute.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFEA405F"));
                this.btnExecute.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fd = new System.Windows.Forms.OpenFileDialog();
            fd.Filter = "Spaceman map file (*.map)|*.map";
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                this.txMapFile.Text = fd.FileName;
            };
 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog foldiag = new FolderBrowserDialog();
            foldiag.Description = "Select a folder in which to save your custom fields file...";
            //foldiag.SelectedPath = Application.StartupPath;

            if (foldiag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txOutDir.Text = foldiag.SelectedPath;
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            if (this.txMapFile.Text == "" || this.txOutDir.Text == "") 
            {
                System.Windows.Forms.MessageBox.Show("Input parameters missing!");
                return;
            }

            MapReader mr = new MapReader();
            
            mr.ReadFromTxt(this.txMapFile.Text);

            SourceFileWriter sfw = new SourceFileWriterXDoc();
            
            sfw.AddBody(mr.CustomFieldsList);

            sfw.Save(this.txOutDir.Text);

            this.btnExecute.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF49F500"));

            System.Windows.Forms.MessageBox.Show("Prcessing completed!");
        }

        private void txMapFile_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
