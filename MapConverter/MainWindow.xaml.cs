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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MapConverter.Components;
using MapConverter.Components.MapProcessors;

namespace MapConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".asc";
            dlg.Filter = "ASCII Maps(*.asc)|*.asc";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                this.FilenameTextBox.Text = filename;
            }
        }

        private void convertButton_Click(object sender, RoutedEventArgs e)
        {
            var asciiReader = new AsciiMapReader();
            var map = asciiReader.ReadMap(this.FilenameTextBox.Text, 500, 500);

            if (this.NormaliseCheckBox.IsChecked.HasValue && this.NormaliseCheckBox.IsChecked.Value)
            {
                var norm = new NormaliseElevation();
                map = norm.Process(map);
            }

            if (this.DebumpCheckBox.IsChecked.HasValue && this.DebumpCheckBox.IsChecked.Value)
            {
                var bump = new RemoveLoneBumps();
                map = bump.Process(map);
            }

            var det = new DetermineTileTypes();
            map = det.Process(map);

            var exporter = new MapExporter();
            exporter.Export(map, this.OutputTextBox.Text);

        }
    }
}
