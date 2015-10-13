using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace KompilatorPólDodatkowych
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(HandleEsc);

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
